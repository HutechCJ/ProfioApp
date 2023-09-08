// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'result_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ResultModel _$ResultModelFromJson(Map<String, dynamic> json) => ResultModel(
      data: json['data'],
      isError: json['isError'] as bool? ?? false,
      errorMessage: json['errorMessage'] as String? ?? '',
    );

Map<String, dynamic> _$ResultModelToJson(ResultModel instance) =>
    <String, dynamic>{
      'data': instance.data,
      'isError': instance.isError,
      'errorMessage': instance.errorMessage,
    };
