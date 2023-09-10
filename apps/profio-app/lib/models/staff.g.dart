// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'staff.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Staff _$StaffFromJson(Map<String, dynamic> json) => Staff(
      id: json['id'] as String? ?? '',
      name: json['name'] as String? ?? '',
      phone: json['phone'] as String? ?? '',
      position: $enumDecodeNullable(_$StaffPositionEnumMap, json['position']) ??
          StaffPosition.driver,
    );

Map<String, dynamic> _$StaffToJson(Staff instance) => <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'phone': instance.phone,
      'position': _$StaffPositionEnumMap[instance.position]!,
    };

const _$StaffPositionEnumMap = {
  StaffPosition.driver: 0,
  StaffPosition.shipper: 1,
};
