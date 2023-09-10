export type Vehicle = {
    id: string
    zipCodeCurrent: string
    licensePlate: string
    type: VehicleType
    status: VehicleStatus
    staff: {
        id: string
        name: string
        phone: string
        position: string
    }
}

export enum VehicleType {
    Truck,
    Trailer,
    Van,
    Motorcycle,
}

export enum VehicleStatus {
    Idle,
    Busy,
    Offline,
}

export type CreateVehicleData = Omit<Vehicle, 'id' | 'staff'> & {
    staffId: string
}

export type UpdateVehicleData = Omit<Vehicle, 'staff'> & {
    staffId: string
}
