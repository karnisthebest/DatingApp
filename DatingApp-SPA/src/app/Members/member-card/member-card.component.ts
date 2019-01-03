import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/User';


@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  // we will pass down user info from member-list component
  // so this is a childe of member-list component
  @Input() user: User;

  constructor() { }

  ngOnInit() {
  }

}
