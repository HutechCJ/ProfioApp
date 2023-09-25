import { useMutation } from '@tanstack/react-query';
import hubApi from './hub.service';
import { UpdateHubData } from './hub.types';

const useUpdateHub = () => {
  return useMutation({
    mutationFn: (data: UpdateHubData) => hubApi.updateHub(data),
  });
};

export default useUpdateHub;
