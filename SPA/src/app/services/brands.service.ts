import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ApiUrls } from "../config/apiUrls";
import { Brand } from "../models/brand";

@Injectable({
  providedIn: "root"
})
export class BrandsService {
  private readonly apiUrls: ApiUrls;

  constructor(private http: HttpClient) {
    this.apiUrls = new ApiUrls();
  }

  get(url: string) {
    return this.http.get<Brand[]>(`${url}/brand`);
  }

  create(body: Brand) {
    return this.http.post(`${this.apiUrls.productApi}/brand`, body);
  }

  update(body: Brand, id: string) {
    return this.http.put(`${this.apiUrls.productApi}/brand/${id}`, body);
  }
  delete(id: string) {
    return this.http.delete(`${this.apiUrls.productApi}/brand/${id}`);
  }
}
