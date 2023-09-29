import { useMutation } from '@tanstack/react-query';
import orderApi from './order.service';
import { EmailOrderData } from './order.types';

const useSenderEmailOrder = () => {
  return useMutation({
    mutationFn: (data: EmailOrderData) => orderApi.senderEmailOrder(data),
  });
};

export default useSenderEmailOrder;
