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
       //SASACC admin
       case 'A0':
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

      //SASACC chair
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

        //SASACC Action com
      case 'A2':
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

        //SASACC Man com
      case 'A3':
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

        //Federation admin
      case 'B0':
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

          //Federation chair
      case 'B1':
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


          //Federation action com
      case 'B2':
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


          //Federation Man com
      case 'B3':
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

        //Province admin
      case 'C0':
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


        //Province chair
      case 'C1':
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


        //Province Action com
      case 'C2':
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

        //Province Man com
      case 'C3':
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

        //Club admin
      case 'D0':
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


        //Club chair
      case 'D1':
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


        //Club Action com
      case 'D2':
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


        //Club Man com
      case 'D3':
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

        //Club member
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

        //Recreational member
      case 'E1':
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
