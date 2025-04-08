import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ApiUrls } from "../config/apiUrls";
import { Order } from "../models/order";
import { OrderIn } from "../models/order-in";

@Injectable({
  providedIn: "root"
})
export class OrderService {
  private readonly apiUrls: ApiUrls;

  constructor(private http: HttpClient) {
    this.apiUrls = new ApiUrls();
  }

  get(url: string) {
    return this.http.get<Order[]>(`${url}/order/`);
  }
  getByClientId(id: string) {
    return this.http.get<Order[]>(
      `${this.apiUrls.orderApi}/order/${id}/clientId/`
    );
  }

  post(order: OrderIn) {
    return this.http.post(`${this.apiUrls.orderApi}/order`, order);
  }

  put(order: Order, id: string) {
    return this.http.put(`${this.apiUrls.orderApi}/order/${id}`, order);
  }

  // delete(id: string) {
  //   return this.http.delete(`${this.apiUrls.orderApi}/order/${id}`, {
  //     responseType: "text"
  //   });
  // }

  cancel(id: string) {
    return this.http.get(`${this.apiUrls.orderApi}/order/${id}/cancel`);
  }
}
