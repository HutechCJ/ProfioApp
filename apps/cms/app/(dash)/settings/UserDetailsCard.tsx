import useUser from '@/features/user/useUser';
import {
  Box,
  Card,
  CardContent,
  CardHeader,
  CircularProgress,
  Divider,
  TextField,
} from '@mui/material';
import React from 'react';

function UserDetailsCard() {
  const { data: user, isLoading } = useUser();

  return (
      <Card sx={{ minHeight: 600 }}>
        <CardHeader title="Your Profile" subheader={`ID: ${user?.id}`} />
        <Divider />
        {isLoading ? (
          <Box
            sx={{
              display: 'flex',
              justifyContent: 'center',
              alignItems: 'center',
              height: '35vh',
            }}
          >
            <CircularProgress />
          </Box>
        ) : (
          <CardContent>
            <Box component="form" noValidate sx={{ mt: 1 }}>
              <TextField
                margin="dense"
                fullWidth
                variant="outlined"
                id="user-username"
                label="Username"
                defaultValue={user?.userName}
                InputProps={{
                  readOnly: true,
                }}
              />
              <TextField
                margin="dense"
                fullWidth
                variant="outlined"
                id="user-email"
                label="Email"
                defaultValue={user?.email}
                InputProps={{
                  readOnly: true,
                }}
              />
              <TextField
                margin="dense"
                fullWidth
                variant="outlined"
                id="user-fullName"
                label="Full Name"
                defaultValue={user?.fullName}
                InputProps={{
                  readOnly: true,
                }}
              />
            </Box>
          </CardContent>
        )}
      </Card>
  );
}

export default UserDetailsCard;
