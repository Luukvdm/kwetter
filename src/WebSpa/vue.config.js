module.exports = {
  pages: {
    app: {
      entry: "src/main.js",
      template: "public/index.html",
      filename: "index.html",
      excludeChunks: ["silent-renew-oidc"]
    },
    silentrenewoidc: {
      entry: "src/util/silent-renew-oidc.js",
      template: "public/silent-renew-oidc.html",
      filename: "oidc/silent-renew.html",
      excludeChunks: ["app"]
    }
  }
};
