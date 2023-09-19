import { Staff } from '../staff/staff.types';

export type Vehicle = {
  id: string;
  zipCodeCurrent: string;
  licensePlate: string;
  type: VehicleType;
  status: VehicleStatus;
  staff: Nullable<Staff>;
};

export enum VehicleType {
  Truck,
  Trailer,
  Van,
  Motorcycle,
}

export enum VehicleStatus {
  Idle,
  Busy,
  Offline,
}

export type CreateVehicleData = Omit<
  Vehicle,
  'id' | 'staff' | 'type' | 'status'
> & {
  staffId: string;
  type: string;
  status: string;
};

export type UpdateVehicleData = Omit<Vehicle, 'staff' | 'type' | 'status'> & {
  staffId: string;
  type: string;
  status: string;
};
