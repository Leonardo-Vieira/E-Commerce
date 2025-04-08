import { Injectable } from "@angular/core";
import {
  CanActivate,
  Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot
} from "@angular/router";
import Swal from "sweetalert2";
import * as jwt_decode from "jwt-decode";
import { AuthenticationService } from "../services/authentication.service";

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authService: AuthenticationService
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let local = JSON.parse(localStorage.getItem("currentUser"));

    if (local) {
      const decodedToken = jwt_decode(local.accessToken);
      let collaboratorStatus =
        decodedToken.clb.toLowerCase() == "true" ? true : false;
      if (collaboratorStatus) {
        return true;
      } else {
        Swal.fire({
          type: "error",
          title: "Forbidden access.",
          text: "You can't enter this page."
        });
        this.router.navigate(["index"]);
        return false;
      }
    } else {
      Swal.fire({
        type: "error",
        title: "Forbidden access.",
        text: "You can't enter this page."
      });
      this.router.navigate(["index"]);
      return false;
    }
  }
}
