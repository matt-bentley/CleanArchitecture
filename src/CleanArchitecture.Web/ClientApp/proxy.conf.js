const { env } = require('process');

const PROXY_CONFIG = [
  {
    context: [
      "/api",
      "/swagger",
    ],
    target: 'https://localhost:7283',
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  },
  {
    context: [
      "/healthz",
      "/liveness"
    ],
    target: 'https://localhost:7251',
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
