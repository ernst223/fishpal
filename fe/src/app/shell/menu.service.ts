import { Injectable } from '@angular/core';
import { MenuItem } from './menu-item';
import { ActivatedRoute } from '@angular/router';

@Injectable()
export class MenuService {

  constructor(private router: ActivatedRoute) { }

  getMenuItems(): MenuItem[] {

    const role = localStorage.getItem('role');
    const items: MenuItem[] = [];
    const roles = ['A1', 'A2', 'A3', 'A4', 'A5', 'A6', 'A7', 'A8', 'A9', 'A10', 'A11', 'A12', 'A13',
    'B1', 'B2', 'B3', 'B4', 'B5', 'B6', 'B7', 'B8', 'B9', 'B10', 'B11', 'B12', 'B13',
    'C1', 'C2', 'C3', 'C4', 'C5', 'C6', 'C7', 'C8', 'C9', 'C10', 'C11', 'C12', 'C13',
    'D1', 'D2', 'D3', 'D4', 'D5', 'D6', 'D7', 'D8', 'D9', 'D10', 'D11', 'D12', 'D13', 'E1', 'E2'];

    if(roles.indexOf(role) > -1) {
      items.push({
        title: 'Dashboard',
        route: '/dashboard',
        icon: 'assessment'
      });
    }
    if(roles.indexOf(role) > -1) {
      items.push({
        title: 'Member Information',
        route: '/user-information/user-information',
        icon: 'person',
      });
    }
    if(role == 'A1' || role == 'B1' || role == 'C1' || role == 'D1') {
      items.push({
        title: 'Role Management',
        route: '/role-management/role-management',
        icon: 'swap_vert',
      });
    }
    if(roles.indexOf(role) > -1) {
      items.push({
        title: 'Calendar of Events',
        route: '/calendar-of-events/calendar-of-events',
        icon: 'date_range',
      });
    }
    if(roles.indexOf(role) > -1) {
      items.push({
        title: 'Document Management',
        route: '/document-management/document-management',
        icon: 'folder_special',
      });
    }
    if(roles.indexOf(role) > -1) {
      items.push({
        title: 'Communication',
        route: '/communication/communication',
        icon: 'chat',
      });
    }
    if(roles.indexOf(role) > -1) {
      items.push({
        title: 'Courses',
        route: '/courses/courses',
        icon: 'school',
      });
    }
    if(roles.indexOf(role) > -1) {
      items.push({
        title: 'Protea Colors',
        route: '/protea-colors/protea-colors',
        icon: 'spa',
      });
    }
    if(roles.indexOf(role) > -1) {
      items.push({
        title: 'Reports',
        route: '/reporting/reporting',
        icon: 'rate_review',
      });
    }
    if(roles.indexOf(role) > -1) {
      items.push({
        title: 'Clothes Ordering',
        route: '/clothes-order/clothes-order',
        icon: 'shopping_cart',
      });
    }
    if(roles.indexOf(role) > -1) {
      items.push({
        title: 'Payments',
        route: '/payments/payments',
        icon: 'credit_card',
      });
    }
    if(roles.indexOf(role) > -1) {
      items.push({
        title: 'Membership Card',
        route: '/membership-cards/membership-cards',
        icon: 'contact_mail',
      });
    }
    if(roles.indexOf(role) > -1) {
      items.push({
        title: 'Mobile App',
        route: '/mobile-app/mobile-app',
        icon: 'phone_iphone',
      });
    }

    return items;
  }
}
