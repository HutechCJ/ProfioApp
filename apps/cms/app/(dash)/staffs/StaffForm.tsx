'use client';

import { TextField } from '@mui/material';

const StaffForm = () => {
  return (
    <>
      <TextField
        autoFocus
        margin="dense"
        id="name"
        label="Email Address"
        type="email"
        fullWidth
        variant="standard"
      />
    </>
  );
};

export default StaffForm;
