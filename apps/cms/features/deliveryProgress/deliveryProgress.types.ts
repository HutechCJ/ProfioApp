import { Order } from '../order/order.types';

export type DeliveryProgress = {
  id: string;
  currentLocation: Location;
  percentComplete: number;
  estimatedTimeRemaining: string;
  lastUpdated: string;
  order: Order;
};

export type CreateDeliveryProgressData = Omit<
  DeliveryProgress,
  'id' | 'order' | 'estimatedTimeRemaining'
> & {
  orderId: string;
};

export type UpdateDeliveryProgressData = Omit<
  DeliveryProgress,
  'order' | 'estimatedTimeRemaining'
> & {
  orderId: string;
};
