import { SweetAlertOptions } from 'sweetalert2';

export const confirmSwalOption: SweetAlertOptions = {
  title: 'Are you sure?',
  text: 'Please confirm this update!',
  icon: 'warning',
  showCancelButton: true,
  confirmButtonColor: '#007dc3',
  cancelButtonColor: '#d32f2f',
  confirmButtonText: 'Yes!',
};

export const successSwalOption: SweetAlertOptions = {
  title: 'Successfully!',
  text: 'Your data has been updated.',
  icon: 'success',
  confirmButtonColor: '#007dc3',
  confirmButtonText: 'OK',
};
