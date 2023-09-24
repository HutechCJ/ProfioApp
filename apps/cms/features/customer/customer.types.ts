export type Customer = {
  id: string;
  name: string;
  phone: string;
  email: string;
  gender: Gender;
  address: Address;
};

export enum Gender {
  Male,
  Female,
  Other,
}

export type CreateCustomerData = Omit<Customer, 'id' | 'gender'> & {
  gender: string;
};

export type UpdateCustomerData = Omit<Customer, 'gender'> & {
  gender: string;
};
