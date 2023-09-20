'use client';

import React from 'react';

import { OrderStatus } from '@/features/order/order.types';
import useCreateOrder from '@/features/order/useCreateOrder';
import OrderForm from './OrderForm';

interface AddOrderProps {
  onSuccess: () => void;
}

const AddOrder: React.FC<AddOrderProps> = ({ onSuccess }) => {
  const { mutate: createOrder, error, isError, isSuccess } = useCreateOrder();

  const handleAddOrder = (data: FormData) => {
    createOrder({
      startedDate: data.get('startedDate') as string,
      expectedDeliveryTime: data.get('expectedDeliveryTime') as string,
      status:
        OrderStatus[parseInt(data.get('status') as string) as OrderStatus],
      street: data.get('street') as string,
      ward: data.get('ward') as string,
      city: data.get('city') as string,
      province: data.get('province') as string,
      zipCode: data.get('zipCode') as string,
      destinationZipCode: data.get('destinationZipCode') as string,
      note: data.get('note') as string,
      distance: parseInt(data.get('distance') as string),
      customerId: data.get('customerId') as string,
    });
  };

  return (
    <OrderForm
      onSubmit={handleAddOrder}
      initialValue={{}}
      error={error}
      isError={isError}
      isSuccess={isSuccess}
      onSuccess={onSuccess}
      labelButton="SUBMIT"
    />
  );
};

export default AddOrder;
