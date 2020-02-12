import { Component } from '@angular/core';
import { Product } from './Models/product.model';
import { ProductService } from './product.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ecommerce-client';
  

  product: Product = new Product();

  products:Product[]; //get all as array list of product

  constructor(private productService: ProductService)
  {

  }

  ngOnInit(){
    this.productService.getAll().subscribe(products=>{
      this.products = products;
    });
  }

  setProduct(product:Product){
    this.products.push(product); //this push method used for show value in card after saved.
  }
}
