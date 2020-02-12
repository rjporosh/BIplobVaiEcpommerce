import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from './Models/product.model';
import { Observable } from 'rxjs';


const baseUrl ="http://localhost:53605/api/"; // call to API controller from here 

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http:HttpClient)  {

   }


   public getAll():Observable<Product[]>
   {
    return this.http.get<Product[]>(baseUrl+"products");
   }

   public add(product:Product):Observable<Product>
   {

    return this.http.post<Product>(baseUrl+"products",product); //will return observable return
   }
}
