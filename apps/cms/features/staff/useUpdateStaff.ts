import { useMutation } from '@tanstack/react-query';
import staffApi from './staff.service';

const useUpdateStaff = () => {
  return useMutation({
    mutationFn: (id: string) => staffApi.updateStaff(id),
  });
};

export default useUpdateStaff;
