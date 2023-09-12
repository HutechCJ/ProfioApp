import React from 'react';
import { Typography, Stack, Avatar } from '@mui/material';
import { SxProps } from '@mui/system';

export interface StatProps {
  icon: React.ReactNode;
  iconSx?: SxProps;
  iconColor?: string;
  label?: string;
  value?: number;
  description?: string;
}

const Stat: React.FC<StatProps> = ({
  icon,
  iconSx = { width: 60, height: 60 },
  iconColor = 'blue',
  label = '',
  value = 0,
  description = '',
}) => {
  return (
    <Stack
      direction="row"
      justifyContent="center"
      alignItems="center"
      spacing={2}
    >
      <Avatar sx={{ bgcolor: `${iconColor}`, width: 100, height: 100 }}>
        {React.cloneElement(icon as React.ReactElement, { sx: iconSx })}
      </Avatar>
      <Stack>
        <Typography variant="body1">{label}</Typography>
        <Typography variant="h2" fontWeight="bold">
          {value}
        </Typography>
        <Typography variant="body1" color="gray">
          {description}
        </Typography>
      </Stack>
    </Stack>
  );
};

export default Stat;
