// Core-js comes from oidc client I think
// Not sure why this needs it, but the vuex-oidc store example uses it
// https://github.com/IdentityModel/oidc-client-js/commit/68ac9be44711a785095a8fae358bc0f9ea3ba70d
import "core-js/features/promise";
import { vuexOidcProcessSilentSignInCallback } from "vuex-oidc";

vuexOidcProcessSilentSignInCallback();
