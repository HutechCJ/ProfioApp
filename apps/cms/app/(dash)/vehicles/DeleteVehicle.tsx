'use client';

import React from 'react';

import VehicleForm from './VehicleForm';
import useGetVehicle from '@/features/vehicle/useGetVehicle';
import useDeleteVehicle from '@/features/vehicle/useDeleteVehicle';

interface DeleteVehicleProps {
  onSuccess: () => void;
  params: { vehicleId: string };
}

const DeleteVehicle: React.FC<DeleteVehicleProps> = ({ onSuccess, params }) => {
  const {
    data: vehicleApiRes,
    isLoading,
    isError,
    refetch,
  } = useGetVehicle(params.vehicleId);
  const {
    mutate: deleteVehicle,
    error,
    isError: isErrorDeleteVehicle,
    isSuccess,
  } = useDeleteVehicle();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (isError || !vehicleApiRes) {
    return <div>There was an error!</div>;
  }

  const handleDeleteVehicle = (id: string) => {
    deleteVehicle(id);
  };

  return (
    <VehicleForm
      onSubmit={() => {
        handleDeleteVehicle(params.vehicleId);
        refetch();
      }}
      initialValue={{
        zipCodeCurrent: vehicleApiRes.data.zipCodeCurrent,
        licensePlate: vehicleApiRes.data.licensePlate,
        type: vehicleApiRes.data.type,
        status: vehicleApiRes.data.status,
        staffId: vehicleApiRes.data.staff?.id,
      }}
      error={error}
      isError={isErrorDeleteVehicle}
      isSuccess={isSuccess}
      onSuccess={onSuccess}
      labelButton="DELETE"
    />
  );
};

export default DeleteVehicle;
