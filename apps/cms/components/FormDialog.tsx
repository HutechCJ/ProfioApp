'use client';

import React, { useState } from 'react';
import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from '@mui/material';

interface FormDialogProps {
  buttonText: string;
  buttonVariant?: 'contained' | 'outlined' | 'text';
  buttonColor?: 'primary' | 'secondary' | 'success' | 'error';
  buttonIcon?: React.ReactNode;
  dialogTitle: string;
  dialogDescription?: string;
  submitButton: string;
  handleSubmitFn?: () => void;
  componentProps?: React.ReactNode;
}

const FormDialog: React.FC<FormDialogProps> = ({
  buttonText = '',
  buttonVariant = 'contained',
  buttonColor = 'primary',
  buttonIcon,
  dialogTitle = '',
  dialogDescription = '',
  submitButton = '',
  handleSubmitFn,
  componentProps,
}) => {
  const [open, setOpen] = useState(false);

  const handleClickOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const handleSubmit = () => {
    if (handleSubmitFn) {
      handleSubmitFn();
    }
    handleClose();
  };

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
        <DialogTitle>{dialogTitle}</DialogTitle>
        <DialogContent>
          <DialogContentText>{dialogDescription}</DialogContentText>
          {componentProps}
        </DialogContent>
        <DialogActions>
          <Button variant="contained" color="error" onClick={handleClose}>
            Đóng
          </Button>
          <Button variant="contained" color="success" onClick={handleSubmit}>
            {submitButton}
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
};

export default FormDialog;
