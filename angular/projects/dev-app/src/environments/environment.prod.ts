import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'Warehouse',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44332/',
    redirectUri: baseUrl,
    clientId: 'Warehouse_App',
    responseType: 'code',
    scope: 'offline_access Warehouse',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44332',
      rootNamespace: 'HaoLife.Abp.Warehouse',
    },
    Warehouse: {
      url: 'https://localhost:44333',
      rootNamespace: 'HaoLife.Abp.Warehouse',
    },
  },
} as Environment;
