import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isAdmin = false;

  constructor(private userService: UserService,
    private router: Router){

  }

  async ngOnInit() {
    let user = this.userService.getUser();
    if (user.role_name.indexOf("Admin") !== -1){
      this.isAdmin = true;
    }
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout(){
    this.userService.logout();
    this.router.navigate(['/login']);
  }
}
