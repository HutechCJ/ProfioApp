import React from 'react';

import { Card, CardContent, Box, Stack, Typography } from '@mui/material';

import PauseCircleFilledIcon from '@mui/icons-material/PauseCircleFilled';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import ArchiveIcon from '@mui/icons-material/Archive';
import CancelIcon from '@mui/icons-material/Cancel';
import { OrderStatus } from '@/features/order/order.types';

const statuses = [
  {
    value: OrderStatus.Pending,
    color: '#e2e2e2',
    icon: <PauseCircleFilledIcon sx={{ width: 50, height: 50 }} />,
  },
  {
    value: OrderStatus.InProgress,
    color: '#ed9a56',
    icon: <LocalShippingIcon sx={{ width: 50, height: 50 }} />,
  },
  {
    value: OrderStatus.Completed,
    color: '#61b2de',
    icon: <CheckCircleIcon sx={{ width: 50, height: 50 }} />,
  },
  {
    value: OrderStatus.Received,
    color: '#579f5a',
    icon: <ArchiveIcon sx={{ width: 50, height: 50 }} />,
  },
  {
    value: OrderStatus.Cancelled,
    color: '#d36363',
    icon: <CancelIcon sx={{ width: 50, height: 50 }} />,
  },
];

interface OrderStatusCardProps {
  status: OrderStatus;
}

const OrderStatusCard: React.FC<OrderStatusCardProps> = ({ status }) => {
  return (
    <Box>
      <Card
        sx={{
          minHeight: 290,
          bgcolor: statuses.find((s) => s.value === status)?.color || '#e2e2e2',
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
        }}
      >
        <CardContent sx={{ textAlign: 'center' }}>
          <Stack
            sx={{
              textAlign: 'center',
              justifyContent: 'center',
              alignItems: 'center',
            }}
          >
            <Typography variant="body1" gutterBottom>
              Status
            </Typography>
            {statuses.find((s) => s.value === status)?.icon || (
              <PauseCircleFilledIcon sx={{ width: 50, height: 50 }} />
            )}
            <Typography variant="h6" fontWeight="bold" mt={1} gutterBottom>
              {OrderStatus[status]}
            </Typography>
          </Stack>
        </CardContent>
      </Card>
    </Box>
  );
};

export default OrderStatusCard;
