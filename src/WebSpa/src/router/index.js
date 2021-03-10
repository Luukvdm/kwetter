import { createRouter, createWebHistory } from "vue-router";
import { vuexOidcCreateRouterMiddleware } from "vuex-oidc";
import routes from "./routes.js";
import store from "@/store";

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

router.beforeEach(vuexOidcCreateRouterMiddleware(store, "oidcStore"));

export default router;
