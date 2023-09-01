﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#pragma warning disable 219, 612, 618
#nullable enable

namespace Profio.Infrastructure.Persistence.Optimization
{
    public partial class ApplicationDbContextModel
    {
        partial void Initialize()
        {
            var identityRole = IdentityRoleEntityType.Create(this);
            var identityRoleClaim = IdentityRoleClaimEntityType.Create(this);
            var identityUserClaim = IdentityUserClaimEntityType.Create(this);
            var identityUserLogin = IdentityUserLoginEntityType.Create(this);
            var identityUserRole = IdentityUserRoleEntityType.Create(this);
            var identityUserToken = IdentityUserTokenEntityType.Create(this);
            var customer = CustomerEntityType.Create(this);
            var delivery = DeliveryEntityType.Create(this);
            var deliveryProgress = DeliveryProgressEntityType.Create(this);
            var hub = HubEntityType.Create(this);
            var incident = IncidentEntityType.Create(this);
            var order = OrderEntityType.Create(this);
            var orderHistory = OrderHistoryEntityType.Create(this);
            var route = RouteEntityType.Create(this);
            var staff = StaffEntityType.Create(this);
            var vehicle = VehicleEntityType.Create(this);
            var applicationUser = ApplicationUserEntityType.Create(this);

            IdentityRoleClaimEntityType.CreateForeignKey1(identityRoleClaim, identityRole);
            IdentityUserClaimEntityType.CreateForeignKey1(identityUserClaim, applicationUser);
            IdentityUserLoginEntityType.CreateForeignKey1(identityUserLogin, applicationUser);
            IdentityUserRoleEntityType.CreateForeignKey1(identityUserRole, identityRole);
            IdentityUserRoleEntityType.CreateForeignKey2(identityUserRole, applicationUser);
            IdentityUserTokenEntityType.CreateForeignKey1(identityUserToken, applicationUser);
            DeliveryEntityType.CreateForeignKey1(delivery, order);
            DeliveryEntityType.CreateForeignKey2(delivery, vehicle);
            DeliveryProgressEntityType.CreateForeignKey1(deliveryProgress, order);
            IncidentEntityType.CreateForeignKey1(incident, orderHistory);
            OrderEntityType.CreateForeignKey1(order, customer);
            OrderHistoryEntityType.CreateForeignKey1(orderHistory, delivery);
            OrderHistoryEntityType.CreateForeignKey2(orderHistory, hub);
            RouteEntityType.CreateForeignKey1(route, hub);
            RouteEntityType.CreateForeignKey2(route, hub);
            VehicleEntityType.CreateForeignKey1(vehicle, staff);

            IdentityRoleEntityType.CreateAnnotations(identityRole);
            IdentityRoleClaimEntityType.CreateAnnotations(identityRoleClaim);
            IdentityUserClaimEntityType.CreateAnnotations(identityUserClaim);
            IdentityUserLoginEntityType.CreateAnnotations(identityUserLogin);
            IdentityUserRoleEntityType.CreateAnnotations(identityUserRole);
            IdentityUserTokenEntityType.CreateAnnotations(identityUserToken);
            CustomerEntityType.CreateAnnotations(customer);
            DeliveryEntityType.CreateAnnotations(delivery);
            DeliveryProgressEntityType.CreateAnnotations(deliveryProgress);
            HubEntityType.CreateAnnotations(hub);
            IncidentEntityType.CreateAnnotations(incident);
            OrderEntityType.CreateAnnotations(order);
            OrderHistoryEntityType.CreateAnnotations(orderHistory);
            RouteEntityType.CreateAnnotations(route);
            StaffEntityType.CreateAnnotations(staff);
            VehicleEntityType.CreateAnnotations(vehicle);
            ApplicationUserEntityType.CreateAnnotations(applicationUser);

            AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
            AddAnnotation("ProductVersion", "7.0.10");
            AddAnnotation("Relational:MaxIdentifierLength", 63);
        }
    }
}
