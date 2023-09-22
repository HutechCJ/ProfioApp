'use client';
import { useQuery } from '@tanstack/react-query';
import orderApi from './order.service';

const useCountByOrderStatus = () => {
  const queryData = useQuery([`users/count`], {
    queryFn: () => orderApi.countByStatus(),
    keepPreviousData: true,
  });

  return queryData;
};
export default useCountByOrderStatus;
