<template>
  <div id="app">
    <router-view />
  </div>
</template>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
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
h3 {
  color: #7f534b;
}
</style>