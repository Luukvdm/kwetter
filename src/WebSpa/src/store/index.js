import { createStore } from "vuex";
import modules from "./modules";
import oidcModule from "./oidcModule.js";

modules.oidcStore = oidcModule;

export default createStore({
  state: {},
  mutations: {},
  actions: {},
  modules: modules,
  plugins: []
});
