import { useMutation } from '@tanstack/react-query';
import customerApi from './customer.service';

const useDeleteCustomer = () => {
  return useMutation({
    mutationFn: (id: string) => customerApi.deleteCustomer(id),
  });
};

export default useDeleteCustomer;
