const signalr = require("@microsoft/signalr");
import { baseUrl } from "@/config/api.config.js";
import store from "@/store";

export function hubConnectionBuilder(endpoint) {
  // const url = new URL(endpoint, baseUrl);
  // const url = baseUrl + endpoint;
  const url = baseUrl + endpoint;
  let connection = new signalr.HubConnectionBuilder()
    .withUrl(url.toString(), {
      accessTokenFactory: () => {
        if (store.state.oidcStore.access_token) {
          return store.state.oidcStore.access_token;
        }
        return;
      }
    })
    .configureLogging(signalr.LogLevel.Information)
    .withAutomaticReconnect()
    .build();
  return connection;
}
