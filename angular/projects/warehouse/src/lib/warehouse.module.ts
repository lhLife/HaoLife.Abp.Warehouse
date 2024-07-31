import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { WarehouseComponent } from './components/warehouse.component';
import { WarehouseRoutingModule } from './warehouse-routing.module';

@NgModule({
  declarations: [WarehouseComponent],
  imports: [CoreModule, ThemeSharedModule, WarehouseRoutingModule],
  exports: [WarehouseComponent],
})
export class WarehouseModule {
  static forChild(): ModuleWithProviders<WarehouseModule> {
    return {
      ngModule: WarehouseModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<WarehouseModule> {
    return new LazyModuleFactory(WarehouseModule.forChild());
  }
}
