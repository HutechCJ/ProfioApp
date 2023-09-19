'use client';
import { useQuery } from '@tanstack/react-query';
import vehicleApi from './vehicle.service';

const useGetVehicle = (id: string) => {
  const queryData = useQuery([`users/get/${id}`], {
    queryFn: () => vehicleApi.getVehicleById(id),
    keepPreviousData: true,
  });

  return queryData;
};
export default useGetVehicle;
