'use client'

import HttpService from '@/common/services/http.service'
import { Vehicle, CreateVehicleData, UpdateVehicleData } from './vehicle.types'
import { getPagingQueryString } from '@/common/utils/string'

class VehicleApiService extends HttpService {
    getVehicles(options?: Partial<PagingOptions>) {
        const query = options ? getPagingQueryString(options) : ''
        return this.get<Paging<Vehicle>>(`/vehicles/${query}`)
    }

    getVehicleById(id: string) {
        return this.get<Vehicle>(`/vehicles/${id}`)
    }

    createVehicle(data: CreateVehicleData) {
        return this.post<Vehicle>(`/vehicles`, data)
    }

    updateVehicle(id: string, data: UpdateVehicleData) {
        return this.put(`/vehicles/${id}`, data)
    }

    deleteVehicle(id: string) {
        return this.delete<Vehicle>(`/vehicles/${id}`)
    }
}

const vehicleApi = new VehicleApiService()

export default vehicleApi
