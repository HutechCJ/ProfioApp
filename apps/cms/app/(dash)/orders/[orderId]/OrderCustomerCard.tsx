import React from 'react';

import { Card, CardContent, Box, Stack, Typography } from '@mui/material';
import Link from '@/components/Link';

import PersonPinIcon from '@mui/icons-material/PersonPin';
import { Customer, Gender } from '@/features/customer/customer.types';

interface OrderCustomerCardProps {
  customer: Nullable<Customer>;
}

const OrderCustomerCard: React.FC<OrderCustomerCardProps> = ({ customer }) => {
  return (
    <Box>
      <Link href={`/customers/${customer?.id}`} sx={{ textDecoration: 'none' }}>
        <Card
          sx={{
            height: 290,
            bgcolor: '#e3e3e3',
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
          }}
        >
          <CardContent>
            <Stack
              sx={{
                textAlign: 'center',
                justifyContent: 'center',
                alignItems: 'center',
              }}
            >
              <Typography variant="body1" gutterBottom>
                Customer
              </Typography>
              <PersonPinIcon sx={{ width: 50, height: 50 }} />
              <Typography variant="h6" fontWeight="bold">
                {customer?.name} ({Gender[customer?.gender || 'Other']})
              </Typography>
              <Typography variant="body1" fontWeight="bold" gutterBottom>
                Phone: {customer?.phone}
              </Typography>
              <Typography variant="body1" color="#575757" gutterBottom>
                {customer?.id}
              </Typography>
            </Stack>
          </CardContent>
        </Card>
      </Link>
    </Box>
  );
};

export default OrderCustomerCard;
