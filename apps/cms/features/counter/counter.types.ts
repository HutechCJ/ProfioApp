export type Counter = {
    // [key: string]: number
    customer: number
    delivery: number
    incident: number
    order: number
    staff: number
    vehicle: number
}

export type CounterEntity = keyof Counter
