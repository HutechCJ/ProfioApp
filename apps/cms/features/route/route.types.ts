import { Hub } from '../hub/hub.types'

export type Route = {
    id: string
    distance: number
    startHub: Hub
    endHub: Hub
}

export type CreateRouteData = Omit<Route, 'id' | 'startHub' | 'endHub'> & {
    startHubId: string
    endHubId: string
}

export type UpdateRouteData = Omit<Route, 'startHub' | 'endHub'> & {
    startHubId: string
    endHubId: string
}
