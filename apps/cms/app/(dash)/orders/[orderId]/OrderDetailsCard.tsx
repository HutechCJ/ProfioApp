import React from 'react';

import { Card, CardContent, Container, Stack, Typography } from '@mui/material';

import InventoryIcon from '@mui/icons-material/Inventory';

interface OrderDetailsCardProps {
  startedDate: string;
  expectedDeliveryTime: string;
  destinationAddress: Nullable<Address>;
  destinationZipCode: string;
  note: string;
  distance: number;
}

const OrderDetailsCard: React.FC<OrderDetailsCardProps> = ({
  startedDate,
  expectedDeliveryTime,
  destinationAddress,
  destinationZipCode,
  note,
  distance,
}) => {
  return (
    <Container>
      <Card
        sx={{
          minHeight: 250,
          bgcolor: '#dd77f2',
          display: 'flex',
          alignItems: 'center',
          mt: 2,
          px: 6,
        }}
      >
        <CardContent>
          <Stack
            direction="row"
            justifyContent="flex-start"
            alignItems="center"
            spacing={8}
          >
            <Stack
              direction="column"
              justifyContent="center"
              alignItems="center"
              spacing={2}
            >
              <Typography variant="h6" gutterBottom>
                Order
              </Typography>
              <InventoryIcon sx={{ width: 150, height: 150 }} />
            </Stack>
            <Stack>
              <Typography variant="h6" gutterBottom>
                Started Date: <strong>{startedDate}</strong>
              </Typography>
              <Typography variant="h6" gutterBottom>
                Expected Delivery Time: <strong>{expectedDeliveryTime}</strong>
              </Typography>
              <Typography variant="h6" gutterBottom>
                Destination Address:{' '}
                <strong>
                  {(() => {
                    const { street, ward, city, province } =
                      destinationAddress || {};
                    const addressParts = [];

                    if (street) addressParts.push(street);
                    if (ward) addressParts.push(ward);
                    if (city) addressParts.push(city);
                    if (province) addressParts.push(province);

                    return addressParts.join(', ');
                  })()}
                </strong>
              </Typography>
              <Typography variant="h6" gutterBottom>
                Destination Zip Code: <strong>{destinationZipCode}</strong>
              </Typography>
              <Typography variant="h6" gutterBottom>
                Note: <strong>{note}</strong>
              </Typography>
              <Typography variant="h6" gutterBottom>
                Distance: <strong>{distance}</strong>
              </Typography>
            </Stack>
          </Stack>
        </CardContent>
      </Card>
    </Container>
  );
};

export default OrderDetailsCard;
