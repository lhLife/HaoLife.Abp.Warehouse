import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class WarehouseService {
  apiName = 'Warehouse';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/Warehouse/sample' },
      { apiName: this.apiName }
    );
  }
}
