import { useMutation } from '@tanstack/react-query';
import staffApi from './staff.service';
import { UpdateStaffRequest } from './staff.types';

const useUpdateStaff = () => {
  return useMutation({
    mutationFn: (data: UpdateStaffRequest) => staffApi.updateStaff(data),
  });
};

export default useUpdateStaff;
