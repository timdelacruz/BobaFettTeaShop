import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  login() {
    this.accountService.login(this.model).subscribe(response => {
      this.router.navigateByUrl('/order');
      console.log('Logged in succesfully');
      this.toastr.success('Logged in succesfully');
    }, error => {
      console.log('Log in failed');
      this.toastr.error();
    });
  }

  /*loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }*/

  logout() {
    this.accountService.logout();
    console.log('Logged out succesfully');
    this.router.navigateByUrl('/');
  }

}
