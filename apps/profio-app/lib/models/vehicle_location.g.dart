// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'vehicle_location.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

VehicleLocation _$VehicleLocationFromJson(Map<String, dynamic> json) =>
    VehicleLocation(
      id: json['id'] as String,
      laititude: (json['laititude'] as num).toDouble(),
      longtitude: (json['longtitude'] as num).toDouble(),
    );

Map<String, dynamic> _$VehicleLocationToJson(VehicleLocation instance) =>
    <String, dynamic>{
      'id': instance.id,
      'laititude': instance.laititude,
      'longtitude': instance.longtitude,
    };
