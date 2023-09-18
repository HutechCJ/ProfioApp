import { useMutation } from '@tanstack/react-query';
import vehicleApi from './vehicle.service';
import { CreateVehicleData } from './vehicle.types';

const useCreateVehicle = () => {
  return useMutation({
    mutationFn: (data: CreateVehicleData) => vehicleApi.createVehicle(data),
  });
};

export default useCreateVehicle;
