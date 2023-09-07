export type Order = {
    id: string
    startedDate: string
    expectedDeliveryTime: string
    status: string
    destinationAddress: Address
    destinationZipCode: string
    note: string
    distance: number
    customerId: string
}

export type Address = {
    street: string
    ward: string
    city: string
    province: string
    zipCode: string
}
