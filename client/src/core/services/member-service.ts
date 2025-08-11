import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../../types/member';

@Injectable({
  providedIn: 'root',
})
export class MemberService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;
  private member: Member | null = null;

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'users');
  }

  getMember(id: string){
      return this.http.get<Member>(this.baseUrl + 'users/'+id);
  }

  calculateAge(member: Member) {
    const timeDiff = Math.abs(Date.now() - new Date(member.dateOfBirth).getTime());
    const age = Math.floor((timeDiff / (1000 * 3600 * 24))/365.25);
    return age;
  }

}
