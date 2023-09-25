'use client';

import React from 'react';

import Link from '@/components/Link';
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

import { Hub, HubStatus } from '@/features/hub/hub.types';
import useGetHubs from '@/features/hub/useGetHubs';
import CopyTextButton from '@/components/CopyTextButton';

function HubList() {
  const [paginationModel, setPaginationModel] = React.useState({
    page: 0,
    pageSize: 10,
  });

  const {
    data: pagingHubs,
    isLoading,
    refetch,
    remove,
  } = useGetHubs({
    PageIndex: paginationModel.page + 1,
    PageSize: paginationModel.pageSize,
  });

  const rowCount = pagingHubs?.data.totalCount || 0;

  const [rowCountState, setRowCountState] = React.useState(rowCount);

  React.useEffect(() => {
    setRowCountState((prevRowCountState) =>
      rowCount !== undefined ? rowCount : prevRowCountState
    );
  }, [rowCount, setRowCountState]);

  const columns: GridColDef<Hub>[] = [
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
            <Link href={`/hubs/${params.value}`}>
              <Typography variant="button">{truncatedValue}</Typography>
            </Link>
          </>
        );
      },
    },
    {
      field: 'status',
      headerName: 'Status',
      headerAlign: 'center',
      align: 'center',
      width: 350,
      renderCell(params) {
        const getColor = () => {
          const value = params.value as HubStatus;
          if (value === HubStatus.Active) return 'success';
          if (value === HubStatus.Broken) return 'error';
          if (value === HubStatus.UnderMaintenance) return 'info';
          if (value === HubStatus.Full) return 'warning';
          return 'default';
        };
        return (
          <Chip
            color={getColor()}
            label={`${HubStatus[params.value]}`}
            sx={{ width: 90 }}
          />
        );
      },
    },
    {
      field: 'zipCode',
      width: 400,
      headerName: 'Zip Code',
      headerAlign: 'center',
      align: 'center',
    },
    {
      field: 'location',
      width: 400,
      headerName: 'Location (Lat, Long)',
      headerAlign: 'center',
      align: 'center',
      valueGetter: (params) => {
        const { location: hubLocation } = params.row;
        return hubLocation
          ? `${hubLocation.latitude}, ${hubLocation.longitude}`
          : '';
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
          HUB LIST
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
        rows={pagingHubs?.data.items || []}
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

export default HubList;
