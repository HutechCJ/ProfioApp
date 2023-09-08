'use client'

import { OrderStatus } from '@/features/order/order.types'
import useGetOrder from '@/features/order/useGetOrder'
import { Box } from '@mui/material'
import React from 'react'

function Order({ params }: { params: { orderId: string } }) {
    const { data: orderApiRes, isLoading, error } = useGetOrder(params.orderId)

    if (isLoading) {
        return 'Loading...'
    }

    if (!orderApiRes) {
        return "There is an error occurred!"
    }

    return (
        <Box>
            <div>{orderApiRes.data.id}</div>
            <div>Status: {OrderStatus[orderApiRes.data.status]}</div>
        </Box>
    )
}

export default Order
