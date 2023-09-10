import { OrderHistory } from '../orderHistory/orderHistory.types'

export type Incident = {
    id: string
    description: string
    status: IncidentStatus
    time: string
    orderHistory: OrderHistory
}

export enum IncidentStatus {
    InProgress,
    Resolved,
    Closed,
}

export type CreateIncidentData = Omit<Incident, 'id'>

export type UpdateIncidentData = Incident
