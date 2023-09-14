// 'use client'

import { Order, OrderStatus } from '@/features/order/order.types';
import useGetOrderDeliveries from '@/features/order/useGetOrderDeliveries';
import { Card, CardContent, Step, Stepper, StepLabel } from '@mui/material';
import React from 'react';

const steps = [
  {
    label: 'On Received',
    type: OrderStatus.Pending,
  },
  {
    label: 'Order Delivering',
    type: OrderStatus.InProgress,
  },
  {
    label: 'On Shipping',
    type: OrderStatus.Completed,
  },
  {
    label: 'Order Delivered',
    type: OrderStatus.Received,
  },
];

function OrderStepper({ order }: { order: Order }) {
  // const { data, isLoading, isError } = useGetOrderDeliveries(order.id)

  // const getActiveStep = () => {
  //     switch (order.status) {
  //         case OrderStatus.Pending:
  //             break
  //         case OrderStatus.InProgress:
  //             break
  //         case OrderStatus.Completed:
  //             break
  //         case OrderStatus.Received:
  //             break
  //         case OrderStatus.Cancelled:
  //             break
  //         default:
  //             break
  //     }
  // }

  return (
    <Card
      sx={{
        width: '100%',
      }}
    >
      <CardContent>
        <Stepper
          activeStep={
            order.status === OrderStatus.Cancelled
              ? 0
              : steps.findIndex((step) => step.type === order.status) + 1
          }
          alternativeLabel
        >
          {steps.map((step) => {
            if (
              order.status > step.type &&
              order.status !== OrderStatus.Cancelled
            ) {
              if (step.type === OrderStatus.Pending) {
                step.label = 'Order Processed';
              }
            }

            if (
              order.status === OrderStatus.Cancelled &&
              step.type === OrderStatus.Pending
            ) {
              step.label = 'Cancelled';
            }

            return (
              <Step key={step.label}>
                <StepLabel
                  error={
                    order.status === OrderStatus.Cancelled &&
                    step.type === OrderStatus.Pending
                  }
                >
                  {step.label}
                </StepLabel>
              </Step>
            );
          })}
        </Stepper>
      </CardContent>
    </Card>
  );
}

export default OrderStepper;
