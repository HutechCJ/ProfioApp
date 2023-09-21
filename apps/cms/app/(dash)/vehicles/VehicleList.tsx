'use client'

import React from 'react'

import Link from '@/components/Link'
import LoadingButton from '@/components/LoadingButton'
import { Box, Typography, Stack, ButtonGroup, Chip } from '@mui/material'
import { DataGrid, GridColDef } from '@mui/x-data-grid'

import ReplayIcon from '@mui/icons-material/Replay'
import PersonAddIcon from '@mui/icons-material/PersonAdd'
import LocalShippingIcon from '@mui/icons-material/LocalShipping'
import RvHookupIcon from '@mui/icons-material/RvHookup'
import AirportShuttleIcon from '@mui/icons-material/AirportShuttle'
import TwoWheelerIcon from '@mui/icons-material/TwoWheeler'

import {
    Vehicle,
    VehicleType,
    VehicleStatus,
} from '@/features/vehicle/vehicle.types'
import useGetVehicles from '@/features/vehicle/useGetVehicles'
import FormDialog from '@/components/FormDialog'
import AddVehicle from './AddVehicle'
import EditVehicle from './EditVehicle'
import DeleteVehicle from './DeleteVehicle'
import ActionForList from '@/components/ActionForList'

function VehicleList() {
    const [paginationModel, setPaginationModel] = React.useState({
        page: 0,
        pageSize: 10,
    })

    const {
        data: pagingVehicles,
        isLoading,
        refetch,
        remove,
    } = useGetVehicles({
        PageIndex: paginationModel.page + 1,
        PageSize: paginationModel.pageSize,
    })

    const rowCount = pagingVehicles?.data.totalCount || 0

    const [rowCountState, setRowCountState] = React.useState(rowCount)

    React.useEffect(() => {
        setRowCountState((prevRowCountState) =>
            rowCount !== undefined ? rowCount : prevRowCountState
        )
    }, [rowCount, setRowCountState])

    const columns: GridColDef<Vehicle>[] = [
        {
            field: 'id',
            headerName: 'ID',
            width: 300,
            renderCell(params) {
                return (
                    <Link href={`/vehicles/${params.value}`}>
                        <Typography variant="button">{params.value}</Typography>
                    </Link>
                )
            },
        },
        {
            field: 'status',
            headerName: 'STATUS',
            width: 120,
            renderCell(params) {
                const getColor = () => {
                    const value = params.value as VehicleStatus
                    if (value === VehicleStatus.Busy) return 'error'
                    if (value === VehicleStatus.Idle) return 'warning'
                    return 'primary'
                }
                return (
                    <Chip
                        color={getColor()}
                        label={`${VehicleStatus[params.value]}`}
                    />
                )
            },
        },
        {
            field: 'zipCodeCurrent',
            width: 150,
            headerName: 'ZIP CODE CURRENT',
        },
        {
            field: 'licensePlate',
            width: 150,
            headerName: 'LICENSE PLATE',
        },
        {
            field: 'type',
            width: 150,
            headerName: 'TYPE',
            renderCell(params) {
                const getColor = () => {
                    const value = params.value as VehicleType
                    if (value === VehicleType.Trailer) return 'error'
                    if (value === VehicleType.Van) return 'warning'
                    if (value === VehicleType.Motorcycle) return 'secondary'
                    return 'primary'
                }
                const getIcon = () => {
                    const value = params.value as VehicleType
                    if (value === VehicleType.Trailer) return <RvHookupIcon />
                    if (value === VehicleType.Van) return <AirportShuttleIcon />
                    if (value === VehicleType.Motorcycle)
                        return <TwoWheelerIcon />
                    return <LocalShippingIcon />
                }
                return (
                    <Chip
                        icon={getIcon()}
                        color={getColor()}
                        label={`${VehicleType[params.value]}`}
                    />
                )
            },
        },
        {
            field: 'staff',
            width: 200,
            headerName: 'STAFF',
            valueGetter: (params) => {
                const { staff } = params.row
                return `${staff?.name || 'Empty'}`
            },
            renderCell(params) {
                const { staff } = params.row
                return (
                    <Link href={`/staffs/${staff?.id}`} sx={{ color: 'black' }}>
                        <Typography variant="button">
                            {staff?.name || 'Empty'}
                        </Typography>
                    </Link>
                )
            },
        },
        {
            field: 'actions',
            width: 400,
            headerName: 'ACTIONS',
            sortable: false,
            filterable: false,
            renderCell: (params) => {
                const vehicleId = params.row.id
                return (
                    <ActionForList
                        entityId={vehicleId}
                        entity="vehicle"
                        detailsLink={`/vehicles/${vehicleId}`}
                        editComponentProps={(handleClose) => (
                            <EditVehicle
                                onSuccess={() => {
                                    handleClose()
                                    refetch()
                                }}
                                params={{ vehicleId: vehicleId }}
                            />
                        )}
                        deleteComponentProps={(handleClose) => (
                            <DeleteVehicle
                                onSuccess={() => {
                                    handleClose()
                                    refetch()
                                }}
                                params={{ vehicleId: vehicleId }}
                            />
                        )}
                    />
                )
            },
        },
    ]

    return (
        <Box sx={{ paddingY: 4 }}>
            <Stack
                direction="row"
                justifyContent="space-between"
                alignItems="center"
                marginBottom={2}
            >
                <Typography variant="h5" fontWeight="bold">
                    VEHICLE LIST
                </Typography>
                <ButtonGroup
                    variant="text"
                    aria-label="button-group-reload-and-create"
                >
                    <LoadingButton
                        onClick={() => {
                            remove()
                            refetch()
                        }}
                        variant="contained"
                        startIcon={<ReplayIcon />}
                    >
                        RELOAD
                    </LoadingButton>
                    <FormDialog
                        buttonText="ADD VEHICLE"
                        buttonVariant="contained"
                        buttonColor="success"
                        buttonIcon={<PersonAddIcon />}
                        dialogTitle="ADD VEHICLE"
                        dialogDescription="Please enter information for the vehicle"
                        componentProps={(handleClose) => (
                            <AddVehicle
                                onSuccess={() => {
                                    refetch()
                                    handleClose()
                                }}
                            />
                        )}
                    />
                </ButtonGroup>
            </Stack>
            <DataGrid
                columns={columns}
                rows={pagingVehicles?.data.items || []}
                rowCount={rowCountState}
                loading={isLoading}
                pageSizeOptions={[5, 10, 20, 50, 100]}
                paginationModel={paginationModel}
                paginationMode="server"
                onPaginationModelChange={setPaginationModel}
                sx={{
                    backgroundColor: 'white',
                    width: "100%"
                }}
            />
        </Box>
    )
}

export default VehicleList
