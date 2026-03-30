import TestHttps from './dotnetHTTPLib/bin/Debug/net9.0/dotnetHTTPLib.cjs';

// HTTP works on ALL Node.js versions
const httpResult = await TestHttps.TestHttps.fetchAsync('http://httpbin.org/get');
console.log(httpResult); // "OK: 196 bytes"

// HTTPS crashes with SIGSEGV on Node.js 22+, works on Node.js 18
const httpsResult = await TestHttps.TestHttps.fetchAsync('https://httpbin.org/get');
console.log(httpsResult); // SIGSEGV on Node 22+