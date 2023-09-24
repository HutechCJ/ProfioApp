'use client';

import React, { useState } from 'react';
import {
  TextField,
  Button,
  Box,
  Card,
  CardHeader,
  CardContent,
  Divider,
  Grid,
} from '@mui/material';

import { useSnackbar } from 'notistack';
import useChangePassword from '@/features/user/useChangePassword';

export default function ChangePasswordCard() {
  const [formData, setFormData] = useState({
    oldPassword: '',
    newPassword: '',
    confirmPassword: '',
  });

  const { enqueueSnackbar } = useSnackbar();
  const { mutate: changePasswordMutation } = useChangePassword();

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    if (formData.newPassword !== formData.confirmPassword) {
      enqueueSnackbar('New passwords do not match.', { variant: 'error' });
      return;
    }

    try {
      await changePasswordMutation(formData);
      enqueueSnackbar('Password changed successfully!', { variant: 'success' });
      clearForm();
    } catch (error) {
      enqueueSnackbar('Password change failed. Please try again.', {
        variant: 'error',
      });
    }
  };

  const clearForm = () => {
    setFormData({
      oldPassword: '',
      newPassword: '',
      confirmPassword: '',
    });
  };

  return (
    <Card sx={{ height: 400 }}>
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
            label="Old Password"
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
          <Grid
            container
            direction="row"
            spacing={1}
            columns={{ xs: 1, sm: 1, md: 2 }}
            mt={1}
          >
            <Grid item xs={1}>
              <Button
                type="submit"
                variant="contained"
                color="primary"
                size="large"
                fullWidth
              >
                SUBMIT
              </Button>
            </Grid>
            <Grid item xs={1}>
              <Button
                type="button"
                variant="contained"
                color="error"
                size="large"
                fullWidth
                onClick={clearForm}
              >
                CANCEL
              </Button>
            </Grid>
          </Grid>
        </Box>
      </CardContent>
    </Card>
  );
}
