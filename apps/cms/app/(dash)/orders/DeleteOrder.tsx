'use client';

import React from 'react';

import OrderForm from './OrderForm';
import useGetOrder from '@/features/order/useGetOrder';
import useDeleteOrder from '@/features/order/useDeleteOrder';

interface DeleteOrderProps {
  onSuccess: () => void;
  params: { orderId: string };
}

const DeleteOrder: React.FC<DeleteOrderProps> = ({ onSuccess, params }) => {
  const {
    data: orderApiRes,
    isLoading,
    isError,
    refetch,
  } = useGetOrder(params.orderId);
  const {
    mutate: deleteOrder,
    error,
    isError: isErrorDeleteOrder,
    isSuccess,
  } = useDeleteOrder();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (isError || !orderApiRes) {
    return <div>There was an error!</div>;
  }

  const handleDeleteOrder = (id: string) => {
    deleteOrder(id);
  };

  return (
    <OrderForm
      onSubmit={() => {
        handleDeleteOrder(params.orderId);
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
      isError={isErrorDeleteOrder}
      isSuccess={isSuccess}
      onSuccess={onSuccess}
      labelButton="DELETE"
    />
  );
};

export default DeleteOrder;
