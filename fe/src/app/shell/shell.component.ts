import { Component, OnInit,Directive,Input, ViewChild } from '@angular/core';
import { MenuService } from './menu.service';
import { MenuItem } from './menu-item';
import { Observable } from 'rxjs';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { map } from 'rxjs/operators';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { MatDialog, MatSidenav } from '@angular/material';
import { Router } from '@angular/router';
import { SettingsComponent } from '../administration/settings/settings.component';

@Component({
  selector: 'app-shell',
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.scss']
})

export class ShellComponent implements OnInit {

  menuItems: MenuItem[] = [];
  @ViewChild('sidenav', {static: false}) sidenav: MatSidenav;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
  .pipe(
    map(result => result.matches)
  );
  constructor(private menuService: MenuService,  private breakpointObserver: BreakpointObserver, private router: Router, public dialog: MatDialog) { }

  ngOnInit() {
    this.menuItems = this.menuService.getMenuItems();
  }

  signOut() {
    this.router.navigate(['']);
    localStorage.clear();
  }

  settingsPage() {
    //this.router.navigate(['/settings']);
    const dialogRef = this.dialog.open(SettingsComponent, {
      height: '400px',
      width: '600px',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  onClick() {
      console.log(`isOpen: ${this.sidenav.opened}`);
  }
}
