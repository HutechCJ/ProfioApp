'use client';

import React from 'react';

import { Gender } from '@/features/customer/customer.types';
import useCreateCustomer from '@/features/customer/useCreateCustomer';
import CustomerForm from './CustomerForm';

interface AddCustomerProps {
  onSuccess: () => void;
}

const AddCustomer: React.FC<AddCustomerProps> = ({ onSuccess }) => {
  const {
    mutate: createCustomer,
    error,
    isError,
    isSuccess,
  } = useCreateCustomer();

  const handleAddCustomer = (data: FormData) => {
    createCustomer({
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
      onSubmit={handleAddCustomer}
      initialValue={{}}
      error={error}
      isError={isError}
      isSuccess={isSuccess}
      onSuccess={onSuccess}
      labelButton="SUBMIT"
    />
  );
};

export default AddCustomer;
