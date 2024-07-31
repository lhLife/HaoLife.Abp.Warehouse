import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { WarehouseComponent } from './components/warehouse.component';
import { WarehouseService } from '@hao-life.Abp/warehouse';
import { of } from 'rxjs';

describe('WarehouseComponent', () => {
  let component: WarehouseComponent;
  let fixture: ComponentFixture<WarehouseComponent>;
  const mockWarehouseService = jasmine.createSpyObj('WarehouseService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [WarehouseComponent],
      providers: [
        {
          provide: WarehouseService,
          useValue: mockWarehouseService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WarehouseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
