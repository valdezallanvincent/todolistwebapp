import { Component, Inject, AfterViewInit } from '@angular/core';
import { FormControl, Validators, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'dialogservice-dialog',
    templateUrl: './adduser.dialog.component.html',
    styleUrls: ['./adduser.dialog.component.css']
})
export class AddUserDialog implements AfterViewInit {

  pageLoaded = false;
  submitted = false;
  public addUserForm: FormGroup;

    constructor(public  formBuilder: FormBuilder,
        public dialogRef: MatDialogRef<AddUserDialog, AddUserData>,
        @Inject(MAT_DIALOG_DATA) public data: any) {
            
        }

    ngAfterViewInit(){
      setTimeout(()=>{  
        this.addUserForm = this.formBuilder.group({
         fullName: ['', Validators.required],
         userName: ['', Validators.required],
         password: ['', Validators.required]
         })
         this.pageLoaded=true;
      }, 0);
    }    
    
    get fval() { 
        if(this.pageLoaded){
            return this.addUserForm.controls; 
        }
    }
    onCancelClick(): void {
        this.dialogRef.close(undefined);
    }

    onConfirmClick(): void {
        this.submitted = true;
        if (this.addUserForm.invalid)
        {
            return;
        }
        this.data.userName = this.addUserForm.value.userName;
        this.data.password = this.addUserForm.value.password;
        this.data.fullName = this.addUserForm.value.fullName;
        
        this.dialogRef.close(this.data);
    }
}

export class AddUserData {
    fullName: string;
    userName: string;
    password: string;
}
