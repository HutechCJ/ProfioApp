// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'vehicle_location.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

VehicleLocation _$VehicleLocationFromJson(Map<String, dynamic> json) =>
    VehicleLocation(
      id: json['id'] as String,
      latitude: (json['latitude'] as num).toDouble(),
      longitude: (json['longitude'] as num).toDouble(),
      orderIds: (json['orderIds'] as List<dynamic>?)
              ?.map((e) => e as String)
              .toList() ??
          const [],
    );

Map<String, dynamic> _$VehicleLocationToJson(VehicleLocation instance) =>
    <String, dynamic>{
      'id': instance.id,
      'latitude': instance.latitude,
      'longitude': instance.longitude,
      'orderIds': instance.orderIds,
    };
