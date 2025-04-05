import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { MemberCardComponent } from "../member-card/member-card.component";
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { UserParams } from '../../_models/userParams';
import { FormsModule } from '@angular/forms';
import { ButtonsModule } from 'ngx-bootstrap/buttons';

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [MemberCardComponent, PaginationModule, FormsModule, ButtonsModule],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {
  memberService = inject(MembersService);
  userParams!: UserParams;
  genderList = [{ value: 'male', display: "Males" }, { value: "female", display: "Females" }]



  ngOnInit(): void {
    this.userParams = this.memberService.getUserParams();
    console.log('Initial userParams:', this.userParams);
    this.userParams.pageNumber = 1;
    this.loadMembers();

  }


  resetFilters() {
    this.userParams = new UserParams(null);
    this.userParams.gender = 'male'; 
    this.userParams.minAge = 18;
    this.userParams.maxAge = 99;
    this.userParams.pageNumber = 1;
    this.userParams.pageSize = 5;
    this.userParams.orderBy = 'lastActive';

    this.memberService.setUserParams(this.userParams);

    this.loadMembers();
  }
  

  loadMembers() {
    // Zapisz zmienione parametry do localStorage
    this.memberService.setUserParams(this.userParams);

    // Załaduj członków z serwisu
    this.memberService.getMembers();
  }


  pageChanged(event: any) {
    if (this.userParams.pageNumber !== event.page) {
      this.userParams.pageNumber = event.page;
      this.memberService.setUserParams(this.userParams);
      this.loadMembers();
    }
  }
}