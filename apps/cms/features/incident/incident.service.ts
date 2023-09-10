'use client'

import HttpService from '@/common/services/http.service'
import { CreateIncidentData, Incident, UpdateIncidentData } from './incident.types'
import { getPagingQueryString } from '@/common/utils/string'

class IncidentApiService extends HttpService {
    getIncidents(options?: Partial<PagingOptions>) {
        const query = options ? getPagingQueryString(options) : ''
        return this.get<Paging<Incident>>(`/incidents/${query}`)
    }

    getIncidentById(id: string) {
        return this.get<Incident>(`/incidents/${id}`)
    }

    createIncident(data: CreateIncidentData) {
        return this.post<Incident>(`/incidents`, data)
    }

    updateIncident(id: string, data: UpdateIncidentData) {
        return this.put(`/incidents/${id}`, data)
    }

    deleteIncident(id: string) {
        return this.delete<Incident>(`/incidents/${id}`)
    }
}

const incidentApi = new IncidentApiService()

export default incidentApi
