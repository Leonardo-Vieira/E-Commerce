import { Provider } from "./provider";
import { Brand } from "./brand";
import { ProductType } from "./product-type";

export class Product {
    code: string;
    name: string;
    status: boolean;
    description: string;
    price: number;
    stock: number;
    provider: Provider;
    brand: Brand;
    productType: ProductType;
    id: string;
}
