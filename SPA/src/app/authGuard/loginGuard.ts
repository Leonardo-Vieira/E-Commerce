import { Injectable } from "@angular/core";
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router
} from "@angular/router";
import Swal from "sweetalert2";

@Injectable()
export class LoginGuard implements CanActivate {
  constructor(private router: Router) {}
  redirectUrl: string;

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    this.redirectUrl = state.url;
    let local = JSON.parse(localStorage.getItem("currentUser"));

    if (local) {
      Swal.fire({
        type: "error",
        title: "Already logged in.",
        text: "You can't access the login page, since you're already logged in."
      });
      this.router.navigate([this.redirectUrl]);
      return false;
    } else {
      return true;
    }
  }
}
