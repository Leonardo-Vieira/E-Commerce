import { Injectable } from "@angular/core";
import { ApiUrls } from "../config/apiUrls";
import { HttpClient } from "@angular/common/http";
import { Product } from "../models/product";
import { map, catchError } from "rxjs/operators";
import { throwError, Subject, BehaviorSubject } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class ProductService {
  private readonly apiUrls: ApiUrls;

  constructor(private http: HttpClient) {
    this.apiUrls = new ApiUrls();
  }

  create(product: Product) {
    return this.http.post(`${this.apiUrls.productApi}/product`, product);
  }

  get(url: string) {
    return this.http
      .get<Product[]>(`${url}/product`);
  }

  edit(id: string, product: any) {
    return this.http.put(`${this.apiUrls.productApi}/product/${id}`, product);
  }

  delete(id: string) {
    return this.http
      .delete(`${this.apiUrls.productApi}/product/${id}`, {
        responseType: "text"
      })
      .pipe(map((result: any) => console.log(result)));
  }
}
