'use client';

import useGetOrderHubsPath from '@/features/order/useGetOrderHubsPath';
import { Box, Alert, Stack, Button } from '@mui/material';
import {
  DirectionsRenderer,
  DirectionsService,
  GoogleMap,
  Marker,
  useJsApiLoader,
  Polyline,
} from '@react-google-maps/api';
import React from 'react';
import useSignalR from '@/common/hooks/useSignalR';
import { HubConnectionState } from '@microsoft/signalr';

const containerStyle = {
  width: '100%',
  height: '400px',
  border: 0,
  borderRadius: 10,
};

const center: google.maps.LatLngLiteral = {
  lat: 14.0583,
  lng: 108.2772,
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

  const { connection } = useSignalR(`/ws/current-location?orderId=${orderId}`);

  const [map, setMap] = React.useState<google.maps.Map | null>(null);
  const [directionResponse, setDirectionResponse] =
    React.useState<google.maps.DirectionsResult | null>(null);
  const [orderLocation, setOrderLocation] = React.useState<
    google.maps.LatLngLiteral | google.maps.LatLng | null
  >(null);
  const [directionsServiceOptions, setDirectionsServiceOptions] =
    React.useState<google.maps.DirectionsRequest | null>(null);
  const [orderLocationPolylines, setOrderLocationPolylines] = React.useState<
    google.maps.LatLng[]
  >([]);

  const onLoad = React.useCallback(function callback(map: google.maps.Map) {
    setMap(map);
  }, []);

  const onUnmount = React.useCallback(function callback(map: google.maps.Map) {
    setMap(null);
  }, []);

  const directionsCallback = React.useCallback(
    (
      result: google.maps.DirectionsResult | null,
      status: google.maps.DirectionsStatus
    ) => {
      if (result !== null) {
        if (status === 'OK') {
          setDirectionResponse(result);
        } else {
          console.error('Direction Response: ', result);
        }
      }
    },
    []
  );

  const trackCurrentOrderLocation = React.useCallback(() => {
    if (!map || !orderLocation) return;
    const bounds = new google.maps.LatLngBounds(orderLocation);
    map.fitBounds(bounds);
    map.setCenter(orderLocation);
    map.setZoom(8);
  }, [map, orderLocation]);

  const directionsResult = React.useMemo(() => {
    return {
      directions: directionResponse,
    };
  }, [directionResponse]);

  React.useEffect(() => {
    if (
      map &&
      orderHubsPathApiRes &&
      orderHubsPathApiRes.data.items[0].location &&
      orderHubsPathApiRes.data.items[1].location
    ) {
      const bounds = new window.google.maps.LatLngBounds();
      const origin = new window.google.maps.LatLng(
        orderHubsPathApiRes.data.items[0].location.latitude,
        orderHubsPathApiRes.data.items[0].location.longitude
      );
      const destination = new window.google.maps.LatLng(
        orderHubsPathApiRes.data.items[1].location.latitude,
        orderHubsPathApiRes.data.items[1].location.longitude
      );

      setDirectionsServiceOptions({
        origin,
        destination,
        travelMode: google.maps.TravelMode.DRIVING,
      });

      bounds.extend(origin);
      bounds.extend(destination);

      map.fitBounds(bounds);
    }
  }, [map, orderHubsPathApiRes]);

  React.useEffect(() => {
    connection.on('SendLocation', (message: VehicleLocation) => {
      setOrderLocation((oldLocation) => {
        const newLocation = new window.google.maps.LatLng(
          message.latitude,
          message.longitude
        );

        if (oldLocation !== null) {
          setOrderLocationPolylines((oldPoly) => [...oldPoly, newLocation]);
        }

        return newLocation;
      });
    });

    return () => {
      connection.off('SendLocation');
    };
  }, [connection, map]);

  if (
    !orderHubsPathApiRes ||
    orderHubsPathApiRes.data.items.length <= 0 ||
    hubsPathLoading ||
    hubsPathError
  )
    return null;

  return (
    <Box>
      <Stack sx={{ mb: 2 }} spacing={1}>
        {isLoaded && map && !directionsResult.directions && (
          <Alert severity="error">
            Direction Service is not available at this time
          </Alert>
        )}
        {connection.state !== HubConnectionState.Connected && (
          <Alert
            severity={
              connection.state === HubConnectionState.Disconnected
                ? 'error'
                : 'warning'
            }
          >{`${HubConnectionState[connection.state]} to server`}</Alert>
        )}
        {orderLocation && (
          <Button variant="contained" onClick={trackCurrentOrderLocation}>
            Find order location
          </Button>
        )}
      </Stack>
      {isLoaded && (
        <GoogleMap
          center={center}
          mapContainerStyle={containerStyle}
          zoom={5}
          onLoad={onLoad}
          onUnmount={onUnmount}
        >
          <>
            {directionsServiceOptions &&
              orderHubsPathApiRes.data.items[0].location !== null &&
              orderHubsPathApiRes.data.items[1].location !== null && (
                <DirectionsService
                  options={directionsServiceOptions}
                  callback={directionsCallback}
                />
              )}
            {directionsResult.directions && (
              <DirectionsRenderer
                options={{
                  suppressMarkers: true,
                }}
                directions={directionsResult.directions}
              />
            )}
            {orderLocation !== null && (
              <Marker
                position={orderLocation}
                icon={'https://img.icons8.com/color/48/truck--v1.png'}
                title={'Your Order Current Location'}
              />
            )}
            <Polyline
              options={{
                geodesic: true,
                strokeColor: '#FF0000',
                strokeOpacity: 1.0,
                strokeWeight: 2,
              }}
              path={orderLocationPolylines}
            />
            {orderHubsPathApiRes.data.items.map((hub, i) => {
              return (
                <Marker
                  key={hub.id}
                  position={{
                    lat: hub.location?.latitude ?? 0,
                    lng: hub.location?.longitude ?? 0,
                  }}
                  icon={`${
                    i > 0
                      ? 'https://img.icons8.com/stickers/48/marker-b.png'
                      : 'https://img.icons8.com/stickers/48/marker-a.png'
                  }`}
                  title={`${hub.id}`}
                />
              );
            })}
          </>
        </GoogleMap>
      )}
    </Box>
  );
}

export default GoogleMapComponent;
