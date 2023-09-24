'use client';

import React from 'react';

import Link from '@/components/Link';
import LoadingButton from '@/components/LoadingButton';
import {
  Box,
  Typography,
  Stack,
  ButtonGroup,
  Chip,
  Divider,
} from '@mui/material';
import { DataGrid, GridColDef, GridToolbar } from '@mui/x-data-grid';

import ReplayIcon from '@mui/icons-material/Replay';
import PersonAddIcon from '@mui/icons-material/PersonAdd';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import RvHookupIcon from '@mui/icons-material/RvHookup';
import AirportShuttleIcon from '@mui/icons-material/AirportShuttle';
import TwoWheelerIcon from '@mui/icons-material/TwoWheeler';

import {
  Vehicle,
  VehicleType,
  VehicleStatus,
} from '@/features/vehicle/vehicle.types';
import useGetVehicles from '@/features/vehicle/useGetVehicles';
import FormDialog from '@/components/FormDialog';
import AddVehicle from './AddVehicle';
import EditVehicle from './EditVehicle';
import ActionForList from '@/components/ActionForList';
import useDeleteVehicle from '@/features/vehicle/useDeleteVehicle';
import useCountByVehicleType from '@/features/vehicle/useCountByVehicleType';
import useCountByVehicleStatus from '@/features/vehicle/useCountByVehicleStatus';
import CopyTextButton from '@/components/CopyTextButton';

function VehicleList() {
  const [paginationModel, setPaginationModel] = React.useState({
    page: 0,
    pageSize: 10,
  });

  const {
    data: pagingVehicles,
    isLoading,
    refetch,
    remove,
  } = useGetVehicles({
    PageIndex: paginationModel.page + 1,
    PageSize: paginationModel.pageSize,
  });

  const { mutate: deleteVehicle, isSuccess } = useDeleteVehicle();

  const { refetch: refetchCountType } = useCountByVehicleType();
  const { refetch: refetchCountStatus } = useCountByVehicleStatus();

  React.useEffect(() => {
    if (isSuccess) {
      refetchCountStatus();
      refetchCountType();
      refetch();
    }
  }, [isSuccess, refetchCountStatus, refetchCountType, refetch]);

  const rowCount = pagingVehicles?.data.totalCount || 0;

  const [rowCountState, setRowCountState] = React.useState(rowCount);

  React.useEffect(() => {
    setRowCountState((prevRowCountState) =>
      rowCount !== undefined ? rowCount : prevRowCountState,
    );
  }, [rowCount, setRowCountState]);

  const columns: GridColDef<Vehicle>[] = [
    {
      field: 'id',
      headerName: 'ID',
      headerAlign: 'center',
      align: 'center',
      width: 150,
      renderCell(params) {
        const maxLength = 6;
        const truncatedValue =
          params.value.length > maxLength
            ? params.value.slice(0, 8) + '...'
            : params.value;

        return (
          <>
            <CopyTextButton text={params.value} />
            <Link href={`/vehicles/${params.value}`}>
              <Typography variant="button">{truncatedValue}</Typography>
            </Link>
          </>
        );
      },
    },
    {
      field: 'status',
      headerName: 'STATUS',
      headerAlign: 'center',
      align: 'center',
      width: 120,
      renderCell(params) {
        const getColor = () => {
          const value = params.value as VehicleStatus;
          if (value === VehicleStatus.Busy) return 'error';
          if (value === VehicleStatus.Idle) return 'warning';
          return 'primary';
        };
        return (
          <Chip
            color={getColor()}
            label={`${VehicleStatus[params.value]}`}
            sx={{ width: 70 }}
          />
        );
      },
    },
    {
      field: 'zipCodeCurrent',
      width: 200,
      headerName: 'ZIP CODE CURRENT',
      headerAlign: 'center',
      align: 'center',
    },
    {
      field: 'licensePlate',
      width: 150,
      headerName: 'LICENSE PLATE',
      headerAlign: 'center',
      align: 'center',
    },
    {
      field: 'type',
      width: 150,
      headerName: 'TYPE',
      headerAlign: 'center',
      align: 'center',
      renderCell(params) {
        const getColor = () => {
          const value = params.value as VehicleType;
          if (value === VehicleType.Trailer) return 'error';
          if (value === VehicleType.Van) return 'warning';
          if (value === VehicleType.Motorcycle) return 'secondary';
          return 'primary';
        };
        const getIcon = () => {
          const value = params.value as VehicleType;
          if (value === VehicleType.Trailer) return <RvHookupIcon />;
          if (value === VehicleType.Van) return <AirportShuttleIcon />;
          if (value === VehicleType.Motorcycle) return <TwoWheelerIcon />;
          return <LocalShippingIcon />;
        };
        return (
          <Chip
            icon={getIcon()}
            color={getColor()}
            label={`${VehicleType[params.value]}`}
            sx={{ width: 120 }}
          />
        );
      },
    },
    {
      field: 'staff',
      width: 300,
      headerName: 'STAFF',
      headerAlign: 'center',
      align: 'center',
      valueGetter: (params) => {
        const { staff } = params.row;
        return `${staff?.name || 'Empty'}`;
      },
      renderCell(params) {
        const { staff } = params.row;
        return (
          <Link href={`/staffs/${staff?.id}`} sx={{ color: 'black' }}>
            <Typography variant="button">{staff?.name || 'Empty'}</Typography>
          </Link>
        );
      },
    },
    {
      field: 'actions',
      width: 320,
      headerName: 'ACTIONS',
      headerAlign: 'center',
      align: 'center',
      sortable: false,
      filterable: false,
      renderCell: (params) => {
        const vehicleId = params.row.id;
        return (
          <ActionForList
            entityId={vehicleId}
            entity="vehicle"
            detailsLink={`/vehicles/${vehicleId}`}
            editComponentProps={(handleClose) => (
              <EditVehicle
                onSuccess={() => {
                  handleClose();
                  refetch();
                }}
                params={{ vehicleId: vehicleId }}
              />
            )}
            handleDelete={() => deleteVehicle(vehicleId)}
          />
        );
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
          VEHICLE LIST
        </Typography>
        <ButtonGroup variant="text" aria-label="button-group-reload-and-create">
          <LoadingButton
            onClick={() => {
              remove();
              refetch();
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
                  refetch();
                  handleClose();
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

export default VehicleList;
