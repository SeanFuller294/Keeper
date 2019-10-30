<template>
  <div class="home container-fluid">
    <h1>{{this.message}} {{user.username}}</h1>
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
    <div class="row justify-content-around">
      <div
        class="col-3 mt-3 border mx-2"
        v-for="keep in this.keeps"
        v-if="keep.isPrivate == false"
        :key="keep.Id"
        id="keep"
      >
        <br />
        <img :src="keep.img" />
        <br />
        {{keep.name}}
        <br />
        <button class="btn btn-primary" @click="viewKeep(keep.id)">Views {{keep.views}}</button>
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
        class="col-3 mt-3 border mx-2"
        v-for="keep in this.keeps"
        v-if="user.id == keep.userId && keep.isPrivate == true"
        :key="keep.Id"
        id="keep"
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
        <form @submit="postAKeep" class="my-2">
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
          <button type="submit" class="btn btn-primary mb-4">Post Keep</button>
        </form>
        <form @submit="createVault" class="my-2">
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
      vaultDescription: "",
      message: "Welcome Home"
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
    if (this.$store.state.user.username == "Jim") {
      this.message = "He's Dead";
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
      this.message = "Welcome Home";
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
  color: #e5f2c9;
  cursor: pointer;
}
body {
  background-color: #1e1a1d;
  color: #7f534b;
}
#keep {
  background-color: #1f0318;
  color: #ede3e4;
}
form {
  color: #7f534b;
}
h1 {
  color: #7f534b;
}
</style>