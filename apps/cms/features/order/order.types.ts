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

export type CreateOrderData = Omit<
  Order,
  'id' | 'status' | 'customer' | 'destinationAddress'
> & {
  status: string;
  street: string;
  ward: string;
  city: string;
  province: string;
  zipCode: string;
  customerId: string;
};

export type UpdateOrderData = Omit<
  Order,
  'status' | 'customer' | 'destinationAddress'
> & {
  status: string;
  street: string;
  ward: string;
  city: string;
  province: string;
  zipCode: string;
  customerId: string;
};
