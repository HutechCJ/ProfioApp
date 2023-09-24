import { useMutation } from '@tanstack/react-query';
import customerApi from './customer.service';
import { UpdateCustomerData } from './customer.types';

const useUpdateCustomer = () => {
  return useMutation({
    mutationFn: (data: UpdateCustomerData) => customerApi.updateCustomer(data),
  });
};

export default useUpdateCustomer;
