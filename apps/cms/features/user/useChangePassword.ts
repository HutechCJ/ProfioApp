import StoreKeys from '@/common/constants/storekeys';
import useLocalStorage from '@/common/hooks/useLocalStorage';
import { useMutation } from '@tanstack/react-query';
import userApi from './user.service';
import { ChangePassword } from './user.types';

const useChangePassword = () => {
  const { set } = useLocalStorage();
  return useMutation({
    mutationFn: (data: ChangePassword) => userApi.changePassword(data),
    onSuccess({ data }, variables, context) {
      set(StoreKeys.ACCESS_TOKEN, data.token);
    },
  });
};

export default useChangePassword;
