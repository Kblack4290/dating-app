import { CanDeactivateFn } from '@angular/router';
import { MemberProfile } from '../../features/members/member-profile/member-profile';

export const preventUnsavedChangesGuard: CanDeactivateFn<MemberProfile> = (
  component,
  currentRoute,
  currentState,
  nextState
) => {
  if(component.editForm?.dirty) {
    const confirmationMessage = 'You have unsaved changes. Do you really want to leave?';
    return confirm(confirmationMessage);
  }
  return true;
};
