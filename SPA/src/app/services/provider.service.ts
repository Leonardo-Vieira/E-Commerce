import { Injectable } from "@angular/core";
import { ApiUrls } from "../config/apiUrls";
import { HttpClient } from "@angular/common/http";
import { Provider } from "../models/provider";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: "root"
})
export class ProviderService {
  private readonly apiUrls: ApiUrls;

  constructor(private http: HttpClient) {
    this.apiUrls = new ApiUrls();
  }

  get(url: string) {
    return this.http.get<Provider[]>(`${url}/provider`);
  }

  create(provider: Provider) {
    return this.http.post(`${this.apiUrls.productApi}/provider`, provider);
  }

  edit(id: string, provider: any) {
    return this.http.put(`${this.apiUrls.productApi}/provider/${id}`, provider);
  }

  delete(id: string) {
    return this.http
      .delete(`${this.apiUrls.productApi}/provider/${id}`, {
        responseType: "text"
      })
      .pipe(map((result: any) => console.log(result)));
  }
}
