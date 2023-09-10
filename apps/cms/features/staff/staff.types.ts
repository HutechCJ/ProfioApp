export type Staff = {
  id: string;
  name: string;
  phone: string & { length: 10 };
  position: StaffPosition;
};

export enum StaffPosition {
  Driver,
  Shipper,
}

export type CreateStaffRequest = Omit<Staff, 'id' | 'position'> & {
  position: string;
};
