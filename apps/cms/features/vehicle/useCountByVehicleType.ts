'use client';
import { useQuery } from '@tanstack/react-query';
import vehicleApi from './vehicle.service';

const useCountByVehicleType = () => {
  const queryData = useQuery([`users/count`], {
    queryFn: () => vehicleApi.countByVehicleType(),
    keepPreviousData: true,
  });

  return queryData;
};
export default useCountByVehicleType;
