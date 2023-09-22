import { useMutation } from '@tanstack/react-query';
import orderApi from './order.service';

const useDeleteOrder = () => {
  return useMutation({
    mutationFn: (id: string) => orderApi.deleteOrder(id),
  });
};

export default useDeleteOrder;
