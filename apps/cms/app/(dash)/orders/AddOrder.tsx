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
      destinationAddress: {
        street: data.get('destinationAddress.street') as string,
        ward: data.get('destinationAddress.ward') as string,
        city: data.get('destinationAddress.city') as string,
        province: data.get('destinationAddress.province') as string,
        zipCode: data.get('destinationAddress.zipCode') as string,
      },
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
