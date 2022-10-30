import { Injectable } from '@angular/core';
import { MenuItem } from './menu-item';
import { ActivatedRoute } from '@angular/router';

@Injectable()
export class MenuService {

  constructor(private router: ActivatedRoute) { }

  getMenuItems(): MenuItem[] {

    const role = localStorage.getItem('role');
    const items: MenuItem[] = [];

    switch (role) {
      case 'A1':
        items.push({
          title: 'Dashboard',
          route: '/dashboard',
          icon: 'assessment'
        });
        items.push({
          title: 'Member Information',
          route: '/user-information/user-information',
          icon: 'person',
        });
        items.push({
          title: 'Calendar of Events',
          route: '/calendar-of-events/calendar-of-events',
          icon: 'date_range',
        });
        items.push({
          title: 'Document Management',
          route: '/document-management/document-management',
          icon: 'folder_special',
        });
        items.push({
          title: 'Communication',
          route: '/communication/communication',
          icon: 'chat',
        });
        items.push({
          title: 'Courses',
          route: '/courses/courses',
          icon: 'school',
        });
        items.push({
          title: 'Protea Colors',
          route: '/protea-colors/protea-colors',
          icon: 'spa',
        });
        items.push({
          title: 'Reports',
          route: '/reporting/reporting',
          icon: 'rate_review',
        });
        items.push({
          title: 'Clothes Ordering',
          route: '/clothes-order/clothes-order',
          icon: 'shopping_cart',
        });
        items.push({
          title: 'Payments',
          route: '/payments/payments',
          icon: 'credit_card',
        });
        items.push({
          title: 'Membership Card',
          route: '/membership-cards/membership-cards',
          icon: 'contact_mail',
        });
        items.push({
          title: 'Mobile App',
          route: '/mobile-app/mobile-app',
          icon: 'phone_iphone',
        });
        break;

        case 'E0':
        items.push({
          title: 'Dashboard',
          route: '/dashboard',
          icon: 'assessment'
        });
        items.push({
          title: 'Member Information',
          route: '/user-information/user-information',
          icon: 'person',
        });
        items.push({
          title: 'Calendar of Events',
          route: '/calendar-of-events/calendar-of-events',
          icon: 'date_range',
        });
        items.push({
          title: 'Document Management',
          route: '/document-management/document-management',
          icon: 'folder_special',
        });
        items.push({
          title: 'Communication',
          route: '/communication/communication',
          icon: 'chat',
        });
        items.push({
          title: 'Courses',
          route: '/courses/courses',
          icon: 'school',
        });
        items.push({
          title: 'Protea Colors',
          route: '/protea-colors/protea-colors',
          icon: 'spa',
        });
        items.push({
          title: 'Reports',
          route: '/reporting/reporting',
          icon: 'rate_review',
        });
        items.push({
          title: 'Clothes Ordering',
          route: '/clothes-order/clothes-order',
          icon: 'shopping_cart',
        });
        items.push({
          title: 'Payments',
          route: '/payments/payments',
          icon: 'credit_card',
        });
        items.push({
          title: 'Membership Card',
          route: '/membership-cards/membership-cards',
          icon: 'contact_mail',
        });
        items.push({
          title: 'Mobile App',
          route: '/mobile-app/mobile-app',
          icon: 'phone_iphone',
        });
        break;
      default:

        break;
    }



    return items;
  }
}
