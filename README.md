# node-dotnet-ssl-issue

Minimal reproduction of a SIGSEGV crash when making HTTPS requests from .NET code running inside Node.js via [node-api-dotnet](https://github.com/microsoft/node-api-dotnet).

## The Problem

HTTP requests from .NET's `HttpClient` work fine when hosted in Node.js, but HTTPS requests crash with a **SIGSEGV** on Node.js 22+. Node.js 18 is unaffected.

## Project Structure

- **`dotnetHTTPLib/`** — A .NET 9.0 library exposing a `TestHttps.FetchAsync(url)` method via `[JSExport]`
- **`test-http.ts`** — TypeScript script that calls the .NET method with both HTTP and HTTPS URLs

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (tested on v18 and v22+)

## Setup

```bash
npm install
```

## Run

```bash
npm test
```

This builds the .NET library and runs `test-http.ts`, which:

1. Fetches `http://httpbin.org/get` — works on all Node.js versions
2. Fetches `https://httpbin.org/get` — crashes with SIGSEGV on Node.js 22+
