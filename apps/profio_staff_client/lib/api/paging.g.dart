// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'paging.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Paging _$PagingFromJson(Map<String, dynamic> json) => Paging(
      pageIndex: json['pageIndex'] as int? ?? 0,
      pageSize: json['pageSize'] as int? ?? 0,
      count: json['count'] as int? ?? 0,
      totalCount: json['totalCount'] as int? ?? 0,
      totalPages: json['totalPages'] as int? ?? 0,
      hasPreviousPage: json['hasPreviousPage'] as bool? ?? false,
      hasNextPage: json['hasNextPage'] as bool? ?? false,
      items: json['items'] as List<dynamic>? ?? const [],
    );

Map<String, dynamic> _$PagingToJson(Paging instance) => <String, dynamic>{
      'pageIndex': instance.pageIndex,
      'pageSize': instance.pageSize,
      'count': instance.count,
      'totalCount': instance.totalCount,
      'totalPages': instance.totalPages,
      'hasPreviousPage': instance.hasPreviousPage,
      'hasNextPage': instance.hasNextPage,
      'items': instance.items,
    };
