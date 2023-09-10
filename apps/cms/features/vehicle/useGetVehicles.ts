'use client';
import { useQuery } from '@tanstack/react-query';
import vehicleApi from './vehicle.service';
import { getPagingQueryString } from '@/common/utils/string';

const useGetVehicles = (options?: Partial<PagingOptions>) => {
  const queryData = useQuery(
    [`vehicles/get${options ? `?${getPagingQueryString(options)}` : ''}`],
    {
      queryFn: () => vehicleApi.getVehicles(options),
      // keepPreviousData: true,
    },
  );

  return queryData;
};
export default useGetVehicles;
