'use client';

import React from 'react';

import StaffForm from './StaffForm';
import useGetStaff from '@/features/staff/useGetStaff';
import useDeleteStaff from '@/features/staff/useDeleteStaff';

interface DeleteStaffProps {
  onSuccess: () => void;
  params: { staffId: string };
}

const DeleteStaff: React.FC<DeleteStaffProps> = ({ onSuccess, params }) => {
  const {
    data: staffApiRes,
    isLoading,
    isError,
    refetch,
  } = useGetStaff(params.staffId);
  const {
    mutate: deleteStaff,
    error,
    isError: isErrorDeleteStaff,
    isSuccess,
  } = useDeleteStaff();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (isError || !staffApiRes) {
    return <div>There was an error!</div>;
  }

  const handleDeleteStaff = (id: string) => {
    deleteStaff(id);
  };

  return (
    <StaffForm
      onSubmit={() => {
        handleDeleteStaff(params.staffId);
        refetch();
      }}
      initialValue={{
        name: staffApiRes.data.name,
        phone: staffApiRes.data.phone,
        position: staffApiRes.data.position,
      }}
      error={error}
      isError={isErrorDeleteStaff}
      isSuccess={isSuccess}
      onSuccess={onSuccess}
      labelButton="DELETE"
    />
  );
};

export default DeleteStaff;
