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

export type CreateOrderData = Omit<Order, 'id' | 'status' | 'customer'> & {
  status: string;
  customerId: string;
};

export type UpdateOrderData = Omit<Order, 'status' | 'customer'> & {
  status: string;
  customerId: string;
};

export type EmailOrderData = {
  id: string;
  customerName: string;
  email: string;
  phone: string;
  from: string;
  to: string;
};
