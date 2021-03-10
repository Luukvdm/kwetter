import { vuexOidcCreateStoreModule } from "vuex-oidc";
import oidcConfig from "@/config/oidc.config.js";
// import axios from "axios";

let module = vuexOidcCreateStoreModule(
  oidcConfig,
  {
    namespaced: true,
    dispatchEventsOnWindow: true
  },
  // Optional OIDC event listeners
  {
    accessTokenExpired: () => {
      console.log("Access token did expire");
    },
    silentRenewError: () => console.log("OIDC user is unloaded"),
    userSignedOut: () => console.log("OIDC user is signed out"),
    oidcError: payload => console.log("OIDC error", payload)

    /* userLoaded: user => {
      axios.defaults.headers.common[
        "Authorization"
      ] = `Bearer ${user.access_token}`;
    } */
    /* userUnloaded: () => console.log("OIDC user is unloaded"),
    accessTokenExpiring: () => console.log("Access token will expire"),
    accessTokenExpired: () => {
      console.log("Access token did expire");
    },
    silentRenewError: () => console.log("OIDC user is unloaded"),
    userSignedOut: () => console.log("OIDC user is signed out"),
    oidcError: payload => console.log("OIDC error", payload)*/
  }
);

export default module;
