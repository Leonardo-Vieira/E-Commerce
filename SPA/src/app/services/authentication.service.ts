import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ApiUrls } from "../config/apiUrls";
import { Authentication } from "../models/authentication";
import { Subject } from "rxjs";

import { GlobalInterface } from "../interfaces/global-interface";
import { Globals } from "src/globals";
import { map } from "rxjs/operators";
import * as jwt_decode from "jwt-decode";
import Swal from "sweetalert2";

@Injectable()
export class AuthenticationService {
  private readonly apiUrls: ApiUrls;
  public configObservable = new Subject<GlobalInterface>();

  constructor(private http: HttpClient, private globals: Globals) {
    this.apiUrls = new ApiUrls();
  }

  register(user: Authentication) {
    return this.http.post(
      `${this.apiUrls.authenticationApi}/auth/register`,
      user
    );
  }

  login(email: string, password: string) {
    return this.http
      .post<any>(`${this.apiUrls.authenticationApi}/auth/login`, {
        email,
        password
      })
      .pipe(
        map(user => {
          // login successful if there's a jwt token in the response
          if (user && user.accessToken) {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem("currentUser", JSON.stringify(user));
            try {
              const decodedToken = jwt_decode(user.accessToken);
              this.configObservable.next({
                isCollaborator: (decodedToken.clb.toLowerCase() == "true") ? true : false,
                userLogged: true
              });

            } catch (error) {
              Swal.fire({
                type: "error",
                title: "error",
                text: error
              });
            }
          }
          return user;
        })
      );
  }

  logout() {
    localStorage.removeItem("currentUser");
    this.configObservable.next({ isCollaborator: false, userLogged: false });
  }
}
