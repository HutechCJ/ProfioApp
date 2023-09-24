'use client';

import useUser from '@/features/user/useUser';
import { Typography, Divider, Skeleton } from '@mui/material';

function UserGreeting() {
  const { data: user, isLoading } = useUser();
  if (!user) return null;
  return (
    <>
      {isLoading ? (
        <Skeleton sx={{ bgcolor: 'grey.800' }} width={150} height={40} />
      ) : (
        <Typography variant="h5" component="h5" mt={2}>
          {`Hello ${user.fullName}!`}
        </Typography>
      )}

      <Divider sx={{ marginBottom: 2 }} />
    </>
  );
}

export default UserGreeting;
