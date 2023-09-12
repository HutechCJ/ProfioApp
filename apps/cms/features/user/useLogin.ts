import StoreKeys from '@/common/constants/storekeys';
import useLocalStorage from '@/common/hooks/useLocalStorage';
import { useMutation } from '@tanstack/react-query';
import userApi from './user.service';
import { LoginRequest } from './user.types';

const useLogin = () => {
  const { set } = useLocalStorage();
  return useMutation({
    mutationFn: (data: LoginRequest) => userApi.login(data),
    onSuccess({ data }, variables, context) {
      set(StoreKeys.ACCESS_TOKEN, data.token);
    },
  });
};

export default useLogin;
