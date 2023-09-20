'use client';

import React from 'react';

import useUpdateOrder from '@/features/order/useUpdateOrder';
import OrderForm from './OrderForm';
import useGetOrder from '@/features/order/useGetOrder';
import { OrderStatus } from '@/features/order/order.types';

interface EditOrderProps {
  onSuccess: () => void;
  params: { orderId: string };
}

const EditOrder: React.FC<EditOrderProps> = ({ onSuccess, params }) => {
  const {
    data: orderApiRes,
    isLoading,
    isError,
    refetch,
  } = useGetOrder(params.orderId);
  const {
    mutate: updateOrder,
    error,
    isError: isErrorUpdateOrder,
    isSuccess,
  } = useUpdateOrder();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (isError || !orderApiRes) {
    return <div>There was an error!</div>;
  }

  const handleEditOrder = (orderId: string, data: FormData) => {
    updateOrder({
      id: orderId,
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
      onSubmit={(data: FormData) => {
        handleEditOrder(params.orderId, data);
        refetch();
      }}
      initialValue={{
        startedDate: orderApiRes.data.startedDate,
        expectedDeliveryTime: orderApiRes.data.expectedDeliveryTime,
        status: orderApiRes.data.status,
        street: orderApiRes.data.destinationAddress?.street,
        ward: orderApiRes.data.destinationAddress?.ward,
        city: orderApiRes.data.destinationAddress?.city,
        province: orderApiRes.data.destinationAddress?.province,
        zipCode: orderApiRes.data.destinationAddress?.zipCode,
        destinationZipCode: orderApiRes.data.destinationZipCode,
        note: orderApiRes.data.note,
        distance: orderApiRes.data.distance,
        customerId: orderApiRes.data.customer?.id,
      }}
      error={error}
      isError={isErrorUpdateOrder}
      isSuccess={isSuccess}
      onSuccess={onSuccess}
      labelButton="UPDATE"
    />
  );
};

export default EditOrder;
