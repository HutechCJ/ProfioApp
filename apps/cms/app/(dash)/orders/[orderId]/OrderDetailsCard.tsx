import React from 'react';

import { Card, CardContent, Box, Stack, Typography } from '@mui/material';

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
    <Box>
      <Card
        sx={{
          minHeight: 80,
          height: 230,
          bgcolor: '#e3e3e3',
          display: 'flex',
          justifyContent: 'flex-start',
          alignItems: 'center',
          pt: 2,
          px: 2
        }}
      >
        <CardContent>
          <Stack>
            <Typography variant="h6" fontWeight="bold" gutterBottom>
              • Order • 
            </Typography>
            <Typography variant="body1" gutterBottom>
              Started Date: <strong>{startedDate}</strong>
            </Typography>
            <Typography variant="body1" gutterBottom>
              Expected Delivery Time: <strong>{expectedDeliveryTime}</strong>
            </Typography>
            <Typography variant="body1" gutterBottom>
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
            <Typography variant="body1" gutterBottom>
              Destination Zip Code: <strong>{destinationZipCode}</strong>
            </Typography>
            <Typography variant="body1" gutterBottom>
              Note: <strong>{note}</strong>
            </Typography>
            <Typography variant="body1" gutterBottom>
              Distance: <strong>{distance}</strong>
            </Typography>
          </Stack>
        </CardContent>
      </Card>
    </Box>
  );
};

export default OrderDetailsCard;
