'use client';

import Copyright from '@/components/Copyright';
import LoadingButton from '@/components/LoadingButton';
import useLogin from '@/features/user/useLogin';
import {
  Alert,
  AlertTitle,
  Box,
  Checkbox,
  Container,
  FormControlLabel,
  Stack,
  TextField,
  Typography,
} from '@mui/material';
import Image from 'next/image';
import { useSnackbar } from 'notistack';
import React, { useEffect } from 'react';
import Logo from '../../../../cms/public/images/CJ_logo.png';

function SignIn() {
  const { enqueueSnackbar } = useSnackbar();
  const {
    mutate: login,
    data: result,
    isLoading,
    isSuccess,
    error,
    isError,
  } = useLogin();

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    login({
      userName: data.get('userName') as string,
      password: data.get('password') as string,
    });
  };

  useEffect(() => {
    if (isSuccess) {
      enqueueSnackbar(`Logged in!`, { variant: 'success' });
      window.location.reload();
    }
  }, [isSuccess, enqueueSnackbar]);

  return (
    <Container
      sx={{
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'space-between',
        minHeight: '100vh',
        position: 'relative',
      }}
    >
      <Box
        sx={{
          marginTop: 8,
          marginBottom: 2,
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
        }}
      >
        <Stack
          direction="row"
          justifyContent="center"
          alignItems="center"
          spacing={2}
        >
          <Image src={Logo} alt="CJ Logo" width={80} height={80} />
          <Typography variant="h3" fontWeight="bold">
            Profio Application
          </Typography>
        </Stack>
        <Typography variant="h5" mt={1}>
          Content Management System for Profio
        </Typography>
        <Typography variant="h4" fontWeight="bold" m={10}>
          Welcome Back!
        </Typography>
        <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
          <TextField
            margin="normal"
            required
            fullWidth
            id="userName"
            label="Email"
            name="userName"
            autoComplete="userName"
          />
          <TextField
            margin="normal"
            required
            fullWidth
            name="password"
            label="Password"
            type="password"
            id="password"
            autoComplete="current-password"
          />
          <FormControlLabel
            control={<Checkbox value="remember" color="primary" />}
            label="Remember me"
          />
          <Box>
            {isError && (
              <Alert severity="error">
                <AlertTitle>Error</AlertTitle>
                <Stack>
                  {(error as any).response?.data?.data || 'Unknown Error'}
                </Stack>
              </Alert>
            )}
          </Box>
          <LoadingButton
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
            loading={isLoading}
            size="large"
          >
            SIGN IN
          </LoadingButton>
        </Box>
      </Box>
      <Box
        sx={{
          position: 'absolute',
          bottom: 0,
          textAlign: 'center',
          padding: 2,
        }}
      >
        <Copyright />
      </Box>
    </Container>
  );
}

export default SignIn;
