import { useMutation } from '@tanstack/react-query';
import orderApi from './order.service';
import { UpdateOrderData } from './order.types';

const useUpdateOrder = () => {
  return useMutation({
    mutationFn: (data: UpdateOrderData) => orderApi.updateOrder(data),
  });
};

export default useUpdateOrder;
