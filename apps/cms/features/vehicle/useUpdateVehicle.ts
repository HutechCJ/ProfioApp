import { useMutation } from '@tanstack/react-query';
import vehicleApi from './vehicle.service';
import { UpdateVehicleData } from './vehicle.types';

const useUpdateVehicle = () => {
  return useMutation({
    mutationFn: (data: UpdateVehicleData) => vehicleApi.updateVehicle(data),
  });
};

export default useUpdateVehicle;
