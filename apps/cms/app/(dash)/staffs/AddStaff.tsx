'use client';

import React from 'react';

import { StaffPosition } from '@/features/staff/staff.types';
import useCreateStaff from '@/features/staff/useCreateStaff';
import StaffForm from './StaffForm';

interface AddStaffProps {
  onSuccess: () => void;
}

const AddStaff: React.FC<AddStaffProps> = ({ onSuccess }) => {
  const { mutate: createStaff, error, isError, isSuccess } = useCreateStaff();

  const handleAddStaff = (data: FormData) => {
    createStaff({
      name: data.get('name') as string,
      phone: data.get('phone') as string & { length: 10 },
      position:
        StaffPosition[
          parseInt(data.get('position') as string) as StaffPosition
        ],
    });
  };

  return (
    <StaffForm
      onSubmit={handleAddStaff}
      initialValue={{}}
      error={error}
      isError={isError}
      isSuccess={isSuccess}
      onSuccess={onSuccess}
      labelButton="SUBMIT"
    />
  );
};

export default AddStaff;
