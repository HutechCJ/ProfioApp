import { Customer } from '../customer/customer.types';

export type Order = {
  id: string;
  startedDate: string;
  expectedDeliveryTime: string;
  status: OrderStatus;
  destinationAddress: Nullable<Address>;
  destinationZipCode: string;
  note: string;
  distance: number;
  customer: Nullable<Customer>;
};

export enum OrderStatus {
  Pending,
  InProgress,
  Completed,
  Received,
  Cancelled,
}

export type CreateOrderData = Omit<Order, 'id' | 'customer'> & {
  customerId: string;
};
