import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ApiUrls } from "../config/apiUrls";
import { ProductType } from "../models/product-type";

@Injectable({
  providedIn: "root"
})
export class ProductTypesService {
  private readonly apiUrls: ApiUrls;

  constructor(private http: HttpClient) {
    this.apiUrls = new ApiUrls();
  }

  get(url: string) {
    return this.http.get<ProductType[]>(`${url}/producttype`);
  }

  post(productype: ProductType) {
    return this.http.post(`${this.apiUrls.productApi}/producttype`, productype);
  }

  put(productype: ProductType, id: string) {
    return this.http.put(`${this.apiUrls.productApi}/producttype/${id}`, productype);
  }

  delete(id: string) {
    return this.http.delete(`${this.apiUrls.productApi}/producttype/${id}`, {
      responseType: "text"
    });
  }
}
