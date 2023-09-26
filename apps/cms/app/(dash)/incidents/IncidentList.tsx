'use client';

import React from 'react';

// import Link from '@/components/Link';
import LoadingButton from '@/components/LoadingButton';
import {
  Box,
  Typography,
  Stack,
  ButtonGroup,
  Divider,
  Chip,
} from '@mui/material';
import { DataGrid, GridColDef, GridToolbar } from '@mui/x-data-grid';

import ReplayIcon from '@mui/icons-material/Replay';

import { Incident, IncidentStatus } from '@/features/incident/incident.types';
import useGetIncidents from '@/features/incident/useGetIncidents';
import CopyTextButton from '@/components/CopyTextButton';

function IncidentList() {
  const [paginationModel, setPaginationModel] = React.useState({
    page: 0,
    pageSize: 10,
  });

  const {
    data: pagingIncidents,
    isLoading,
    refetch,
    remove,
  } = useGetIncidents({
    PageIndex: paginationModel.page + 1,
    PageSize: paginationModel.pageSize,
  });

  const rowCount = pagingIncidents?.data.totalCount || 0;

  const [rowCountState, setRowCountState] = React.useState(rowCount);

  React.useEffect(() => {
    setRowCountState((prevRowCountState) =>
      rowCount !== undefined ? rowCount : prevRowCountState,
    );
  }, [rowCount, setRowCountState]);

  const columns: GridColDef<Incident>[] = [
    {
      field: 'id',
      headerName: 'ID',
      headerAlign: 'center',
      align: 'center',
      width: 250,
      renderCell(params) {
        const maxLength = 6;
        const truncatedValue =
          params.value.length > maxLength
            ? params.value.slice(0, 8) + '...'
            : params.value;

        return (
          <>
            <CopyTextButton text={params.value} />
            {/* <Link href={`/incidents/${params.value}`}> */}
            <Typography variant="button">{truncatedValue}</Typography>
            {/* </Link> */}
          </>
        );
      },
    },
    {
      field: 'status',
      headerName: 'STATUS',
      headerAlign: 'center',
      align: 'center',
      width: 200,
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
      width: 250,
      headerName: 'TIME',
      headerAlign: 'center',
      align: 'center',
      valueGetter: (params) => {
        const { time } = params.row;
        return time ? `${new Date(time).toLocaleString()}` : '';
      },
    },
    {
      field: 'description',
      width: 400,
      headerName: 'DESCRIPTION',
      headerAlign: 'center',
      align: 'center',
    },
    {
      field: 'delivery',
      width: 250,
      headerName: 'DELIVERY',
      headerAlign: 'center',
      align: 'center',
      valueGetter: (params) => {
        const { delivery } = params.row;
        return delivery ? `${delivery.id}` : '';
      },
    },
  ];

  return (
    <Box>
      <Stack
        direction={{ sm: 'column', md: 'row' }}
        justifyContent="space-between"
        alignItems={{ sm: 'flex-start', md: 'center' }}
        spacing={{ sm: 1 }}
        marginBottom={2}
      >
        <Typography variant="h5" fontWeight="bold">
          INCIDENT LIST
        </Typography>
        <ButtonGroup variant="text" aria-label="button-group-reload-and-create">
          <LoadingButton
            onClick={() => {
              remove();
              refetch();
            }}
            variant="contained"
            startIcon={<ReplayIcon />}
            loading={isLoading}
          >
            RELOAD
          </LoadingButton>
        </ButtonGroup>
      </Stack>
      <DataGrid
        columns={columns}
        rows={pagingIncidents?.data.items || []}
        rowCount={rowCountState}
        loading={isLoading}
        pageSizeOptions={[5, 10, 20, 50, 100]}
        paginationModel={paginationModel}
        paginationMode="server"
        onPaginationModelChange={setPaginationModel}
        sx={{
          backgroundColor: 'white',
          width: '100%',
          height: 668,
        }}
        slots={{
          toolbar: (props) => (
            <>
              <GridToolbar {...props} />
              <Divider />
            </>
          ),
        }}
      />
    </Box>
  );
}

export default IncidentList;
