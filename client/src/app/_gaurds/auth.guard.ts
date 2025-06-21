import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountsService } from '../_services/accounts.service';

export const authGuard: CanActivateFn = (route, state) => {
  let accountService = inject(AccountsService);
  const toastr = inject(ToastrService);

  if (accountService.currentUser()) {
    return true;
  } else {
    toastr.error('Access Denied');
    return false;
  }
};
