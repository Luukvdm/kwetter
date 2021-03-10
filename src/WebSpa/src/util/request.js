import axios from "axios";
import store from "@/store";
import {
  baseUrl,
  authTokenHeader,
  authTokenPrefix,
  requestTimeout,
  contentType
} from "@/config/api.config.js";

const instance = axios.create({
  baseURL: baseUrl,
  timeout: requestTimeout,
  headers: {
    "Content-Type": contentType
  }
});

instance.interceptors.request.use(
  config => {
    if (store.state.oidcStore.access_token) {
      config.headers[authTokenHeader] =
        authTokenPrefix + store.state.oidcStore.id_token;
    }

    // if (config.data) config.data = Vue.prototype.$baseLodash.pickBy(config.data, Vue.prototype.$baseLodash.identity)

    // Do form shizzle
    // if (config.data && config.headers['Content-Type'] === 'application/x-www-form-urlencoded;charset=UTF-8') config.data = qs.stringify(config.data)

    // if (debounce.some((item) => config.url.includes(item)))
    //   loadingInstance = Vue.prototype.$baseLoading()
    return config;
  },
  error => {
    console.log(error);
    return Promise.reject(error);
  }
);

export default instance;
