const baseUrl = process.env.VUE_APP_BASE_URL + process.env.VUE_APP_PUBLIC_PATH;
const idsBaseUrl = process.env.VUE_APP_BASE_IDENTITY_SERVER_URL;
const appName = process.env.VUE_APP_APP_NAME;

const oidcConfig = {
  authority: idsBaseUrl + "/",
  clientId: appName,
  redirectUri: baseUrl + "/oidc/callback",
  popupRedirectUri: baseUrl + "/oidc/popup-callback",
  responseType: "code",
  scope: "openid profile tweet.api userrelations.api media.api hub",
  automaticSilentRenew: true,
  automaticSilentSignin: false,
  silentRedirectUri: baseUrl + "/oidc/silent-renew.html"
};

module.exports = oidcConfig;
