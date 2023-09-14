'use client';

import { GoogleMap, useJsApiLoader } from '@react-google-maps/api';
import {
  Box,
  Stack,
  Button,
  Input,
  InputLabel,
  FormControl,
  Container,
  Paper,
  Typography,
  Stepper,
} from '@mui/material';
import React from 'react';
import useGetOrderHubsPath from '@/features/order/useGetOrderHubsPath';

const containerStyle = {
  width: '100%',
  height: '400px',
};

const center = {
  lat: -3.745,
  lng: -38.523,
};

function GoogleMapComponent({ orderId }: { orderId: string }) {
  const { isLoaded } = useJsApiLoader({
    id: 'google-map-script',
    googleMapsApiKey: `${process.env.NEXT_PUBLIC_GOOGLE_MAP_API_KEY}`,
  });

  const {
    data: orderHubsPathApiRes,
    isLoading: hubsPathLoading,
    isError: hubsPathError,
  } = useGetOrderHubsPath(orderId);

  const [map, setMap] = React.useState(null);

  const onLoad = React.useCallback(function callback(map: any) {
    // This is just an example of getting and using the map instance!!! don't just blindly copy!
    const bounds = new window.google.maps.LatLngBounds(center);
    map.fitBounds(bounds);

    setMap(map);
  }, []);

  const onUnmount = React.useCallback(function callback(map: any) {
    setMap(null);
  }, []);

  return (
    <Box>
      {
        // isLoaded && (
        //     <GoogleMap
        //         mapContainerStyle={containerStyle}
        //         center={center}
        //         zoom={10}
        //         onLoad={onLoad}
        //         onUnmount={onUnmount}
        //     >
        //         {/* Child components, such as markers, info windows, etc. */}
        //         <></>
        //     </GoogleMap>
        // )
      }
      {orderHubsPathApiRes && (
        <iframe
          width="100%"
          height="500"
          frameBorder="0"
          style={{
            border: 0,
            borderRadius: 10,
          }}
          allowFullScreen
          referrerPolicy="no-referrer-when-downgrade"
          src={`https://www.google.com/maps/embed/v1/directions?${new URLSearchParams(
            {
              region: 'VN',
              key: `${process.env.NEXT_PUBLIC_GOOGLE_MAP_API_KEY}`,
              origin: `${
                orderHubsPathApiRes.data.items[0].location?.latitude ?? 0
              },${orderHubsPathApiRes.data.items[0].location?.longitude ?? 0}`,
              destination: `${
                orderHubsPathApiRes.data.items[1].location?.latitude ?? 0
              },${orderHubsPathApiRes.data.items[1].location?.longitude ?? 0}`,
            },
          )}`}
        ></iframe>
      )}
    </Box>
  );
}

export default GoogleMapComponent;
