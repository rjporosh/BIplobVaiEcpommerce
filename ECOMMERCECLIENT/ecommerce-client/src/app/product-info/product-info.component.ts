import { Component, OnInit, Input } from '@angular/core';
import { Product } from '../Models/product.model';

@Component({
  selector: 'product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.css']
})
export class ProductInfoComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  @Input() product: Product; //input using for set data from outside
}
