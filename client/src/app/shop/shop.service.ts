import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Brand } from '../shared/models/brand';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';
import { Type } from '../shared/models/type';
import {ShopParams} from '../shared/models/shopParams';
@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl ="http://localhost:5067/api/";
  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams)
  {
    let params = new HttpParams();
    if(shopParams.brandId >0) params = params.append('brandId', shopParams.brandId);
    if(shopParams.typeId>0) params = params.append('typeId', shopParams.typeId);
       params = params.append('sort', shopParams.sort);
       params = params.append('pageIndex', shopParams.pageNumber);
       params = params.append('pageSize', shopParams.pageSize);
    if(shopParams.search) params = params.append('search', shopParams.search);

    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'product', {params});
  }

  getBrand()
  {
    return this.http.get<Brand[]>(this.baseUrl + 'product/brands');
  }

  getType()
  {
    return this.http.get<Type[]>(this.baseUrl + 'product/types');
  }
}
