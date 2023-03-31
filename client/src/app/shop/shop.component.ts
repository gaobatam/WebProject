import { Component,OnInit,ViewChild,ElementRef } from '@angular/core';
import { Brand } from '../shared/models/brand';
import { Product } from '../shared/models/product';
import { ShopParams } from '../shared/models/shopParams';
import { Type } from '../shared/models/type';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm?: ElementRef;
  products: Product [] =[];
  brands: Brand[] = [];
  types: Type[] =[];
  shopParams= new ShopParams();
  sortOption = [
    {name:'Alphabetical', value:'name'},
    {name:'Price: Low to High', value:'priceAsc'},
    {name:'Price: High to Low', value:'priceDesc'},
  ];

  totalCount=0;

  constructor(private shopService: ShopService) {}
   
  ngOnInit(): void {
    this.getProduct();
    this.getBrands();
    this.getTypes();
  }

  getProduct()
  { 
    this.shopService.getProducts(this.shopParams).subscribe({
      next: respone=> {
        this.products = respone.data;
        this.shopParams.pageNumber = respone.pageIndex;
        this.shopParams.pageSize = respone.pageSize;
        this.totalCount = respone.count;
      }, 
      error: error => console.log(error)
    })
  }

  getBrands()
  { 
    this.shopService.getBrand().subscribe({
      next: respone=> this.brands = [{id:0, name:'All'}, ...respone], 
      error: error => console.log(error)
    })
  }

  getTypes()
  { 
    this.shopService.getType().subscribe({
      next: respone=> this.types = [{id:0, name:'All'}, ...respone], 
      error: error => console.log(error)
    })
  }

  onBrandSelected(brandId: number)
  {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProduct();
  }

  onTypeSelected(typeId: number)
  {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProduct();
  }

  onSortSelected(event: any)
  {
    this.shopParams.sort = event.target.value;
    this.getProduct();
  }

  onPageChanged(event: any)
  {
    if(this.shopParams.pageNumber !=event)
      this.shopParams.pageNumber = event;
      this.getProduct();
  }

  onSearch()
  {
    this.shopParams.search = this.searchTerm?.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProduct();
  }

  onReset()
  {
    if(this.searchTerm) this.searchTerm.nativeElement.value ='';
    this.shopParams = new ShopParams();
    this.getProduct();
  }
}
