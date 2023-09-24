'use client';
import { useQuery } from '@tanstack/react-query';
import customerApi from './customer.service';

const useGetCustomer = (id: string) => {
  const queryData = useQuery([`users/get/${id}`], {
    queryFn: () => customerApi.getCustomerById(id),
    keepPreviousData: true,
  });

  return queryData;
};
export default useGetCustomer;
