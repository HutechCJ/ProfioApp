import CopyTextButton from '@/components/CopyTextButton';
import { StaffPosition } from '@/features/staff/staff.types';
import useUser from '@/features/user/useUser';
import {
  Box,
  Card,
  CardContent,
  CardHeader,
  CircularProgress,
  Divider,
  TextField,
  Typography,
} from '@mui/material';
import React from 'react';

function UserDetailsCard() {
  const { data: user, isLoading } = useUser();

  return (
    <Card sx={{ minHeight: 634 }}>
      <CardHeader
        title="Your Profile"
        subheader={`ID: ${user?.id.toUpperCase()}`}
        action={
          <CopyTextButton
            text={user?.id.toUpperCase()}
            titleTooltip="Copy ID"
          />
        }
      />
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
            <Divider sx={{ my: 1 }} />
            <Typography variant="h6">
              Information of staff working at Profio:
            </Typography>
            <Typography variant="body1" color="gray" gutterBottom>
              ID: {user?.staff?.id}
              {user?.staff?.id && <CopyTextButton text={user?.staff?.id} />}
            </Typography>
            <TextField
              margin="dense"
              fullWidth
              variant="outlined"
              id="user-staff-fullName"
              label="Staff's Full Name"
              defaultValue={user?.staff?.name || 'Needs to be updated!'}
              InputProps={{
                readOnly: true,
              }}
            />
            <TextField
              margin="dense"
              fullWidth
              variant="outlined"
              id="user-staff-phone"
              label="Phone"
              defaultValue={user?.staff?.phone || 'Needs to be updated!'}
              InputProps={{
                readOnly: true,
              }}
            />
            <TextField
              margin="dense"
              fullWidth
              variant="outlined"
              id="user-staff-position"
              label="Position"
              defaultValue={
                user?.staff?.position
                  ? StaffPosition[user.staff.position]
                  : 'Needs to be updated!'
              }
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
