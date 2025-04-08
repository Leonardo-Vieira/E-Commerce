import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import {
  MatButtonModule,
  MatCheckboxModule,
  MatFormFieldModule,
  MatIconModule,
  MatToolbarModule,
  MatTableModule,
  MatNativeDateModule
} from "@angular/material";
import { MatInputModule } from "@angular/material/input";
import { MatAutocompleteModule } from "@angular/material/autocomplete";
import { MatSelectModule } from "@angular/material/select";
import { RouterModule, Routes } from "@angular/router";
import { NgxLoadingModule } from "ngx-loading";

import { AppComponent } from "./app.component";
import { LoginComponent } from "./components/login/login.component";
import { RegisterComponent } from "./components/register/register.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HomeComponent } from "./components/home/home.component";
import { Globals } from "src/globals";
import { AuthGuard } from "./authGuard/authGuard";
import { JwtInterceptor } from "./helpers/jwt-interceptor";
import { ProductComponent } from "./components/collaborator-page/product/product.component";
import { BrandComponent } from "./components/collaborator-page/brand/brand.component";
import { TruncatePipe } from "./pipes/truncate.pipe";
import { ProductTypeComponent } from "./components/collaborator-page/product-type/product-type.component";
import { ProviderComponent } from "./components/collaborator-page/provider/provider.component";
import { AuthenticationService } from "./services/authentication.service";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { NavbarComponent } from "./components/navbar/navbar.component";
import { DemoMaterialModule } from "./material-module";
import { OrderComponent } from "./components/collaborator-page/order/order.component";
import { OrderComponent2 } from "./components/collaborator-page/order2/order2.component";
import { LoginGuard } from "./authGuard/loginGuard";

const router: Routes = [
  { path: "index", component: HomeComponent },
  { path: "login", canActivate: [LoginGuard], component: LoginComponent },
  { path: "register", component: RegisterComponent },
  {
    path: "collaborator-area/order2",
    canActivate: [AuthGuard],
    component: OrderComponent2
  },
  {
    path: "collaborator-area/order",
    canActivate: [AuthGuard],
    component: OrderComponent
  },
  {
    path: "collaborator-area/product",
    canActivate: [AuthGuard],
    component: ProductComponent
  },
  {
    path: "collaborator-area/brand",
    canActivate: [AuthGuard],
    component: BrandComponent
  },
  {
    path: "collaborator-area/producttype",
    canActivate: [AuthGuard],
    component: ProductTypeComponent
  },
  {
    path: "collaborator-area/provider",
    canActivate: [AuthGuard],
    component: ProviderComponent
  },
  { path: "", redirectTo: "/index", pathMatch: "full" },
  { path: "**", redirectTo: "/index", pathMatch: "full" }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    ProductComponent,
    BrandComponent,
    TruncatePipe,
    ProductTypeComponent,
    ProviderComponent,
    NavbarComponent,
    OrderComponent,
    OrderComponent2
  ],
  imports: [
    DemoMaterialModule,
    MatNativeDateModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatToolbarModule,
    MatAutocompleteModule,
    MatSelectModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule.forRoot(router),
    NgxLoadingModule.forRoot({})
  ],
  providers: [
    AuthenticationService,
    Globals,
    AuthGuard,
    LoginGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
