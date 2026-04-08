# AGENTS.md

## Project Overview

This is a hybrid Node.js + .NET 9.0 project that uses `node-api-dotnet` to bridge TypeScript with C# code. It investigates HTTPS connectivity issues when calling .NET HTTP clients from Node.js 22+ (SIGSEGV crash), which works correctly on Node.js 18.

The project also explores agentic token support using Microsoft Identity libraries (`microsoft.identity.web`).

## Tech Stack

- **Runtime**: Node.js (v18 works, v22+ has HTTPS crash)
- **Languages**: TypeScript, C# 12
- **.NET**: net9.0, using `Microsoft.JavaScript.NodeApi` for JS interop
- **Key packages**: `node-api-dotnet`, `tsx`, `microsoft.identity.web.*`

## Project Structure

- `test-http.ts` — TypeScript test harness that imports the compiled .NET module and tests HTTP/HTTPS calls
- `AgenticTokenProvider/` — .NET class library with `[JSExport]` annotated classes for Node.js interop
  - `TestHttps.cs` — HTTP client test (main entry point for repro)
  - `ITokenProvider.cs` — Interface for agentic token acquisition
  - `TokenProvider.cs` — Token provider implementation
  - `TokenProviderFactory.cs` — Factory to create token providers
  - `ServiceCollectionExtensions.cs` — DI registration helpers
- `AgenticTokenProvider/bin/Debug/net9.0/` — Generated JS/TS bindings and compiled output

## Build & Run

```bash
npm install          # Install Node.js dependencies
npm run build        # Build the .NET library: dotnet build ./AgenticTokenProvider
npm test             # Build + run test: tsx test-http.ts
```

**Prerequisites**: .NET 9.0 SDK, Node.js v18+

## Code Conventions

- C# files use `[JSExport]` attribute to expose APIs to Node.js
- TypeScript imports the .NET module via CommonJS from `AgenticTokenProvider/bin/Debug/net9.0/AgenticTokenProvider.cjs`
- No tsconfig — TypeScript is executed directly via `tsx`
- No test framework — tests are simple script executions with console output
- Generated files under `bin/` and `obj/` should not be edited manually
