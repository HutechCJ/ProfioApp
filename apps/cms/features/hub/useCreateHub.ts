import { useMutation } from '@tanstack/react-query';
import hubApi from './hub.service';
import { CreateHubData } from './hub.types';

const useCreateHub = () => {
  return useMutation({
    mutationFn: (data: CreateHubData) => hubApi.createHub(data),
  });
};

export default useCreateHub;
