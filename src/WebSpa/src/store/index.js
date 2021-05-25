import { createStore } from "vuex";
import modules from "./modules";
import oidcModule from "./oidcModule.js";
import createWebSocketPlugin from "./plugins/tweetHubPlugin.js";

modules.oidcStore = oidcModule;
// console.log(oidcModule.state.oidcStore.access_token);

const websocketPlugin = createWebSocketPlugin();

const store = createStore({
  state: {},
  mutations: {},
  actions: {},
  modules: modules,
  plugins: [websocketPlugin]
});

export default store;
