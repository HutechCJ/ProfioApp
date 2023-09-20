import React from 'react';

import { Card, CardContent, Container, Stack, Typography } from '@mui/material';
import Link from '@/components/Link';

import PersonPinIcon from '@mui/icons-material/PersonPin';
import { Staff } from '@/features/staff/staff.types';

interface VehicleStaffCardProps {
  staff: Nullable<Staff>;
}

const VehicleStaffCard: React.FC<VehicleStaffCardProps> = ({ staff }) => {
  return (
    <Container>
      <Link href={`/staffs/${staff?.id}`} sx={{ textDecoration: 'none' }}>
        <Card
          sx={{
            minHeight: 200,
            height: 300,
            bgcolor: '#5ebc5e',
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
                Staff
              </Typography>
              <PersonPinIcon sx={{ width: 120, height: 120 }} />
              <Typography variant="h4" fontWeight="bold" gutterBottom>
                {staff?.name}
              </Typography>
              <Typography variant="body1" color="#575757" gutterBottom>
                {staff?.id}
              </Typography>
            </Stack>
          </CardContent>
        </Card>
      </Link>
    </Container>
  );
};

export default VehicleStaffCard;
