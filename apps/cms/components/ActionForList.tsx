'use client';

import Link from '@/components/Link';
import { Button, Divider, Stack } from '@mui/material';
import React from 'react';

import useSwal from '@/common/hooks/useSwal';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import InfoIcon from '@mui/icons-material/Info';
import FormDialog from './FormDialog';

interface ActionForListProps {
  entityId: string;
  entity: string;
  detailsLink: string;
  editComponentProps: (handleClose: () => void) => any;
  handleDelete: any;
}

const ActionForList: React.FC<ActionForListProps> = ({
  entityId,
  entity,
  detailsLink,
  editComponentProps,
  handleDelete,
}) => {
  const MySwal = useSwal();

  const deletionAction = () => {
    MySwal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#007dc3',
      cancelButtonColor: '#d32f2f',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        handleDelete();
        MySwal.fire({
          title: 'Deleted!',
          text: 'Your data has been deleted.',
          icon: 'success',
          confirmButtonColor: '#007dc3',
          confirmButtonText: 'OK',
        });
      }
    });
  };

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
