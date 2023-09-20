import { useMutation } from '@tanstack/react-query';
import orderApi from './order.service';
import { CreateOrderData } from './order.types';

const useCreateOrder = () => {
  return useMutation({
    mutationFn: (data: CreateOrderData) => orderApi.createOrder(data),
  });
};

export default useCreateOrder;
