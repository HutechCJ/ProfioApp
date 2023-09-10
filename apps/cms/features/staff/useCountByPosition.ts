'use client';
import { useQuery } from '@tanstack/react-query';
import staffApi from './staff.service';

const useCountByPosition = () => {
  const queryData = useQuery([`users/count`], {
    queryFn: () => staffApi.countByPosition(),
    keepPreviousData: true,
  });

  return queryData;
};
export default useCountByPosition;
