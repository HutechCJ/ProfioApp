export type Order = {
  id: string;
  startedDate: string;
  expectedDeliveryTime: string;
  status: OrderStatus;
  destinationAddress: Address | null;
  destinationZipCode: string;
  note: string;
  distance: number;
  customerId: string;
};

export enum OrderStatus {
  Pending,
  InProgress,
  Completed,
  Cancelled,
}
