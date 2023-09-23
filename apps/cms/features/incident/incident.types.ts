import { Delivery } from "../delivery/delivery.types";

export type Incident = {
  id: string;
  description: Nullable<string>;
  status: IncidentStatus;
  time: Nullable<string>;
  delivery: Nullable<Delivery>;
};

export enum IncidentStatus {
  InProgress,
  Resolved,
  Closed,
}

export type CreateIncidentData = Omit<Incident, 'id' | 'delivery'>;

export type UpdateIncidentData = Incident;
