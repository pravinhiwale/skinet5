import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from '../../shared/models/product';
import { ShopService } from '../shop.service';
import {BreadcrumbService} from 'xng-breadcrumb'

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product!:IProduct
  constructor(private shopService:ShopService,private activatedRoute:ActivatedRoute, 
    private  bcService:BreadcrumbService) { 
      this.bcService.set('@productDetails',' ');
    }

  ngOnInit(): void {
    this.loadProduct();
  }
  loadProduct(){
    var shopId=this.activatedRoute.snapshot.paramMap.get('id')
    var id=shopId?+shopId:0
    this.shopService.getProduct(id).subscribe(product=>{
      this.product=product;
      this.bcService.set('@productDetails',product.name)
      },error=>{
        console.log(error);
        }
      )
  }
}
