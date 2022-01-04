import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PaginationModule} from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PagerComponent } from './components/pager/pager.component';


@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot()
    //Pagination Module has its own providers array and those providers need to be injected into our root module at startup
  ],
  exports:[
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent]
})
export class SharedModule { }
