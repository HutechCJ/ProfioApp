import React from 'react';

import { Card, CardContent, Container, Stack, Typography } from '@mui/material';
import Link from '@/components/Link';

import PersonPinIcon from '@mui/icons-material/PersonPin';
import { Customer, Gender } from '@/features/customer/customer.types';

interface OrderCustomerCardProps {
  customer: Nullable<Customer>;
}

const OrderCustomerCard: React.FC<OrderCustomerCardProps> = ({ customer }) => {
  return (
    <Container>
      <Link href={`/customers/${customer?.id}`} sx={{ textDecoration: 'none' }}>
        <Card
          sx={{
            minHeight: 275,
            height: 350,
            bgcolor: 'orange',
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
              <Typography variant="h6" gutterBottom>
                Customer
              </Typography>
              <PersonPinIcon sx={{ width: 150, height: 150 }} />
              <Typography variant="h4" fontWeight="bold">
                {customer?.name} ({Gender[customer?.gender || 'Other']})
              </Typography>
              <Typography variant="h6" fontWeight="bold" gutterBottom>
                Phone: {customer?.phone}
              </Typography>
              <Typography variant="body1" color="#575757" gutterBottom>
                {customer?.id}
              </Typography>
            </Stack>
          </CardContent>
        </Card>
      </Link>
    </Container>
  );
};

export default OrderCustomerCard;
