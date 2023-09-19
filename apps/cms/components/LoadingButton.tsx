import { Button, ButtonProps, CircularProgress } from '@mui/material';
import React from 'react';

function LoadingButton({
  loading = false,
  children,
  ...props
}: ButtonProps & { loading?: boolean }) {
  return (
    <Button disabled={loading} {...props}>
      {loading ? <CircularProgress size={30} /> : children}
    </Button>
  );
}

export default LoadingButton;
