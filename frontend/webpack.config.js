// webpack.config.js
const path = require("path");

module.exports = {
  // ... Otras configuraciones

  // resolve: {
  //   fallback: {
  //     os: require.resolve("os-browserify/browser"),
  //     crypto: require.resolve("crypto-browserify"),
  //   },
  // },
  resolve: {
    // ...
    // add the fallback setting below
    fallback: {
      fs: false,
      os: false,
      path: false,
    },
    // ...
  },
};
