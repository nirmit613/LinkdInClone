import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllPostsComponent } from './all-posts/all-posts.component';
import { MyConnectionsComponent } from './my-connections/my-connections.component';
import { authGuard } from '@abp/ng.core';
import { MyPostsComponent } from './my-posts/my-posts.component';
import { ConnectionListsComponent } from './connection-lists/connection-lists.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'allPosts',
    component: AllPostsComponent,
    canActivate: [authGuard],
  },
  {
    path: 'myPosts',
    component: MyPostsComponent,
    canActivate: [authGuard],
  },
  {
    path: 'myConnections',
    component: MyConnectionsComponent,
    canActivate: [authGuard],
  },
  {
    path: 'connectionList',
    component: ConnectionListsComponent,
    canActivate: [authGuard],
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(m => m.AccountModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'tenant-management',
    loadChildren: () =>
      import('@abp/ng.tenant-management').then(m => m.TenantManagementModule.forLazy()),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
