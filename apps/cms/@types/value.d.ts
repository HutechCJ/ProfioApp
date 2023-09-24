type Address = {
  street: string;
  ward: string;
  city: string;
  province: string;
  zipCode: string;
};

type Coordination = {
  latitude: number;
  longitude: number;
};

type VehicleLocation = Coordination & { id: string };
