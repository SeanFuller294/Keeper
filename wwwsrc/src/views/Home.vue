<template>
  <div class="home container-fluid">
    <h1>Welcome Home {{user.username}}</h1>
    <button v-if="user.id" @click="logout">logout</button>
    <router-link v-else :to="{name: 'login'}">Login</router-link>
    <div class="row">
      <div
        id="VaultNames"
        class="col-4"
        v-for="vault in this.vaults"
        v-if="vault.userId == user.id"
        :key="vault.Id"
        @click="viewVault(vault.id)"
      >{{vault.name}}</div>
    </div>
    <hr />
    <div class="row">
      <div
        class="col-3 mt-3 border"
        v-for="keep in this.keeps"
        v-if="keep.isPrivate == false"
        :key="keep.Id"
      >
        <img :src="keep.img" />
        <br />
        {{keep.name}}
        <br />
        <button class="btn btn-primary" @click="viewKeep(keep.id)">views {{keep.views}}</button>
        <div class="dropdown">
          <div class="btn-group">
            <button
              type="button"
              class="btn btn-success dropdown-toggle"
              data-toggle="dropdown"
              aria-haspopup="true"
              aria-expanded="false"
            >Keep {{keep.hasBeenKept}}</button>
            <div class="dropdown-menu">
              <a
                class="dropdown-item"
                v-for="vault in vaults"
                :key="vault.Id"
                @click="addKeepToVault(vault.id, keep.id)"
              >{{vault.name}}</a>
            </div>
          </div>
        </div>
      </div>
      <div
        class="col-3 mt-3 border"
        v-for="keep in this.keeps"
        v-if="user.id == keep.userId && keep.isPrivate == true"
        :key="keep.Id"
      >
        👁️
        <br />
        <img :src="keep.img" />
        <br />
        {{keep.name}}
        <br />
        <button class="btn btn-primary" @click="viewKeep(keep.id)">views {{keep.views}}</button>
        <div class="dropdown">
          <div class="btn-group">
            <button
              type="button"
              class="btn btn-success dropdown-toggle"
              data-toggle="dropdown"
              aria-haspopup="true"
              aria-expanded="false"
            >Keep {{keep.hasBeenKept}}</button>
            <div class="dropdown-menu">
              <a
                class="dropdown-item"
                v-for="vault in vaults"
                :key="vault.id"
                @click="addKeepToVault(vault.id, keep.id)"
              >{{vault.name}}</a>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row justify-content-center" v-if="user.id">
      <div class="col-4">
        <form @submit="postAKeep">
          <div class="form-group">
            <input class="form-control" id="name" placeholder="Name" v-model="name" required />
          </div>
          <div class="form-group">
            <input
              class="form-control"
              id="description"
              placeholder="Description"
              v-model="description"
              required
            />
          </div>
          <div class="form-group">
            <input class="form-control" id="Url" placeholder="Img Url" v-model="img" required />
          </div>
          <div class="form-group form-check">
            <input type="checkbox" class="form-check-input" id="isPrivate" v-model="isPrivate" />
            <label class="form-check-label" for="isPrivate">Private</label>
          </div>
          <button type="submit" class="btn btn-primary">Post Keep</button>
        </form>
        <form @submit="createVault">
          <div class="form-group">
            <input
              class="form-control"
              id="vaultName"
              placeholder="Name"
              v-model="vaultName"
              required
            />
          </div>
          <div class="form-group">
            <input
              class="form-control"
              id="vaultDescription"
              placeholder="Description"
              v-model="vaultDescription"
              required
            />
          </div>
          <button type="submit" class="btn btn-primary">Create Vault</button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "home",
  data() {
    return {
      name: "",
      description: "",
      img: "",
      isPrivate: false,
      vaultName: "",
      vaultDescription: ""
    };
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
    keeps() {
      return this.$store.state.keeps;
    },
    vaults() {
      return this.$store.state.vaults;
    }
  },
  mounted() {
    this.$store.dispatch("GetKeeps");
    this.$store.dispatch("GetVaults");
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    postAKeep() {
      let newKeep = {
        name: this.name,
        description: this.description,
        img: this.img,
        isPrivate: this.isPrivate
      };
      this.$store.dispatch("postKeep", newKeep);
    },
    viewKeep(keepId) {
      let keep = this.$store.dispatch("getOneKeep", keepId).then(res => {
        res.views++;
        this.$store.dispatch("viewKeep", res);
      });
    },
    viewVault(vaultId) {
      this.$store.dispatch("getOneVault", vaultId);
    },
    addKeepToVault(vaultId, keepId) {
      this.$store.dispatch("addKeepToVault", { vaultId, keepId });
    },
    createVault() {
      this.$store.dispatch("createVault", {
        name: this.vaultName,
        description: this.vaultDescription
      });
    }
  }
};
</script>
<style>
#VaultNames {
  color: blue;
  cursor: pointer;
}
</style>