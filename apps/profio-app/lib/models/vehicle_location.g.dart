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
    );

Map<String, dynamic> _$VehicleLocationToJson(VehicleLocation instance) =>
    <String, dynamic>{
      'id': instance.id,
      'latitude': instance.latitude,
      'longitude': instance.longitude,
    };
