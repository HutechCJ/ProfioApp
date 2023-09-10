'use client';
import { useQuery } from '@tanstack/react-query';
import staffApi from './staff.service';

const useGetStaff = (id: string) => {
  const queryData = useQuery([`users/get/${id}`], {
    queryFn: () => staffApi.getStaffById(id),
    keepPreviousData: true,
  });

  return queryData;
};
export default useGetStaff;
