import { ModuleWithProviders, NgModule } from '@angular/core';
import { WAREHOUSE_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class WarehouseConfigModule {
  static forRoot(): ModuleWithProviders<WarehouseConfigModule> {
    return {
      ngModule: WarehouseConfigModule,
      providers: [WAREHOUSE_ROUTE_PROVIDERS],
    };
  }
}
