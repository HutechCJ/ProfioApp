'use client';
import React from 'react';
import {
  Box,
  Stack,
  Typography,
  Chip,
  Button,
  ButtonGroup,
} from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import Link from '@/components/Link';
import LoadingButton from '@/components/LoadingButton';
import useGetVehicles from '@/features/vehicle/useGetVehicles';
import {
  Vehicle,
  VehicleStatus,
  VehicleType,
} from '@/features/vehicle/vehicle.types';

const columns: GridColDef<Vehicle>[] = [
  {
    field: 'id',
    headerName: 'ID',
    width: 250,
    renderCell(params) {
      return (
        <Link href={`/vehicles/${params.value}`}>
          <Typography variant="button">{params.value}</Typography>
        </Link>
      );
    },
  },
  {
    field: 'status',
    headerName: 'Status',
    width: 120,
    renderCell(params) {
      const getColor = () => {
        const value = params.value as VehicleStatus;
        if (value === VehicleStatus.Busy) return 'error';
        if (value === VehicleStatus.Idle) return 'warning';
        return 'default';
      };
      return (
        <Chip color={getColor()} label={`${VehicleStatus[params.value]}`} />
      );
    },
  },
  {
    field: 'zipCodeCurrent',
    width: 150,
    headerName: 'Zip Code Current',
  },
  {
    field: 'licensePlate',
    width: 200,
    headerName: 'License Plate',
  },
  {
    field: 'type',
    width: 200,
    headerName: 'Type',
    valueGetter: (params) => {
      const { type } = params.row;
      return `${VehicleType[type]}`;
    },
  },
  {
    field: 'staff',
    width: 200,
    headerName: 'Staff',
    valueGetter: (params) => {
      const { staff } = params.row;
      return `${staff?.id || 'Empty'}`;
    },
  },
];

function Vehicles() {
  const [paginationModel, setPaginationModel] = React.useState({
    page: 0,
    pageSize: 10,
  });

  const {
    data: pagingData,
    isLoading,
    isError,
    refetch,
    remove,
  } = useGetVehicles({
    PageIndex: paginationModel.page + 1,
    PageSize: paginationModel.pageSize,
  });

  const rowCount = pagingData?.data.totalCount || 0;

  const [rowCountState, setRowCountState] = React.useState(rowCount);

  React.useEffect(() => {
    setRowCountState((prevRowCountState) =>
      rowCount !== undefined ? rowCount : prevRowCountState,
    );
  }, [rowCount, setRowCountState]);

  return (
    <Box>
      {isError && 'There is an error occurred'}
      <Stack direction="row" justifyContent={'end'} spacing={1} sx={{ mb: 1 }}>
        <ButtonGroup>
          <LoadingButton
            onClick={() => {
              remove();
              refetch();
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
          rows={pagingData?.data.items || []}
          rowCount={rowCountState}
          loading={isLoading}
          pageSizeOptions={[5, 10, 20, 50, 100]}
          paginationModel={paginationModel}
          paginationMode="server"
          onPaginationModelChange={setPaginationModel}
        />
      </Box>
    </Box>
  );
}

export default Vehicles;
