const api = {
  baseUrl: process.env.VUE_APP_BASE_API_URL,
  contentType: "application/json;charset=UTF-8",
  authTokenHeader: "Authorization",
  authTokenPrefix: "Bearer ",
  messageDuration: 3000,
  requestTimeout: 50000
};
module.exports = api;
