const appConfig = require("./app.config.js");
const apiConfig = require("./api.config.js");
const oidcConfig = require("./oidc.config.js");
module.exports = Object.assign({}, appConfig, apiConfig, oidcConfig);
