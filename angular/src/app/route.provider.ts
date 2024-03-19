import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/allPosts',
        name: '::All Posts',
        iconClass: 'fa-solid fa-bars',
        order: 2,
        layout: eLayoutType.application,
      },
      {
        path: '/myPosts',
        name: '::My Posts',
        iconClass: 'fas fa-plus',
        order: 3,
        layout: eLayoutType.application,
      },
      {
        path: '/connectionList',
        name: ':: Linkdin Users',
        iconClass: 'fa-solid fa-user',
        order: 4,
        layout: eLayoutType.application,
      },
      {
        path: '/myConnections',
        name: '::My Connections',
        iconClass: 'fa-solid fa-user-group',
        order: 5,
        layout: eLayoutType.application,
      },
    ]);
  };
}
