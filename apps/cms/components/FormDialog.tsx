'use client';

import React, { useState } from 'react';
import {
  Button,
  IconButton,
  Stack,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from '@mui/material';
import CancelIcon from '@mui/icons-material/Cancel';

interface FormDialogProps {
  buttonText: string;
  buttonVariant?: 'contained' | 'outlined' | 'text';
  buttonColor?: 'primary' | 'secondary' | 'success' | 'error';
  buttonIcon?: React.ReactNode;
  dialogTitle: string;
  dialogDescription?: string;
  componentProps: (handleClose: () => void) => any;
}

const FormDialog: React.FC<FormDialogProps> = ({
  buttonText = '',
  buttonVariant = 'contained',
  buttonColor = 'primary',
  buttonIcon,
  dialogTitle = '',
  dialogDescription = '',
  componentProps,
}) => {
  const [open, setOpen] = useState(false);

  const handleClickOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  return (
    <>
      <Button
        variant={buttonVariant}
        color={
          buttonColor as
            | 'inherit'
            | 'primary'
            | 'secondary'
            | 'success'
            | 'error'
        }
        startIcon={buttonIcon}
        onClick={handleClickOpen}
      >
        {buttonText}
      </Button>

      <Dialog open={open} onClose={handleClose}>
        <Stack
          direction="row"
          justifyContent="space-between"
          alignItems="center"
        >
          <DialogTitle>{dialogTitle}</DialogTitle>
          <DialogActions sx={{ marginRight: 2, marginBottom: 1 }}>
            <IconButton onClick={handleClose}>
              <CancelIcon />
            </IconButton>
          </DialogActions>
        </Stack>
        <DialogContent>
          <DialogContentText>{dialogDescription}</DialogContentText>
          {componentProps(handleClose)}
        </DialogContent>
      </Dialog>
    </>
  );
};

export default FormDialog;
