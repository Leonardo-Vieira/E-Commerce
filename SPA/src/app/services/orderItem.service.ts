import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ApiUrls } from "../config/apiUrls";
import { OrderItem } from "../models/order-item";

@Injectable({
  providedIn: "root"
})
export class OrderItemsService {
  private readonly apiUrls: ApiUrls;

  constructor(private http: HttpClient) {
    this.apiUrls = new ApiUrls();
  }

  get(url: string) {
    return this.http.get<OrderItem[]>(`${url}/order`);
  }

  post(orderItems: OrderItem) {
    return this.http.post(`${this.apiUrls.orderApi}/order`, orderItems);
  }

  put(orderItems: OrderItem, id: string) {
    return this.http.put(`${this.apiUrls.orderApi}/order/${id}`, orderItems);
  }

  delete(id: string) {
    return this.http.delete(`${this.apiUrls.orderApi}/order/${id}`, {
      responseType: "text"
    });
  }
}
