/* eslint-disable react/no-children-prop */
'use client'

import { OrderStatus } from '@/features/order/order.types'
import useGetOrder from '@/features/order/useGetOrder'
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
import LocalShippingIcon from '@mui/icons-material/LocalShipping'
import { useForm } from '@tanstack/react-form'
import React from 'react'
import { GoogleMap, useJsApiLoader } from '@react-google-maps/api'
import _ from 'lodash'

const containerStyle = {
    width: '100%',
    height: '400px',
}

const center = {
    lat: -3.745,
    lng: -38.523,
}

function Order({ params }: { params: { orderId: string } }) {
    const {
        data: orderApiRes,
        isLoading,
        isError,
    } = useGetOrder(params.orderId)

    const form = useForm({
        // Memoize your default values to prevent re-renders
        defaultValues: {
            ...orderApiRes?.data,
        },
        onSubmit: async (values) => {
            // Do something with form data
            console.log(values)
        },
    })

    const { isLoaded } = useJsApiLoader({
        id: 'google-map-script',
        googleMapsApiKey: '',
    })

    const [map, setMap] = React.useState(null)

    const onLoad = React.useCallback(function callback(map: any) {
        // This is just an example of getting and using the map instance!!! don't just blindly copy!
        const bounds = new window.google.maps.LatLngBounds(center)
        map.fitBounds(bounds)

        setMap(map)
    }, [])

    const onUnmount = React.useCallback(function callback(map: any) {
        setMap(null)
    }, [])

    if (isLoading) {
        return 'Loading...'
    }

    if (!orderApiRes || isError) {
        return 'There is an error occurred!'
    }

    return (
        <Container maxWidth="xl" sx={{ '& > :not(style)': { m: 2 } }}>
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
                //         <Box
                //             sx={{
                //                 zIndex: 99,
                //             }}
                //         >
                //             <Typography variant="h1" component="h1">
                //                 Hello
                //             </Typography>
                //         </Box>
                //     </GoogleMap>
                // )
            }
            <Box>
                <iframe
                    width="450"
                    height="250"
                    frameBorder="0"
                    style={{
                        border: 0,
                    }}
                    allowFullScreen
                    referrerPolicy="no-referrer-when-downgrade"
                    src={`http://localhost:4200/api/maps/directions?origin=Oslo+Norway&destination=Telemark+Norway&avoid=tolls|highways`}
                ></iframe>
            </Box>
            <form.Provider>
                <Paper
                    sx={{
                        paddingX: 4,
                        paddingY: 2,
                        width: '100%',
                    }}
                >
                    {Object.keys(orderApiRes.data).map((k) => (
                        <Box
                            key={`formField_${k}`}
                            sx={{
                                marginY: 2,
                            }}
                        >
                            <form.Field
                                name={k}
                                onChange={(value: any) =>
                                    !value
                                        ? 'A first name is required'
                                        : value.length < 3
                                        ? 'First name must be at least 3 characters'
                                        : undefined
                                }
                                onChangeAsyncDebounceMs={500}
                                onChangeAsync={async (value: any) => {
                                    await new Promise((resolve) =>
                                        setTimeout(resolve, 1000)
                                    )
                                    return (
                                        value.includes('error') &&
                                        'No "error" allowed in first name'
                                    )
                                }}
                                children={(field) => {
                                    return (
                                        <FormControl fullWidth>
                                            <InputLabel htmlFor={field.name}>
                                                {_.startCase(field.name)}
                                            </InputLabel>
                                            <Input
                                                name={field.name}
                                                value={field.state.value as any}
                                                onBlur={field.handleBlur}
                                                onChange={(e) =>
                                                    field.handleChange(
                                                        e.target.value
                                                    )
                                                }
                                            />
                                            {/* <FieldInfo field={field} /> */}
                                        </FormControl>
                                    )
                                }}
                            />
                        </Box>
                    ))}
                </Paper>
                <Stack direction="row" spacing={1} sx={{ mb: 1 }}>
                    <Button>Update</Button>
                    <Button>Delete</Button>
                </Stack>
            </form.Provider>
        </Container>
    )
}

export default Order
