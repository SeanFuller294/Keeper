import Vue from 'vue'
import Router from 'vue-router'
// @ts-ignore
import Home from './views/Home.vue'
// @ts-ignore
import Login from './views/Login.vue'
// @ts-ignore
import KeepView from './views/KeepView.vue'
// @ts-ignore
import VaultView from './views/VaultView.vue'
Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/keep',
      name: 'keepView',
      component: KeepView
    },
    {
      path: '/vault',
      name: 'vaultView',
      component: VaultView
    }
  ]
})
