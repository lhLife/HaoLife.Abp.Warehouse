import { TestBed } from '@angular/core/testing';
import { WarehouseService } from './services/warehouse.service';
import { RestService } from '@abp/ng.core';

describe('WarehouseService', () => {
  let service: WarehouseService;
  const mockRestService = jasmine.createSpyObj('RestService', ['request']);
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: RestService,
          useValue: mockRestService,
        },
      ],
    });
    service = TestBed.inject(WarehouseService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
