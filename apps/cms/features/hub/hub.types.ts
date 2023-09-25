export type Hub = {
  id: string;
  zipCode: string;
  location: Nullable<Coordination>;
  status: HubStatus;
};

export enum HubStatus {
  Active,
  Inactive,
  Broken,
  UnderMaintenance,
  Full,
}

export type CreateHubData = Omit<Hub, 'id' | 'status'> & {
  status: string;
};

export type UpdateHubData = Omit<Hub, 'status'> & {
  status: string;
};
