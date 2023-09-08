import 'package:json_annotation/json_annotation.dart';

@JsonEnum()
enum VehicleType {
  @JsonValue(0)
  truck,
  @JsonValue(1)
  trailer,
  @JsonValue(2)
  van,
  @JsonValue(3)
  motorcycle
}
