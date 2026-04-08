import { createRequire } from 'node:module';
const require = createRequire(import.meta.url);
const dotnetLib = require('./AgenticTokenProvider/bin/Debug/net9.0/AgenticTokenProvider.cjs');

const tenantId = '3f3d1cea-7a18-41af-872b-cfbbd5140984';
const clientId = 'f7c0e80b-b00e-48ac-8b57-363a2a9a7cda';
const clientSecret = process.env.AZURE_CLIENT_SECRET || '';
const agentIdentityId = '200fc45d-59f2-4260-9fd2-5413171ddcdb';
const agentUserOid = '6037c803-c89a-4788-afae-8c9d24836a6e';

const scope = 'https://graph.microsoft.com/.default';

console.log('TokenProviderFactory:', dotnetLib.TokenProviderFactory);
console.log('create:', dotnetLib.TokenProviderFactory.create);

const tokenProvider = dotnetLib.TokenProviderFactory.create(tenantId, clientId, clientSecret, scope);

console.log('tokenProvider:', tokenProvider);

const token = await tokenProvider.getTokenAsync(agentIdentityId, agentUserOid);
console.log('Token:', token);
