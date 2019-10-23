<template>
  <div class="VaultView container-fluid">
    <button class="btn btn-primary" @click="goHome">Go Home</button>
    <h1>{{this.vault.name}}</h1>
    <div class="row">
      <div class="col-3" v-for="keep in this.keeps">
        <img :src="keep.img" />
        <br />
        {{keep.name}}
        <br />
        <button class="btn btn-primary" @click="viewKeep(keep.id)">views {{keep.views}}</button>
        <button class="btn btn-success">keep</button>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "VaultView",
  data() {
    return {};
  },
  computed: {
    vault() {
      return this.$store.state.oneVault;
    },
    keeps() {
      return this.$store.state.keeps;
    }
  },
  mounted() {
    this.$store.dispatch("getKeepsByVault", this.$store.state.oneVault.id);
  },
  methods: {
    goHome() {
      this.$router.push({ name: "home" });
    }
  },
  components: {}
};
</script>


<style scoped>
</style>