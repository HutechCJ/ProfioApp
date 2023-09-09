import 'package:json_annotation/json_annotation.dart';
import 'package:profio_staff_client/models/location.dart';

part 'hub.g.dart';

@JsonSerializable()
class Hub {
  String id;
  String zipCode;
  Location location;
  Hub({this.id = '', this.zipCode = '', this.location = const Location()});

  factory Hub.fromJson(Map<String, dynamic> json) => _$HubFromJson(json);

  Map<String, dynamic> toJson() => _$HubToJson(this);
}
