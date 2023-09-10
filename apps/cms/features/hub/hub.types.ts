export type Hub = {
    id: string
    zipCode: string
    location: Location
    status: HubStatus
}

export enum VehicleType {
    Truck,
    Trailer,
    Van,
    Motorcycle,
}

export enum HubStatus {
    Active,
    Inactive,
    Broken,
    UnderMaintenance,
    Full,
}

export type CreateHubData = Omit<Hub, 'id' | 'staff'> & {
    staffId: string
}

export type UpdateHubData = Omit<Hub, 'staff'> & {
    staffId: string
}
