// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'vehicle.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Vehicle _$VehicleFromJson(Map<String, dynamic> json) => Vehicle(
      id: json['id'] as String? ?? '',
      zipCodeCurrent: json['zipCodeCurrent'] as String? ?? '',
      licensePlate: json['licensePlate'] as String? ?? '',
      type: $enumDecodeNullable(_$VehicleTypeEnumMap, json['type']) ??
          VehicleType.truck,
      status: $enumDecodeNullable(_$VehicleStatusEnumMap, json['status']) ??
          VehicleStatus.idle,
      staff: json['staff'] == null
          ? null
          : Staff.fromJson(json['staff'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$VehicleToJson(Vehicle instance) => <String, dynamic>{
      'id': instance.id,
      'zipCodeCurrent': instance.zipCodeCurrent,
      'licensePlate': instance.licensePlate,
      'type': _$VehicleTypeEnumMap[instance.type]!,
      'status': _$VehicleStatusEnumMap[instance.status]!,
      'staff': instance.staff,
    };

const _$VehicleTypeEnumMap = {
  VehicleType.truck: 0,
  VehicleType.trailer: 1,
  VehicleType.van: 2,
  VehicleType.motorcycle: 3,
};

const _$VehicleStatusEnumMap = {
  VehicleStatus.idle: 0,
  VehicleStatus.busy: 1,
  VehicleStatus.offline: 2,
};
