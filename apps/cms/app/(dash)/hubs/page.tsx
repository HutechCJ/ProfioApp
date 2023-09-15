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
import { Hub, HubStatus } from '@/features/hub/hub.types';
import useGetHubs from '@/features/hub/useGetVehicles';

const columns: GridColDef<Hub>[] = [
  {
    field: 'id',
    headerName: 'ID',
    width: 250,
    renderCell(params) {
      return (
        <Link href={`/hubs/${params.value}`}>
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
        const value = params.value as HubStatus;
        if (value === HubStatus.Active) return 'success';
        if (value === HubStatus.Broken) return 'error';
        if (value === HubStatus.UnderMaintenance) return 'info';
        if (value === HubStatus.Full) return 'warning';
        return 'default';
      };
      return <Chip color={getColor()} label={`${HubStatus[params.value]}`} />;
    },
  },
  {
    field: 'zipCode',
    width: 150,
    headerName: 'Zip Code',
  },
  {
    field: 'location',
    width: 200,
    headerName: 'Location (Lat, Long)',
    valueGetter: (params) => {
      const { location: hubLocation } = params.row;
      return hubLocation
        ? `${hubLocation.latitude}, ${hubLocation.longitude}`
        : '';
    },
  },
];

function Hubs() {
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
  } = useGetHubs({
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
          <LoadingButton>Add Hub</LoadingButton>
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

export default Hubs;
