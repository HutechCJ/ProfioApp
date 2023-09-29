import 'package:json_annotation/json_annotation.dart';

@JsonEnum()
enum StaffPosition {
  @JsonValue(0)
  driver,
  @JsonValue(1)
  shipper,
  @JsonValue(2)
  officer,
  @JsonValue(3)
  stoker
}
