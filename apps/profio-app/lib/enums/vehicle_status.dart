import 'package:json_annotation/json_annotation.dart';

@JsonEnum()
enum VehicleStatus {
  @JsonValue(0)
  idle,
  @JsonValue(1)
  busy,
  @JsonValue(2)
  offline
}
