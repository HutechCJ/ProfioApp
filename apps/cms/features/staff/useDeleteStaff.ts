import { useMutation } from '@tanstack/react-query';
import staffApi from './staff.service';

const useDeleteStaff = () => {
  return useMutation({
    mutationFn: (id: string) => staffApi.deleteStaff(id),
  });
};

export default useDeleteStaff;
