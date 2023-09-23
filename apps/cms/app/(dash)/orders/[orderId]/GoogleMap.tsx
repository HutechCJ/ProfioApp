'use client';

import useGetOrderHubsPath from '@/features/order/useGetOrderHubsPath';
import { Box } from '@mui/material';
import PlaceIcon from '@mui/icons-material/Place';
import {
  DirectionsRenderer,
  DirectionsService,
  GoogleMap,
  Marker,
  useJsApiLoader,
  InfoBox,
  InfoWindow,
  MarkerProps,
} from '@react-google-maps/api';
import React from 'react';

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

  const [map, setMap] = React.useState<any>(null);
  const [directionResponse, setDirectionResponse] =
    React.useState<google.maps.DirectionsResult | null>(null);

  const onLoad = React.useCallback(function callback(map: any) {
    const vietnam = new window.google.maps.LatLng(14.0583, 108.2772);

    setMap(map);
  }, []);

  const onUnmount = React.useCallback(function callback(map: any) {
    setMap(null);
  }, []);

  const directionsCallback = React.useCallback(
    (
      result: google.maps.DirectionsResult | null,
      status: google.maps.DirectionsStatus,
    ) => {
      console.log(result);
      if (result !== null) {
        if (status === 'OK') {
          setDirectionResponse(result);
        } else {
          console.error('Direction Response: ', result);
        }
      }
    },
    [],
  );

  const directionsResult = React.useMemo(() => {
    return {
      directions: directionResponse,
    };
  }, [directionResponse]);

  React.useEffect(() => {
    (async () => {
      if (
        orderHubsPathApiRes &&
        orderHubsPathApiRes.data.items[0].location &&
        orderHubsPathApiRes.data.items[1].location
      ) {
        const bounds = new window.google.maps.LatLngBounds();
        const origin = new window.google.maps.LatLng(
          orderHubsPathApiRes.data.items[0].location.latitude,
          orderHubsPathApiRes.data.items[0].location.longitude,
        );
        const destination = new window.google.maps.LatLng(
          orderHubsPathApiRes.data.items[1].location.latitude,
          orderHubsPathApiRes.data.items[1].location.longitude,
        );

        bounds.extend(origin);
        bounds.extend(destination);

        map.fitBounds(bounds);
      }
    })();
  }, [map, orderHubsPathApiRes]);

  return (
    <Box
      sx={{
        width: '100%',
      }}
    >
      {isLoaded &&
        map &&
        !directionsResult.directions &&
        'Direction Service is not available at this time.'}
      {isLoaded && (
        <GoogleMap
          center={center}
          mapContainerStyle={containerStyle}
          zoom={5}
          onLoad={onLoad}
          onUnmount={onUnmount}
        >
          {/* Child components, such as markers, info windows, etc. */}
          <>
            {orderHubsPathApiRes &&
              orderHubsPathApiRes.data.items[0].location !== null &&
              orderHubsPathApiRes.data.items[1].location !== null && (
                <DirectionsService
                  options={{
                    origin: new window.google.maps.LatLng(
                      orderHubsPathApiRes.data.items[0].location.latitude,
                      orderHubsPathApiRes.data.items[0].location.longitude,
                    ),
                    destination: new window.google.maps.LatLng(
                      orderHubsPathApiRes.data.items[1].location.latitude,
                      orderHubsPathApiRes.data.items[1].location.longitude,
                    ),
                    travelMode: google.maps.TravelMode.DRIVING,
                  }}
                  callback={directionsCallback}
                />
              )}
            {directionsResult.directions && (
              <DirectionsRenderer directions={directionsResult.directions} />
            )}
            {orderHubsPathApiRes &&
              orderHubsPathApiRes.data.items.map((hub) => {
                return (
                  <Marker
                    key={hub.id}
                    position={{
                      lat: hub.location?.latitude || 0,
                      lng: hub.location?.longitude || 0,
                    }}
                    // icon={`https://img.icons8.com/emoji/48/round-pushpin-emoji.png`}
                    title={`${hub.id}`}
                    label={` ${hub.zipCode}`}
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
