'use client';
import { useQuery } from '@tanstack/react-query';
import vehicleApi from './vehicle.service';

const useCountByVehicleStatus = () => {
  const queryData = useQuery([`users/count`], {
    queryFn: () => vehicleApi.countByVehicleStatus(),
    keepPreviousData: true,
  });

  return queryData;
};
export default useCountByVehicleStatus;
