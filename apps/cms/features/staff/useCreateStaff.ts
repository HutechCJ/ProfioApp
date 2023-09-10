import { useMutation } from '@tanstack/react-query';
import staffApi from './staff.service';
import { CreateStaffRequest } from './staff.types';

const useCreateStaff = () => {
  return useMutation({
    mutationFn: (data: CreateStaffRequest) => staffApi.createStaff(data),
  });
};

export default useCreateStaff;
