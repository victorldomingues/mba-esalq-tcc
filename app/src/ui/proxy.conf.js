const { LogLevel } = require("@angular/compiler-cli");

module.exports = {
    "/v1/traces": {
      target: process.env["OTEL_EXPORTER_OTLP_ENDPOINT"],
      secure: process.env["NODE_ENV"] !== "development",
      LogLevel: "debug",
      changeOrigin: true,
    },
  };
  
  function parseHeaders(s) {
    const headers = s.split(','); // Split by comma
    const result = {};
  
    headers.forEach(header => {
        const [key, value] = header.split('='); // Split by equal sign
        result[key.trim()] = value.trim(); // Add to the object, trimming spaces
    });
  
    return result;
  }