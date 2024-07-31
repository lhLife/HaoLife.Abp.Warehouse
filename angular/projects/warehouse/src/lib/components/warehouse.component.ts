import { Component, OnInit } from '@angular/core';
import { WarehouseService } from '../services/warehouse.service';

@Component({
  selector: 'lib-warehouse',
  template: ` <p>warehouse works!</p> `,
  styles: [],
})
export class WarehouseComponent implements OnInit {
  constructor(private service: WarehouseService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
