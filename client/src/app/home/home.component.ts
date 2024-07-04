import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: any;
  public userString: any;

  constructor() { }

  ngOnInit(): void {
    this.userString = localStorage.getItem('user');
    console.log(this.userString);
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }



  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

}
