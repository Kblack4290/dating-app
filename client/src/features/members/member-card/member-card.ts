import { Component, input } from '@angular/core';
import { Member } from '../../../types/member';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-member-card',
  imports: [RouterLink],
  templateUrl: './member-card.html',
  styleUrl: './member-card.css',
})
export class MemberCard {
  member = input.required<Member>();

  calculateAge() {
    const timeDiff=Math.abs(Date.now()- new Date(this.member().dateOfBirth).getTime());
    const age = Math.floor((timeDiff / (1000 * 3600 * 24))/365.25);
    return age;
  }
}
