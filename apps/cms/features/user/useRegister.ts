import { useMutation } from '@tanstack/react-query';
import userApi from './user.service';
import { RegisterRequest } from './user.types';

const useRegister = () => {
  return useMutation({
    mutationFn: (data: RegisterRequest) => userApi.register(data),
  });
};

export default useRegister;
