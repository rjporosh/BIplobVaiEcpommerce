import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from './Models/category.model';
import { Observable } from 'rxjs';


const apiUrl ="http://localhost:53605/api/"; // call to API controller from here 


@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http:HttpClient) 
  { 
    
  }

  public getAll():Observable<Category[]>
   {
    return this.http.get<Category[]>(apiUrl+"categories");
   }
}

   

