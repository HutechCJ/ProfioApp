'use client';

import React, { useEffect } from 'react';
import { Button, Divider, Stack } from '@mui/material';
import Link from '@/components/Link';

import InfoIcon from '@mui/icons-material/Info';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import FormDialog from './FormDialog';
import Swal from 'sweetalert2';
import withReactContent from 'sweetalert2-react-content';

interface ActionForListProps {
  entityId: string;
  entity: string;
  detailsLink: string;
  editComponentProps: (handleClose: () => void) => any;
  // deleteComponentProps: (handleClose: () => void) => any;
  handleDelete: any;
  isSuccess: boolean;
  onSuccess: () => void;
  refetchActions: () => void;
}

const ActionForList: React.FC<ActionForListProps> = ({
  entityId,
  entity,
  detailsLink,
  editComponentProps,
  // deleteComponentProps,
  handleDelete,
  isSuccess,
  onSuccess,
  refetchActions,
}) => {
  const MySwal = withReactContent(Swal);

  const deletionAction = () => {
    MySwal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        handleDelete();
        MySwal.fire({
          title: 'Deleted!',
          text: 'Your file has been deleted.',
          icon: 'success',
          confirmButtonColor: '#d33',
          cancelButtonColor: '#3085d6',
          confirmButtonText: 'OK',
        });
      }
    });
  };

  useEffect(() => {
    if (isSuccess) {
      refetchActions();
      onSuccess();
    }
  }, [isSuccess, refetchActions, onSuccess]);

  return (
    <div>
      <Stack
        direction="row"
        divider={<Divider orientation="vertical" flexItem />}
        spacing={2}
      >
        <Link href={detailsLink}>
          <Button color="primary" variant="text" startIcon={<InfoIcon />}>
            Details
          </Button>
        </Link>

        <FormDialog
          buttonText="Edit"
          buttonVariant="text"
          buttonColor="secondary"
          buttonIcon={<EditIcon />}
          dialogTitle={`${entity.toUpperCase()} INFORMATION`}
          dialogDescription={`ID: ${entityId}`}
          componentProps={editComponentProps}
        />

        {/* <FormDialog
          buttonText="Delete"
          buttonVariant="text"
          buttonColor="error"
          buttonIcon={<DeleteIcon />}
          dialogTitle={`Are you sure you want to delete this ${entity.toUpperCase()}?`}
          dialogDescription={`ID: ${entityId}`}
          componentProps={deleteComponentProps}
        /> */}

        <Button
          onClick={deletionAction}
          variant="text"
          startIcon={<DeleteIcon />}
          color="error"
        >
          Delete
        </Button>
      </Stack>
    </div>
  );
};

export default ActionForList;
