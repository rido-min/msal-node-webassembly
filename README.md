# node-dotnet-ssl-issue

## The Problem

msal-node does not support agentic tokens

## Project Structure

- **`AgenticTokenProvider/`** — A .NET 9.0 library exposing a `TestHttps.FetchAsync(url)` method via `[JSExport]`
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
