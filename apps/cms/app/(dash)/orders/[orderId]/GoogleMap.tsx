'use client'

import {
    GoogleMap,
    useJsApiLoader,
    Marker,
    DirectionsRenderer,
    DirectionsService,
} from '@react-google-maps/api'
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
} from '@mui/material'
import React from 'react'
import useGetOrderHubsPath from '@/features/order/useGetOrderHubsPath'

const containerStyle = {
    width: '100%',
    height: '400px',
    border: 0,
    borderRadius: 10,
}

function GoogleMapComponent({ orderId }: { orderId: string }) {
    const { isLoaded } = useJsApiLoader({
        id: 'google-map-script',
        googleMapsApiKey: `${process.env.NEXT_PUBLIC_GOOGLE_MAP_API_KEY}`,
    })

    const {
        data: orderHubsPathApiRes,
        isLoading: hubsPathLoading,
        isError: hubsPathError,
    } = useGetOrderHubsPath(orderId)

    const [map, setMap] = React.useState(null)
    const [directionResponse, setDirectionResponse] =
        React.useState<google.maps.DirectionsResult | null>(null)

    const onLoad = React.useCallback(function callback(map: any) {
        const vietnam = new window.google.maps.LatLng(14.0583, 108.2772)
        const bounds = new window.google.maps.LatLngBounds(vietnam)
        map.fitBounds(bounds)

        setMap(map)
    }, [])

    const onUnmount = React.useCallback(function callback(map: any) {
        setMap(null)
    }, [])

    const directionsCallback = React.useCallback(
        (
            result: google.maps.DirectionsResult | null,
            status: google.maps.DirectionsStatus
        ) => {
            if (result !== null) {
                if (status === 'OK') {
                    setDirectionResponse(result)
                } else {
                    console.error('Direction Response: ', result)
                }
            }
        },
        []
    )

    const directionsResult = React.useMemo(() => {
        return directionResponse
    }, [directionResponse])

    return (
        <Box
            sx={{
                width: '100%',
            }}
        >
            {isLoaded && orderHubsPathApiRes && (
                <GoogleMap
                    mapContainerStyle={containerStyle}
                    // center={center}
                    zoom={5}
                    onLoad={onLoad}
                    onUnmount={onUnmount}
                >
                    {/* Child components, such as markers, info windows, etc. */}
                    <>
                        <DirectionsService
                            options={{
                                origin: new window.google.maps.LatLng(
                                    orderHubsPathApiRes.data.items[0].location
                                        ?.latitude || 0,
                                    orderHubsPathApiRes.data.items[0].location
                                        ?.longitude || 0
                                ),
                                destination: new window.google.maps.LatLng(
                                    orderHubsPathApiRes.data.items[1].location
                                        ?.latitude || 0,
                                    orderHubsPathApiRes.data.items[1].location
                                        ?.longitude || 0
                                ),
                                travelMode: google.maps.TravelMode.DRIVING,
                                region: "VN"
                            }}
                            callback={directionsCallback}
                        />
                        <DirectionsRenderer
                            directions={directionsResult ?? undefined}
                        />
                        {orderHubsPathApiRes.data.items.map((hub) => {
                            return (
                                <Marker
                                    key={hub.id}
                                    position={{
                                        lat: hub.location?.latitude || 0,
                                        lng: hub.location?.longitude || 0,
                                    }}
                                    label={hub.zipCode}
                                />
                            )
                        })}
                    </>
                </GoogleMap>
            )}
            {/* {orderHubsPathApiRes && (
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
                                orderHubsPathApiRes.data.items[0].location
                                    ?.latitude ?? 0
                            },${
                                orderHubsPathApiRes.data.items[0].location
                                    ?.longitude ?? 0
                            }`,
                            destination: `${
                                orderHubsPathApiRes.data.items[1].location
                                    ?.latitude ?? 0
                            },${
                                orderHubsPathApiRes.data.items[1].location
                                    ?.longitude ?? 0
                            }`,
                        }
                    )}`}
                ></iframe>
            )} */}
        </Box>
    )
}

export default GoogleMapComponent
