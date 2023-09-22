import React from 'react';
import { Typography, Stack, Card, CardContent, Box } from '@mui/material';
import { SxProps } from '@mui/system';

export interface StatColorCardProps {
  cardColor?: string;
  icon: React.ReactNode;
  iconSx?: SxProps;
  label?: string;
  value?: number;
}

const StatColorCard: React.FC<StatColorCardProps> = ({
  cardColor = '#e2e2e2',
  icon,
  iconSx = { width: 40, height: 40 },
  label = 'Total',
  value = 0,
}) => {
  return (
    <Box>
      <Card
        sx={{
          bgcolor: `${cardColor}`,
          justifyContent: 'center',
          alignItems: 'center',
          m: 0,
        }}
      >
        <CardContent>
          <Stack direction="column" justifyContent="center" alignItems="center">
            {React.cloneElement(icon as React.ReactElement, { sx: iconSx })}
            <Typography variant="body1" fontWeight="bold" mt={1}>
              {label}
            </Typography>
            <Typography variant="h4" fontWeight="bold">
              {value}
            </Typography>
          </Stack>
        </CardContent>
      </Card>
    </Box>
  );
};

export default StatColorCard;
