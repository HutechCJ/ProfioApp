/* eslint-disable react/no-children-prop */
'use client'

import { OrderStatus, Order as OrderType } from '@/features/order/order.types'
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
import _ from 'lodash'
import GoogleMapComponent from './GoogleMap'
import OrderStepper from './OrderStepper'
import { Customer } from '@/features/customer/customer.types'

function Order({ params }: { params: { orderId: string } }) {
    const {
        data: orderApiRes,
        isLoading: orderLoading,
        isError: orderError,
    } = useGetOrder(params.orderId)

    const form = useForm({
        // Memoize your default values to prevent re-renders
        defaultValues: {
            ...orderApiRes?.data,
            destinationAddress: orderApiRes?.data.destinationAddress
                ? getAddressField(orderApiRes?.data.destinationAddress)
                : '',
            customer: orderApiRes?.data.customer?.name || '',
            customerId: orderApiRes?.data.customer?.id || '',
            status: orderApiRes ? OrderStatus[orderApiRes.data.status] : '',
        },
        onSubmit: async (values) => {
            // Do something with form data
            console.log(values)
        },
    })

    function getAddressField(address: Address) {
        return `${address.street} ${address.province} ${address.ward} ${address.city} ${address.zipCode}`
    }

    if (orderLoading) {
        return 'Loading...'
    }

    if (!orderApiRes || orderError) {
        return 'There is an error occurred!'
    }

    return (
        <Container maxWidth="xl" sx={{ '& > :not(style)': { m: 2 } }}>
            <Typography variant="h4" component="h4">
                {`Order #${orderApiRes.data.id}`}
            </Typography>
            <OrderStepper order={orderApiRes.data} />
            <GoogleMapComponent orderId={params.orderId} />
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
                                name={
                                    k as keyof typeof form.options.defaultValues
                                }
                                // onChange={(value: any) =>
                                //     !value
                                //         ? 'A first name is required'
                                //         : value.length < 3
                                //         ? 'First name must be at least 3 characters'
                                //         : undefined
                                // }
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
                                                        e.target.value as never
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
