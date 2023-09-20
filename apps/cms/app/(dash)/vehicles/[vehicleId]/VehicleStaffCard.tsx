import React from 'react';

import { Card, CardContent, Box, Stack, Typography } from '@mui/material';
import Link from '@/components/Link';

import PersonPinIcon from '@mui/icons-material/PersonPin';
import { Staff } from '@/features/staff/staff.types';

interface VehicleStaffCardProps {
  staff: Nullable<Staff>;
}

const VehicleStaffCard: React.FC<VehicleStaffCardProps> = ({ staff }) => {
  return (
    <Box>
      <Link href={`/staffs/${staff?.id}`} sx={{ textDecoration: 'none' }}>
        <Card
          sx={{
            minHeight: 80,
            height: 200,
            bgcolor: '#e3e3e3',
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            pt: 2,
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
                Staff
              </Typography>
              <PersonPinIcon sx={{ width: 50, height: 50 }} />
              <Typography variant="h6" fontWeight="bold">
                {staff?.name}
              </Typography>
              <Typography variant="body1" fontWeight="bold" gutterBottom>
                Phone: {staff?.phone}
              </Typography>
              <Typography variant="body1" color="#575757" gutterBottom>
                {staff?.id}
              </Typography>
            </Stack>
          </CardContent>
        </Card>
      </Link>
    </Box>
  );
};

export default VehicleStaffCard;
