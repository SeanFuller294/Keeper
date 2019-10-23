<template>
  <div class="home container-fluid">
    <h1>Welcome Home {{user.username}}</h1>
    <button v-if="user.id" @click="logout">logout</button>
    <router-link v-else :to="{name: 'login'}">Login</router-link>
    <div v-for="vault in this.vaults" :to="{name: 'vault'}" :key="vault.Id">{{vault.name}}</div>
    <div class="row">
      <div class="col-3" v-for="keep in this.keeps" v-if="keep.isPrivate == false" :key="keep.Id">
        <img :src="keep.img" />
        <br />
        {{keep.name}}
        <br />
        <button class="btn btn-primary" @click="viewKeep(keep.id)">views {{keep.views}}</button>
        <button class="btn btn-success">keep</button>
      </div>
    </div>
    <div class="row" v-if="user.id">
      <div class="col-12">
        <form>
          <div class="form-group">
            <label for="name"></label>
            <input class="form-control" id="name" placeholder="Name" />
          </div>
          <div class="form-group">
            <label for="description"></label>
            <input class="form-control" id="description" placeholder="Description" />
          </div>
          <div class="form-group">
            <label for="Url"></label>
            <input class="form-control" id="Url" placeholder="Img Url" />
          </div>
          <div class="form-group form-check">
            <input type="checkbox" class="form-check-input" id="isPrivate" />
            <label class="form-check-label" for="isPrivate">Private</label>
          </div>
          <button type="submit" class="btn btn-primary">Submit</button>
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
      newKeep: {
        name: "",
        description: "",
        img: "",
        isPrivate: false
      }
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
    // this.$store.dispatch("GetAuth");
    this.$store.dispatch("GetKeeps");
    this.$store.dispatch("GetVaults");
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    postAPost() {
      this.$store.dispatch("postKeep", this.newKeep);
    },
    viewKeep(keepId) {
      let keep = this.$store.dispatch("getOneKeep", keepId).then(res => {
        res.views++;
        this.$store.dispatch("viewKeep", res);
      });
    }
  }
};
</script>