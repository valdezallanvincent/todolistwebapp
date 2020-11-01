import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/shared/services/user.service';
import { AddUserDialog,AddUserData } from 'src/app/shared/modal/adduser.dialog.component'
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

   users : any;
  constructor(private userService: UserService, private dialog: MatDialog) { }

  async ngOnInit() {
    await this.getUserList()
  }
  
  openAddUserModal(){
    const dialogRef = this.dialog.open<AddUserDialog, AddUserData, AddUserData>(AddUserDialog, {
      width: '300px',
      data: { userName:'',password:'', fullName:'' }
    });

    return dialogRef.afterClosed().subscribe(async result => {

      if (!result) {
        return;
      }

      await this.userService.addUser(result);
      await this.getUserList();
    });
  }

  async getUserList(){
    this.users = await this.userService.getUserList();
  }
}
interface User {
  fullName: string;
  userName: string;
  password: string;
}
