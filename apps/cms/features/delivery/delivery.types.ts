import { Order } from '../order/order.types';
import { Vehicle } from '../vehicle/vehicle.types';

export type Delivery = {
  id: 'string';
  deliveryDate: string;
  order: Order;
  vehicle: Vehicle;
};

export type CreateDeliveryData = Omit<Delivery, 'id' | 'order' | 'vehicle'> & {
  orderId: string;
  vehicleId: string;
};

export type UpdateDeliveryData = Omit<Delivery, 'order' | 'vehicle'> & {
  orderId: string;
  vehicleId: string;
};
