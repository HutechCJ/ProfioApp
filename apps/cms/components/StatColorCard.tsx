import React from 'react';
import { Typography, Stack, Container, Card, CardContent } from '@mui/material';
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
  iconSx = { width: 60, height: 60 },
  label = 'Total',
  value = 0,
}) => {
  return (
    <Container>
      <Card
        sx={{
          bgcolor: `${cardColor}`,
          justifyContent: 'center',
          alignItems: 'center',
        }}
      >
        <CardContent>
          <Stack direction="column" justifyContent="center" alignItems="center">
            {React.cloneElement(icon as React.ReactElement, { sx: iconSx })}
            <Typography variant="h6" mt={1}>
              {label}
            </Typography>
            <Typography variant="h2" fontWeight="bold">
              {value}
            </Typography>
          </Stack>
        </CardContent>
      </Card>
    </Container>
  );
};

export default StatColorCard;
