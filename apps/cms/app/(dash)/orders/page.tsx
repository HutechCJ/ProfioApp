'use client'

import Link from '@/components/Link'
import LoadingButton from '@/components/LoadingButton'
import { Order, OrderStatus } from '@/features/order/order.types'
import useGetOrders from '@/features/order/useGetOrders'
import { Box, Typography } from '@mui/material'
import { DataGrid, GridColDef } from '@mui/x-data-grid'
import React from 'react'

const columns: GridColDef<Order>[] = [
    {
        field: 'id',
        headerName: 'ID',
        width: 200,
        renderCell(params) {
            return (
                <Link href={`/orders/${params.value}`}>
                    <Typography variant="button">{params.value}</Typography>
                </Link>
            )
        },
    },
    {
        field: 'startedDate',
        width: 200,
        headerName: 'startedDate',
        valueGetter: (params) => {
            const { startedDate } = params.row
            return `${new Date(startedDate).toLocaleString()}`
        },
    },
    {
        field: 'expectedDeliveryTime',
        width: 200,
        headerName: 'expectedDeliveryTime',
        valueGetter: (params) => {
            const { expectedDeliveryTime } = params.row
            return `${new Date(expectedDeliveryTime).toLocaleString()}`
        },
    },
    {
        field: 'status',
        headerName: 'status',
        valueGetter: (params) => {
            const { status } = params.row
            return `${OrderStatus[status]}`
        },
    },
    {
        field: 'destinationAddress',
        width: 150,
        headerName: 'destinationAddress',
        valueGetter: (params) => {
            const { destinationAddress } = params.row
            return `${destinationAddress?.street || ''} ${
                destinationAddress?.ward || ''
            } ${destinationAddress?.city || ''} ${
                destinationAddress?.province || ''
            } ${destinationAddress?.zipCode || ''}`
        },
    },
    {
        field: 'destinationZipCode',
        headerName: 'destinationZipCode',
    },
    {
        field: 'note',
        width: 150,
        headerName: 'note',
    },
    {
        field: 'distance',
        headerName: 'distance',
    },
    {
        field: 'customerId',
        width: 200,
        headerName: 'customerId',
    },
]

function Orders() {
    const [paginationModel, setPaginationModel] = React.useState({
        page: 0,
        pageSize: 5,
    })

    const {
        data: pagingOrders,
        isLoading,
        refetch,
        remove,
    } = useGetOrders({
        PageNumber: paginationModel.page + 1,
        PageSize: paginationModel.pageSize,
    })

    const rowCount = pagingOrders?.data.totalPages || 0

    const [rowCountState, setRowCountState] = React.useState(rowCount)

    React.useEffect(() => {
        setRowCountState((prevRowCountState) =>
            rowCount !== undefined ? rowCount : prevRowCountState
        )
    }, [rowCount, setRowCountState])

    return (
        <Box>
            <LoadingButton
                onClick={() => {
                    remove()
                    refetch()
                }}
            >
                Reload
            </LoadingButton>
            <DataGrid
                columns={columns}
                rows={pagingOrders?.data.items || []}
                rowCount={rowCountState}
                loading={isLoading}
                pageSizeOptions={[1, 5, 10]}
                paginationModel={paginationModel}
                paginationMode="server"
                onPaginationModelChange={setPaginationModel}
            />
        </Box>
    )
}

export default Orders
