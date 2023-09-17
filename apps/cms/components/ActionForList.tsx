'use client';

import React from 'react';
import { Button, Divider, Stack } from '@mui/material';
import Link from '@/components/Link';

import InfoIcon from '@mui/icons-material/Info';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import FormDialog from './FormDialog';

interface ActionForListProps {
  entityId: string;
  entity: string;
  detailsLink: string;
  editComponentProps: (handleClose: () => void) => any;
  deleteComponentProps: (handleClose: () => void) => any;
}

const ActionForList: React.FC<ActionForListProps> = ({
  entityId,
  entity,
  detailsLink,
  editComponentProps,
  deleteComponentProps,
}) => {
  return (
    <div>
      <Stack
        direction="row"
        divider={<Divider orientation="vertical" flexItem />}
        spacing={2}
      >
        <Link href={detailsLink}>
          <Button color="primary" startIcon={<InfoIcon />}>
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

        <FormDialog
          buttonText="Delete"
          buttonVariant="text"
          buttonColor="error"
          buttonIcon={<DeleteIcon />}
          dialogTitle={`Are you sure you want to delete this ${entity.toUpperCase()}?`}
          dialogDescription={`ID: ${entityId}`}
          componentProps={deleteComponentProps}
        />
      </Stack>
    </div>
  );
};

export default ActionForList;
