import { Injectable } from '@angular/core';
import { MenuItem } from './menu-item';
import { ActivatedRoute } from '@angular/router';

@Injectable()
export class MenuService {

  constructor( private router: ActivatedRoute) { }

  getMenuItems(): MenuItem[] {

    const role = localStorage.getItem('role');
    const items: MenuItem[] = [];

    switch (role) {
      case 'a1':
        items.push({
          title: 'Dashboard',
          route: '/dashboard',
          icon: 'assessment'
        });
        items.push({
          title: 'User Information',
          route: '/user-information/user-information',
          icon: 'assignment_ind',
        });
        items.push({
          title: 'Dataset',
          route: '/datasetadministration/manage-dataset',
          icon: 'line_style',
        });
        break;
      
      case 'a2':
        items.push({
          title: 'Dashboard',
          route: '/dashboard',
          icon: 'assessment'
        });
        items.push({
          title: 'User Information',
          route: '/user-information/user-information',
          icon: 'assignment_ind',
        });
        items.push({
          title: 'Dataset',
          route: '/datasetadministration/manage-dataset',
          icon: 'line_style',
        });
        break;
      
        default:
          
          break;
    }
      
  

    return items;
  }
}
