<template>
  <div class="keepView container-fluid">
    <div class="row">
      <div class="col-3 offset-5">
        <img :src="this.keep.img" />
        {{this.keep.name}}
        <br />
        {{this.keep.description}}
        <br />
        Views: {{this.keep.views}}
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
  methods: {},
  components: {}
};
</script>


<style scoped>
img {
  height: 400px;
}
</style>