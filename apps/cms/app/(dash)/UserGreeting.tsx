'use client';

import useUser from '@/features/user/useUser';
import { Typography, Divider, Skeleton, Box, Stack } from '@mui/material';
import DigitalClock from './DigitalClock';

function UserGreeting() {
  const { data: user, isLoading } = useUser();
  if (!user) return null;
  return (
    <Box sx={{ my: 2 }}>
      <Stack
        direction={{ sx: 'column', md: 'row' }}
        justifyContent="space-between"
        alignItems="center"
        spacing={2}
      >
        <Box>
          {isLoading ? (
            <Skeleton sx={{ bgcolor: 'grey.800' }} width={150} height={40} />
          ) : (
            <Typography variant="h5" component="h5" mt={2} gutterBottom>
              {`Hello ${user.fullName}!`}
            </Typography>
          )}
        </Box>
        <DigitalClock />
      </Stack>
      <Divider sx={{ marginY: 2 }} />
    </Box>
  );
}

export default UserGreeting;
