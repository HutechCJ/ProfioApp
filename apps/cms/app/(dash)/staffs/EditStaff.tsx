'use client';

import React from 'react';

import useUpdateStaff from '@/features/staff/useUpdateStaff';
import StaffForm from './StaffForm';
import useGetStaff from '@/features/staff/useGetStaff';
import { StaffPosition } from '@/features/staff/staff.types';

interface EditStaffProps {
  onSuccess: () => void;
  params: { staffId: string };
}

const EditStaff: React.FC<EditStaffProps> = ({ onSuccess, params }) => {
  const {
    data: staffApiRes,
    isLoading,
    isError,
    refetch,
  } = useGetStaff(params.staffId);
  const {
    mutate: updateStaff,
    error,
    isError: isErrorUpdateStaff,
    isSuccess,
  } = useUpdateStaff();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (isError || !staffApiRes) {
    return <div>There was an error!</div>;
  }

  const handleEditStaff = (staffId: string, data: FormData) => {
    updateStaff({
      id: staffId,
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
      onSubmit={(data: FormData) => {
        handleEditStaff(params.staffId, data);
        refetch();
      }}
      initialValue={{
        name: staffApiRes.data.name,
        phone: staffApiRes.data.phone,
        position: staffApiRes.data.position,
      }}
      error={error}
      isError={isErrorUpdateStaff}
      isSuccess={isSuccess}
      onSuccess={onSuccess}
      labelButton="UPDATE"
    />
  );
};

export default EditStaff;
