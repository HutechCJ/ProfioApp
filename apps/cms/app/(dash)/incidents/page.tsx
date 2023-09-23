'use client';

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
import React from 'react';
import useGetIncidents from '@/features/incident/useGetVehicles';
import { Incident, IncidentStatus } from '@/features/incident/incident.types';

const columns: GridColDef<Incident>[] = [
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
        const value = params.value as IncidentStatus;
        if (value === IncidentStatus.InProgress) return 'warning';
        if (value === IncidentStatus.Resolved) return 'success';
        return 'default';
      };
      return (
        <Chip color={getColor()} label={`${IncidentStatus[params.value]}`} />
      );
    },
  },
  {
    field: 'time',
    width: 150,
    headerName: 'Time',
    valueGetter: (params) => {
      const { time } = params.row;
      return time ? `${new Date(time).toLocaleString()}` : '';
    },
  },
  {
    field: 'description',
    width: 200,
    headerName: 'Description',
  },
  {
    field: 'delivery',
    width: 200,
    headerName: 'Delivery',
    valueGetter: (params) => {
      const { delivery } = params.row;
      return delivery ? `${delivery.id}` : '';
    },
  },
];

function Incidents() {
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
  } = useGetIncidents({
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
          <LoadingButton>Add Incident</LoadingButton>
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

export default Incidents;
