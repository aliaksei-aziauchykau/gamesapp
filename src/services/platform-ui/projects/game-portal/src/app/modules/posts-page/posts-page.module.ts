import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { PostsPageComponent } from './posts-page.component';
import { PostsPageRoutingModule } from './posts-page-routing.module';

@NgModule({
    declarations: [PostsPageComponent],
    imports: [CommonModule, PostsPageRoutingModule],
})
export class PostsPageModule {}
