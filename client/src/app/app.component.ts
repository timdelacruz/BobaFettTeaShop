import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Boba Fett Tea Shop';
  customers: any;

  constructor(private http: HttpClient) {}

  // tslint:disable-next-line: typedef
  ngOnInit() {
    // tslint:disable-next-line: no-unused-expression
    this.getCustomers();
  }

  // tslint:disable-next-line: typedef
  getCustomers() {
    this.http.get('https://localhost:5001/api/customer').subscribe(response => {
      this.customers = response;
    }, error => {
      console.log(error);
    });
  }
}
