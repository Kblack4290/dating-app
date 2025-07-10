import { Component, input, Input, signal } from '@angular/core';
import { Register } from "../accounts/register/register";
import { User } from '../../types/userTypes';

@Component({
  selector: 'app-home',
  imports: [Register],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  protected registerMode = signal(false);

  showRegistered(value:boolean) {
    this.registerMode.set(value);
  }
}
