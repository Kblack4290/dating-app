import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountsService } from '../_services/accounts.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  private accountService = inject(AccountsService);
  private toastr = inject(ToastrService);
  cancelRegister = output<boolean>();
  model: any = {};

  register() {
    this.accountService.register(this.model).subscribe({
      next: (response: any) => {
        console.log(response);
        this.cancel();
      },
      error: (error: any) => this.toastr.error(error.error),
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
