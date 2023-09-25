'use client';

import React, { useState } from 'react';
import {
  TextField,
  Box,
  Card,
  CardHeader,
  CardContent,
  Divider,
  Grid,
  Typography,
} from '@mui/material';

import { useSnackbar } from 'notistack';
import useChangePassword from '@/features/user/useChangePassword';
import Swal from 'sweetalert2';
import withReactContent from 'sweetalert2-react-content';
import useLocalStorage from '@/common/hooks/useLocalStorage';
import StoreKeys from '@/common/constants/storekeys';
import LoadingButton from '@/components/LoadingButton';

export default function ChangePasswordCard() {
  const localStorage = useLocalStorage();
  const MySwal = withReactContent(Swal);
  const { enqueueSnackbar } = useSnackbar();
  const {
    mutate: changePasswordMutation,
    isError,
    error,
    isLoading,
  } = useChangePassword();

  const [formData, setFormData] = useState({
    oldPassword: '',
    newPassword: '',
    confirmPassword: '',
  });

  const clearForm = () => {
    setFormData({
      oldPassword: '',
      newPassword: '',
      confirmPassword: '',
    });
  };

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setFormData({ ...formData, [name]: value });
  };

  const handlePasswordChangeSuccess = () => {
    enqueueSnackbar('Password changed successfully!', { variant: 'success' });

    MySwal.fire({
      title: 'Password Changed Successfully',
      text: 'You must log in again after changing your password!',
      icon: 'success',
      confirmButtonColor: '#d33',
      confirmButtonText: 'OK, I understand!',
      allowOutsideClick: false,
      allowEscapeKey: false,
    }).then((result) => {
      if (result.isConfirmed) {
        fetch('/api/auth/logout', {
          method: 'POST',
        })
          .then(() => {
            localStorage.remove(StoreKeys.ACCESS_TOKEN);
            window.location.reload();
          })
          .catch(console.error);
      }
    });

    clearForm();
  };

  const handlePasswordChangeError = () => {
    const mutationError = error as any;
    if (
      isError &&
      mutationError?.response?.data?.password[0] === 'Incorrect password.'
    ) {
      enqueueSnackbar('Incorrect current password!', { variant: 'error' });
    } else {
      enqueueSnackbar('Password change failed. Please try again.', {
        variant: 'error',
      });
    }
  };

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    if (formData.newPassword !== formData.confirmPassword) {
      enqueueSnackbar('New passwords do not match.', { variant: 'error' });
      return;
    }

    const passwordRegex = /^(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{6,}$/;

    if (!passwordRegex.test(formData.newPassword)) {
      let errorMessage = 'New password does not meet the criteria:\n';

      if (formData.newPassword.length < 6) {
        errorMessage += 'Passwords must be at least 6 characters.\n';
      }

      if (!/[^\w\s]/.test(formData.newPassword)) {
        errorMessage +=
          'Passwords must have at least one non-alphanumeric characters.\n';
      }

      if (!/\d/.test(formData.newPassword)) {
        errorMessage += "Passwords must have at least one digit ('0'-'9').\n";
      }

      if (!/[A-Z]/.test(formData.newPassword)) {
        errorMessage +=
          "Passwords must have at least one uppercase ('A'-'Z').\n";
      }

      enqueueSnackbar(errorMessage, { variant: 'error' });
      return;
    }

    try {
      await changePasswordMutation(formData);
      if (!isError) {
        handlePasswordChangeSuccess();
      } else {
        handlePasswordChangeError();
      }
    } catch (error) {
      handlePasswordChangeError();
    }
  };

  return (
    <Card sx={{ minHeight: 634 }}>
      <CardHeader
        title="Change Password"
        subheader={'You should change your password periodically!'}
      />
      <Divider />
      <CardContent>
        <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
          <TextField
            margin="dense"
            variant="outlined"
            type="password"
            label="Current Password"
            name="oldPassword"
            value={formData.oldPassword}
            onChange={handleChange}
            fullWidth
            required
          />
          <TextField
            margin="dense"
            variant="outlined"
            type="password"
            label="New Password"
            name="newPassword"
            value={formData.newPassword}
            onChange={handleChange}
            fullWidth
            required
          />
          <TextField
            margin="dense"
            variant="outlined"
            type="password"
            label="Confirm Password"
            name="confirmPassword"
            value={formData.confirmPassword}
            onChange={handleChange}
            fullWidth
            required
          />
          <Box sx={{ mt: 1 }}>
            <Typography variant="h6" gutterBottom>
              Password requirements:
            </Typography>
            <Typography
              variant="body1"
              color="gray"
              fontStyle={'italic'}
              gutterBottom
            >
              Please follow this guide for a strong password
            </Typography>
            <Typography variant="body1" mx={2}>
              • At least 6 characters
            </Typography>
            <Typography variant="body1" mx={2}>
              • At least 1 special characters
            </Typography>
            <Typography variant="body1" mx={2}>
              • At least 1 digit (0 - 9)
            </Typography>
            <Typography variant="body1" mx={2}>
              • At least 1 uppercase (A - Z)
            </Typography>
            <Typography variant="body1" mx={2}>
              • Recommend: Change it often!
            </Typography>
          </Box>
          <Grid
            container
            direction="row"
            spacing={1}
            columns={{ xs: 1, sm: 1, md: 2 }}
            mt={1}
          >
            <Grid item xs={1}>
              <LoadingButton
                type="submit"
                variant="contained"
                color="primary"
                size="large"
                loading={isLoading}
                fullWidth
                disabled={
                  !formData.oldPassword ||
                  !formData.newPassword ||
                  !formData.confirmPassword
                }
              >
                UPDATE PASSWORD
              </LoadingButton>
            </Grid>
            <Grid item xs={1}>
              <LoadingButton
                type="button"
                variant="contained"
                color="error"
                fullWidth
                onClick={clearForm}
                disabled={
                  !formData.oldPassword &&
                  !formData.newPassword &&
                  !formData.confirmPassword
                }
              >
                CANCEL
              </LoadingButton>
            </Grid>
          </Grid>
        </Box>
      </CardContent>
    </Card>
  );
}
