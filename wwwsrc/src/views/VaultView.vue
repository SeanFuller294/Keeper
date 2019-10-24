<template>
  <div class="VaultView container-fluid">
    <p class="float-right" @click="deleteVault">❌</p>
    <button class="btn btn-primary" @click="goHome">Go Home</button>
    <h1>{{this.vault.name}}</h1>
    <h3>{{this.vault.description}}</h3>
    <div class="row">
      <div class="col-3" v-for="keep in this.keeps">
        <img :src="keep.img" />
        <br />
        {{keep.name}}
        <br />
        <button class="btn btn-primary" @click="viewKeep(keep.id)">views {{keep.views}}</button>
        <button class="btn btn-success">keep</button>
        <p @click="removeKeep(keep.id)">🗑️</p>
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
    },
    deleteVault() {
      this.$store.dispatch("deleteVault", this.$store.state.oneVault.id);
      this.$router.push({ name: "home" });
    },
    removeKeep(kId) {
      this.$store.dispatch("removeKeep", {
        vaultId: this.$store.state.oneVault.id,
        keepId: kId
      });
      this.$store.dispatch("getKeepsByVault", this.$store.state.oneVault.id);
    }
  },
  components: {}
};
</script>


<style scoped>
p {
  cursor: pointer;
}
</style>