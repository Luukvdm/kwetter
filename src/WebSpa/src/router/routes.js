import { RouterView } from "vue-router";

import Home from "@/views/home.vue";

import MyAccount from "@/views/account/myAccount.vue";

import Profile from "@/views/profile/profile.vue";

import OidcCallback from "@/views/oidc/oidcCallback.vue";
import OidcPopupCallback from "@/views/oidc/oidcPopupCallback.vue";
import OidcCallbackError from "@/views/oidc/oidcCallbackError.vue";

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
    meta: {
      title: "Home Page",
      isPublic: false
    }
  },
  {
    path: "/account/",
    component: RouterView,
    children: [
      {
        path: "my",
        alias: "",
        name: "My Account",
        component: MyAccount,
        meta: {
          title: "My Account",
          isPublic: false
        }
      }
    ]
  },
  {
    path: "/profile/",
    component: RouterView,
    children: [
      {
        path: "me",
        name: "My Profile",
        component: Profile,
        meta: {
          title: "My Profile",
          isPublic: false
        }
      },
      {
        path: ":username",
        name: "Profile page",
        component: Profile,
        meta: {
          title: "Profile page",
          isPublic: false
        }
      }
    ]
  },
  {
    path: "/oidc/",
    component: RouterView,
    children: [
      {
        path: "callback",
        name: "oidc callback",
        component: OidcCallback,
        meta: {
          isPublic: true
        }
      },
      {
        path: "popup-callback",
        name: "oidc popup callback",
        component: OidcPopupCallback,
        meta: {
          isPublic: true
        }
      },
      {
        path: "callback-error",
        name: "oidc callback error",
        component: OidcCallbackError,
        meta: {
          isPublic: true
        }
      }
    ]
  }
];

export default routes;
