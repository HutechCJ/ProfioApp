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

export type CreateHubData = Omit<Hub, 'id' | 'staff'> & {
  staffId: string;
};

export type UpdateHubData = Omit<Hub, 'staff'> & {
  staffId: string;
};
