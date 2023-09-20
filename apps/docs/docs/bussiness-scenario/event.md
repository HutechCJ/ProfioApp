---
title: Event Storming
description: A workshop for exploring business domains
sidebar_position: 4
---

# Event Storming

<p align="justify">

> Finding events via Usecase. It's a good way to find events in the system and also a good way to find the boundary of the system.

</p>

## Event

### Order Catalog

**Customer** can perform the following actions:

- Create a new order
  - Event associated: OrderCreated
- Get information of an order
  - Event associated: GetOrderById
- Get the delivery progress of an order
  - Event associated: GetDeliveryProgressById

**Officer** and **Stoker** can perform the following actions:

- Get the delivery progress of customer order
  - Event associated:
    - GetDeliveryProgress (paging and filtering)
    - GetDeliveryProgressById
- Manage customer order
  - Event associated:
    - OrderCreated
    - OrderUpdated
    - OrderDeleted
    - GetOrder (paging and filtering)
    - GetOrderById
    - CountOrders
    - GetVehicleByOrderId
    - GetDeliveryByOrderId
    - GetHubPathByOrderId
- Manage customer
  - Event associated:
    - CustomerCreated
    - CustomerUpdated
    - CustomerDeleted
    - GetCustomer (paging and filtering)
    - GetCustomerById

**System** can perform the following actions:

- Send notification to customer
  - Event associated:
    - EmailSent
    - SmsSent

### Driver Catalog

**Sysadmin** can perform the following actions:

- Manage staff
  - Event associated:
    - StaffCreated
    - StaffUpdated
    - StaffDeleted
    - GetStaff (paging and filtering)
    - GetStaffById
    - CountStaffs
    - CountStaffsByPosition
- Manage vehicle
  - Event associated:
    - VehicleCreated
    - VehicleUpdated
    - VehicleDeleted
    - GetVehicle (paging and filtering)
    - GetVehicleById
  - Get delivery
    - Event associated:
      - GetDelivery (paging and filtering)
      - GetDeliveryById
  - Get incident
    - Event associated:
      - GetIncident (paging and filtering)
      - GetIncidentById
      - CountIncidents

**Driver** and **Shipper** can perform the following actions:

- Get vehicle information
  - Event associated:
    - GetVehicleById
- Make a delivery
  - Event associated:
    - DeliveryCreated
    - DeliveryUpdated
    - DeliveryDeleted
    - GetDeliveryById
- Make an incident report
  - Event associated:
    - IncidentCreated
    - IncidentUpdated
    - IncidentDeleted
    - GetIncidentById

**System** can perform the following actions:

- Send notification to driver
  - Event associated:
    - EmailSent
    - SmsSent
    - NotificationSent

### Hub Management

**Sysadmin** can perform the following actions:

- Manage hub
  - Event associated:
    - HubCreated
    - HubUpdated
    - HubDeleted
    - GetHub (paging and filtering)
    - GetHubById
- Manage route
  - Event associated:
    - RouteCreated
    - RouteUpdated
    - RouteDeleted
    - GetRoute (paging and filtering)
    - GetRouteById
- Manage order phase
  - Event associated:
    - OrderPhaseCreated
    - OrderPhaseUpdated
    - OrderPhaseDeleted
    - GetOrderPhase (paging and filtering)
    - GetOrderPhaseById

**System** can perform the following actions:

- Calculate the route between hubs
  - Event associated:
    - RouteCreated
    - RouteUpdated
- Generate order phase:
  - Event associated:
    - OrderPhaseCreated
- Get nearest hub
  - Event associated:
    - GetNearestHub

### Access Control

**Officer**, **Shipper**, **Driver** and **Stoker** can perform the following actions:

- Authenticate
  - Event associated:
    - Login
    - Logout
    - ChangePassword

**Sysadmin** can perform the following actions:

- Create a new account
  - Event associated:
    - AccountCreated
    - AccountUpdated
    - AccountDeleted
    - GetAccount (paging and filtering)
    - GetAccountById

## Collection of Domain Events

<img
loading="lazy"
src={require('../../static/img/event/domain-event.png').default}
alt="Profio Introduction"
style={{ width: '100%', height: 'auto' }}
/>

<p align="justify">
	In the above diagram, we can see that the domain events are listed in the middle of the diagram. The domain events are the events that are raised by the domain objects. The domain objects are the objects that are part of the domain model. The domain model is the model that is used to represent the domain.
</p>

## Bounded Context

<img
loading="lazy"
src={require('../../static/img/event/bounded-context.png').default}
alt="Profio Introduction"
style={{ width: '100%', height: 'auto' }}
/>

<p align="justify">
	In the provided diagram, it's apparent that the domain events are closely tied to the domain model, indicating a strong relationship between the events and the core functionality of the system.
</p>

## Aggregate

<img
loading="lazy"
src={require('../../static/img/event/aggerateroot.png').default}
alt="Profio Introduction"
style={{ width: '100%', height: 'auto' }}
/>

<p align="justify">
	In the above diagram, we can see that the aggregate root is the domain object that is responsible for the domain events. The domain events are the events that are raised by the domain objects. The domain objects are the objects that are part of the domain model. The domain model is the model that is used to represent the domain.
</p>
