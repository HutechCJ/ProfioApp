'use client'
import { useQuery } from '@tanstack/react-query'
import incidentApi from './incident.service'
import { getPagingQueryString } from '@/common/utils/string'

const useGetIncidents = (options?: Partial<PagingOptions>) => {
    const queryData = useQuery(
        [`incidents/get${options ? `?${getPagingQueryString(options)}` : ''}`],
        {
            queryFn: () => incidentApi.getIncidents(options),
            // keepPreviousData: true,
        }
    )

    return queryData
}
export default useGetIncidents
