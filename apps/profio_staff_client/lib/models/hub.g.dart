// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'hub.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Hub _$HubFromJson(Map<String, dynamic> json) => Hub(
      id: json['id'] as String? ?? '',
      zipCode: json['zipCode'] as String? ?? '',
      location: json['location'] == null
          ? const Location()
          : Location.fromJson(json['location'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$HubToJson(Hub instance) => <String, dynamic>{
      'id': instance.id,
      'zipCode': instance.zipCode,
      'location': instance.location,
    };
