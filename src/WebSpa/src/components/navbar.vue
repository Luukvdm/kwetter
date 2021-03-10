<script>
import { mapGetters, mapActions } from "vuex";

export default {
  computed: {
    ...mapGetters("oidcStore", [
      "oidcIsAuthenticated",
      "oidcAuthenticationIsChecked",
      "oidcUser"
    ])
  },
  methods: {
    ...mapActions("oidcStore", ["signOutOidc", "authenticateOidc"])
  }
};
</script>

<template>
  <header>
    <div class="content">
      <div class="logo">
        <router-link class="site-title" to="/">
          <img src="@/assets/logo.png" />
        </router-link>
      </div>
      <nav>
        <div class="nav-content">
          <router-link id="nav-home" to="/">Home</router-link>
          <div v-if="oidcUser">
            <router-link id="nav-account" to="/account/my">Account</router-link>
          </div>
          <a v-if="!oidcIsAuthenticated" @click="authenticateOidc">Login</a>
          <a v-if="oidcIsAuthenticated" @click="signOutOidc">Logout</a>
        </div>
      </nav>
    </div>
  </header>
</template>

<style scoped>
header .content {
  display: flex;
  flex-flow: row;
  justify-content: space-between;
  align-items: center;
  height: 100%;
}
.logo .site-title {
  font-family: "Secular One", Arial, Helvetica;
  color: inherit;
  text-decoration: none;
  font-size: 28pt;
}
.logo img {
  max-height: 65px;
}
nav .nav-content {
  display: flex;
  flex-flow: row;
  align-items: center;
}
nav .nav-content a {
  color: var(--c2);
  padding: 8px 15px;
  border-radius: 5px;
  opacity: 0.75;
  font-size: 20pt;
  text-decoration: none;
  transition: all 0.1s linear;
  font-weight: bold;
}
nav .nav-content a:hover,
nav .nav-content .router-link-active {
  color: var(--c0);
  opacity: 1;
}
nav .nav-content a.router-link-exact-active {
  opacity: 1;
}
.dropdown {
  position: relative;
  display: inline-block;
}
.dropdown-content {
  display: none;
  position: absolute;
  background-color: var(--main-color);
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  z-index: 1;
  border-radius: 5px;
}
.dropdown-content .dropdown-link {
  font-size: 0.8vw;
}
.dropdown-content a {
  display: block;
}
.dropdown:hover .dropdown-content {
  display: block;
}
</style>
