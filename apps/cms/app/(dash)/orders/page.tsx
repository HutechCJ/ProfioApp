'use client'

import Link from '@/components/Link'
import LoadingButton from '@/components/LoadingButton'
import { Order, OrderStatus } from '@/features/order/order.types'
import useGetOrders from '@/features/order/useGetOrders'
import { Box, Stack, Typography, Chip, ButtonGroup, Button } from '@mui/material'
import { DataGrid, GridColDef } from '@mui/x-data-grid'
import React from 'react'

const columns: GridColDef<Order>[] = [
    {
        field: 'id',
        headerName: 'ID',
        width: 250,
        renderCell(params) {
            return (
                <Link href={`/orders/${params.value}`}>
                    <Typography variant="button">{params.value}</Typography>
                </Link>
            )
        },
    },
    {
        field: 'status',
        headerName: 'Status',
        width: 120,
        renderCell(params) {
            const getColor = () => {
                const value = params.value as OrderStatus
                if (value === OrderStatus.Cancelled) return 'error'
                if (value === OrderStatus.Completed) return 'success'
                if (value === OrderStatus.InProgress) return 'warning'
                return 'default'
            }
            return (
                <Chip
                    color={getColor()}
                    label={`${OrderStatus[params.value]}`}
                />
            )
        },
    },
    {
        field: 'startedDate',
        width: 200,
        headerName: 'Started Date',
        valueGetter: (params) => {
            const { startedDate } = params.row
            return `${new Date(startedDate).toLocaleString()}`
        },
    },
    {
        field: 'expectedDeliveryTime',
        width: 200,
        headerName: 'Expected Delivery Time',
        valueGetter: (params) => {
            const { expectedDeliveryTime } = params.row
            return `${new Date(expectedDeliveryTime).toLocaleString()}`
        },
    },
    {
        field: 'destinationZipCode',
        width: 150,
        headerName: 'Zip Code',
    },
    {
        field: 'note',
        width: 200,
        headerName: 'Note',
    },
]

function Orders() {
    const [paginationModel, setPaginationModel] = React.useState({
        page: 0,
        pageSize: 10,
    })

    const {
        data: pagingOrders,
        isLoading,
        isError,
        refetch,
        remove,
    } = useGetOrders({
        PageIndex: paginationModel.page + 1,
        PageSize: paginationModel.pageSize,
    })

    const rowCount = pagingOrders?.data.totalCount || 0

    const [rowCountState, setRowCountState] = React.useState(rowCount)

    React.useEffect(() => {
        setRowCountState((prevRowCountState) =>
            rowCount !== undefined ? rowCount : prevRowCountState
        )
    }, [rowCount, setRowCountState])

    return (
        <Box>
            {isError && 'There is an error occurred'}
            <Stack
                direction="row"
                justifyContent={'end'}
                spacing={1}
                sx={{ mb: 1 }}
            >
                <ButtonGroup>
                    <LoadingButton
                        onClick={() => {
                            remove()
                            refetch()
                        }}
                        loading={isLoading}
                    >
                        Reload
                    </LoadingButton>
                    <LoadingButton>Add Vehicle</LoadingButton>
                </ButtonGroup>
            </Stack>
            <Box sx={{ width: '100%', overflow: 'auto' }}>
                <DataGrid
                    autoHeight
                    columns={columns}
                    rows={pagingOrders?.data.items || []}
                    rowCount={rowCountState}
                    loading={isLoading}
                    pageSizeOptions={[5, 10, 20, 50, 100]}
                    paginationModel={paginationModel}
                    paginationMode="server"
                    onPaginationModelChange={setPaginationModel}
                />
            </Box>
        </Box>
    )
}

export default Orders
