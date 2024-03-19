import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Linkd',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44385/',
    redirectUri: baseUrl,
    clientId: 'Linkd_App',
    responseType: 'code',
    scope: 'offline_access Linkd',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44385',
      rootNamespace: 'Linkd',
    },
  },
} as Environment;
