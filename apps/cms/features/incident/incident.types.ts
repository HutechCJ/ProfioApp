import { OrderHistory } from '../orderHistory/orderHistory.types';

export type Incident = {
  id: string;
  description: Nullable<string>;
  status: IncidentStatus;
  time: Nullable<string>;
  orderHistory: Nullable<OrderHistory>;
};

export enum IncidentStatus {
  InProgress,
  Resolved,
  Closed,
}

export type CreateIncidentData = Omit<Incident, 'id'>;

export type UpdateIncidentData = Incident;
