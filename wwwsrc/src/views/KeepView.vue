<template>
  <div class="keepView container-fluid">
    <button class="btn btn-primary" @click="goHome">Go Home</button>
    <div class="row justify-content-center">
      <div class="col-12">
        {{this.keep.name}}
        <br />
        <img :src="this.keep.img" />
        <br />
        {{this.keep.description}}
        <br />
        Views: {{this.keep.views}}
        Keeps: {{this.keep.hasBeenKept}}
        <p @click="deleteKeep(keep.id)">❌</p>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "keepView",
  data() {
    return {};
  },
  computed: {
    keep() {
      return this.$store.state.oneKeep;
    }
  },
  methods: {
    goHome() {
      this.$router.push({ name: "home" });
    },
    deleteKeep(keepId) {
      this.$store.dispatch("deleteKeep", keepId);
      this.$router.push({ name: "home" });
    }
  },
  components: {}
};
</script>


<style scoped>
img {
  height: 400px;
}
p {
  cursor: pointer;
}
</style>