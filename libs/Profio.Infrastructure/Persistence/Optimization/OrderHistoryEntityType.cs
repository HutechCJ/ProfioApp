// <auto-generated />
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Profio.Domain.Entities;

#pragma warning disable 219, 612, 618
#nullable enable

namespace Profio.Infrastructure.Persistence.Relational.Optimization
{
    internal partial class OrderHistoryEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType? baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "Profio.Domain.Entities.OrderHistory",
                typeof(OrderHistory),
                baseEntityType);

            var id = runtimeEntityType.AddProperty(
                "Id",
                typeof(string),
                propertyInfo: typeof(OrderHistory).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(OrderHistory).GetField("<Id>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                afterSaveBehavior: PropertySaveBehavior.Throw,
                maxLength: 26);

            var hubId = runtimeEntityType.AddProperty(
                "HubId",
                typeof(string),
                propertyInfo: typeof(OrderHistory).GetProperty("HubId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(OrderHistory).GetField("<HubId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);

            var orderId = runtimeEntityType.AddProperty(
                "OrderId",
                typeof(string),
                propertyInfo: typeof(OrderHistory).GetProperty("OrderId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(OrderHistory).GetField("<OrderId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);

            var timestamp = runtimeEntityType.AddProperty(
                "Timestamp",
                typeof(DateTime?),
                propertyInfo: typeof(OrderHistory).GetProperty("Timestamp", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(OrderHistory).GetField("<Timestamp>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);

            var vehicleId = runtimeEntityType.AddProperty(
                "VehicleId",
                typeof(string),
                propertyInfo: typeof(OrderHistory).GetProperty("VehicleId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(OrderHistory).GetField("<VehicleId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);

            var key = runtimeEntityType.AddKey(
                new[] { id });
            runtimeEntityType.SetPrimaryKey(key);

            var index = runtimeEntityType.AddIndex(
                new[] { hubId });

            var index0 = runtimeEntityType.AddIndex(
                new[] { orderId });

            var index1 = runtimeEntityType.AddIndex(
                new[] { vehicleId });

            return runtimeEntityType;
        }

        public static RuntimeForeignKey CreateForeignKey1(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("HubId")! },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("Id")! })!,
                principalEntityType,
                deleteBehavior: DeleteBehavior.SetNull);

            var hub = declaringEntityType.AddNavigation("Hub",
                runtimeForeignKey,
                onDependent: true,
                typeof(Domain.Entities.Hub),
                propertyInfo: typeof(OrderHistory).GetProperty("Hub", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(OrderHistory).GetField("<Hub>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            var orderHistories = principalEntityType.AddNavigation("OrderHistories",
                runtimeForeignKey,
                onDependent: false,
                typeof(ICollection<OrderHistory>),
                propertyInfo: typeof(Domain.Entities.Hub).GetProperty("OrderHistories", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Domain.Entities.Hub).GetField("<OrderHistories>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            return runtimeForeignKey;
        }

        public static RuntimeForeignKey CreateForeignKey2(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("OrderId")! },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("Id")! })!,
                principalEntityType,
                deleteBehavior: DeleteBehavior.Cascade);

            var order = declaringEntityType.AddNavigation("Order",
                runtimeForeignKey,
                onDependent: true,
                typeof(Order),
                propertyInfo: typeof(OrderHistory).GetProperty("Order", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(OrderHistory).GetField("<Order>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            var orderHistories = principalEntityType.AddNavigation("OrderHistories",
                runtimeForeignKey,
                onDependent: false,
                typeof(ICollection<OrderHistory>),
                propertyInfo: typeof(Order).GetProperty("OrderHistories", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Order).GetField("<OrderHistories>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            return runtimeForeignKey;
        }

        public static RuntimeForeignKey CreateForeignKey3(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("VehicleId")! },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("Id")! })!,
                principalEntityType,
                deleteBehavior: DeleteBehavior.SetNull);

            var vehicle = declaringEntityType.AddNavigation("Vehicle",
                runtimeForeignKey,
                onDependent: true,
                typeof(Vehicle),
                propertyInfo: typeof(OrderHistory).GetProperty("Vehicle", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(OrderHistory).GetField("<Vehicle>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            var orderHistories = principalEntityType.AddNavigation("OrderHistories",
                runtimeForeignKey,
                onDependent: false,
                typeof(ICollection<OrderHistory>),
                propertyInfo: typeof(Vehicle).GetProperty("OrderHistories", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Vehicle).GetField("<OrderHistories>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            return runtimeForeignKey;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
            runtimeEntityType.AddAnnotation("Relational:Schema", null);
            runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
            runtimeEntityType.AddAnnotation("Relational:TableName", "OrderHistories");
            runtimeEntityType.AddAnnotation("Relational:ViewName", null);
            runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}