'use client';

import React from 'react';

import useUpdateCustomer from '@/features/customer/useUpdateCustomer';
import CustomerForm from './CustomerForm';
import useGetCustomer from '@/features/customer/useGetCustomer';
import { Gender } from '@/features/customer/customer.types';

interface EditCustomerProps {
  onSuccess: () => void;
  params: { customerId: string };
}

const EditCustomer: React.FC<EditCustomerProps> = ({ onSuccess, params }) => {
  const {
    data: customerApiRes,
    isLoading,
    isError,
    refetch,
  } = useGetCustomer(params.customerId);
  const {
    mutate: updateCustomer,
    error,
    isError: isErrorUpdateCustomer,
    isSuccess,
  } = useUpdateCustomer();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (isError || !customerApiRes) {
    return <div>There was an error!</div>;
  }

  const handleEditCustomer = (customerId: string, data: FormData) => {
    updateCustomer({
      id: customerId,
      name: data.get('name') as string,
      phone: data.get('phone') as string,
      email: data.get('email') as string,
      gender: Gender[parseInt(data.get('gender') as string) as Gender],
      address: {
        street: data.get('address.street') as string,
        ward: data.get('address.ward') as string,
        city: data.get('address.city') as string,
        province: data.get('address.province') as string,
        zipCode: data.get('address.zipCode') as string,
      },
    });
  };

  return (
    <CustomerForm
      onSubmit={(data: FormData) => {
        handleEditCustomer(params.customerId, data);
        refetch();
      }}
      initialValue={{
        name: customerApiRes.data.name,
        phone: customerApiRes.data.phone,
        email: customerApiRes.data.email,
        gender: customerApiRes.data.gender,
        address: {
          street: customerApiRes.data.address?.street,
          ward: customerApiRes.data.address?.ward,
          city: customerApiRes.data.address?.city,
          province: customerApiRes.data.address?.province,
          zipCode: customerApiRes.data.address?.zipCode,
        },
      }}
      error={error}
      isError={isErrorUpdateCustomer}
      isSuccess={isSuccess}
      onSuccess={onSuccess}
      labelButton="UPDATE"
    />
  );
};

export default EditCustomer;
