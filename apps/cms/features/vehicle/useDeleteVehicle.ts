import { useMutation } from '@tanstack/react-query';
import vehicleApi from './vehicle.service';

const useDeleteVehicle = () => {
  return useMutation({
    mutationFn: (id: string) => vehicleApi.deleteVehicle(id),
  });
};

export default useDeleteVehicle;
