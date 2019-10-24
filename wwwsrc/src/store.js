import Vue from 'vue'
import Vuex, { Store } from 'vuex'
import Axios from 'axios'
import router from './router'
import AuthService from './AuthService'

Vue.use(Vuex)

let baseUrl = location.host.includes('localhost') ? '//localhost:5000/' : '/'

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
})

export default new Vuex.Store({
  state: {
    user: {},
    keeps: [],
    vaults: [],
    oneKeep: {},
    oneVault: {}
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    resetState(state) {
      //clear the entire state object of user data
      state.user = {}
    },
    setKeeps(state, payload) {
      state.keeps = payload.data;
    },
    setOneKeep(state, payload) {
      state.oneKeep = payload.data;
    },
    setVaults(state, payload) {
      state.vaults = payload.data;
    },
    setOneVault(state, payload) {
      state.oneVault = payload.data;
    }
  },
  actions: {
    async register({ commit, dispatch }, creds) {
      try {
        let user = await AuthService.Register(creds)
        commit('setUser', user)
        router.push({ name: "home" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async GetAuth({ commit, dispatch }) {
      let data = await AuthService.Authenticate()
      console.log(data);

      return data
    },
    async login({ commit, dispatch }, creds) {
      try {
        let user = await AuthService.Login(creds)
        commit('setUser', user)
        router.push({ name: "home" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async logout({ commit, dispatch }) {
      try {
        let success = await AuthService.Logout()
        if (!success) { }
        commit('resetState')
        //router.push({ name: "login" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async GetKeeps({ commit }) {
      let data = await api.get("keeps")
      commit("setKeeps", data)
    },
    async getOneKeep({ commit }, payload) {
      let data = await api.get("keeps/" + payload)
      commit("setOneKeep", data)
      return data.data
    },
    async getKeepsByVault({ commit, dispatch }, payload) {
      let data = await api.get("vaultKeeps/" + payload).then(res => {
        commit("setKeeps", res)
      })
    },
    async addKeepToVault({ commit, dispatch }, payload) {
      let newVK = {
        vaultId: payload.vaultId,
        keepId: payload.keepId,
        userId: ""
      }
      let keep = await api.get("keeps/" + payload.keepId).then(res => {
        res.data.hasBeenKept++
        api.put("keeps/" + payload.keepId, res.data)
      })

      let data = await api.post("vaultkeeps", newVK)
      let steve = await api.get("keeps");


    },
    async GetVaults({ commit }) {
      try {
        let data = await api.get("vaults")
        commit("setVaults", data)
      } catch (error) {
        console.error(error);
      }
    },
    async getOneVault({ commit }, payload) {
      let data = await api.get("vaults/" + payload)
      commit("setOneVault", data)
      router.push("/vault")
    },
    async createVault({ commit, dispatch }, payload) {
      let v = api.post("vaults", payload)
      await api.get("vaults")
      location.reload()
    },
    async deleteVault({ commit, dispatch }, payload) {
      await api.delete("vaults/" + payload)
      let vaults = await api.get("vaults")
      commit("setVaults", vaults)
    },
    async viewKeep({ commit, dispatch }, payload) {
      let d = await api.put("keeps/" + payload.id, payload)
      let newKeep = {
        data: {
          name: payload.name,
          description: payload.description,
          hasBeenKept: payload.hasBeenKept,
          id: payload.id,
          img: payload.img,
          isPrivate: payload.isPrivate,
          shares: payload.shares,
          userId: payload.userId,
          views: payload.views
        }
      }
      commit("setOneKeep", newKeep)
      router.push("/keep")
    },
    async postKeep({ commit, dispatch }, payload) {
      await api.post("keeps", payload)
      let keeps = await api.get("keeps")
      commit("setKeeps", keeps)
    },
    async deleteKeep({ commit, dispatch }, payload) {
      await api.delete("keeps/" + payload)
      let keeps = await api.get("keeps")
      commit("setKeeps", keeps)
    },
    async removeKeep({ commit, dispatch }, payload) {
      await api.put("vaultKeeps/", payload)
    }
  }
})
