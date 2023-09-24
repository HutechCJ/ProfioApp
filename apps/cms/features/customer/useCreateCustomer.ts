import { useMutation } from '@tanstack/react-query';
import customerApi from './customer.service';
import { CreateCustomerData } from './customer.types';

const useCreateCustomer = () => {
  return useMutation({
    mutationFn: (data: CreateCustomerData) => customerApi.createCustomer(data),
  });
};

export default useCreateCustomer;
