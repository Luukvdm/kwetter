import { createApp } from "vue";
import App from "./app.vue";
import router from "./router";
import store from "./store";
import Toast from "vue-toastification";
// Import the CSS or use your own!
import "vue-toastification/dist/index.css";

import globalCompPlugin from "./plugins/globalComponents.js";

createApp(App)
  .use(store)
  .use(router)
  .use(Toast, {})
  .use(globalCompPlugin)
  .mount("#app");
