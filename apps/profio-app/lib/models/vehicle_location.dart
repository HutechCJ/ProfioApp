import 'package:json_annotation/json_annotation.dart';
part 'vehicle_location.g.dart';

@JsonSerializable()
class VehicleLocation {
  String id;
  double latitude;
  double longitude;
  List<String> orderIds = [];
  VehicleLocation(
      {required this.id,
      required this.latitude,
      required this.longitude,
      this.orderIds = const []});

  factory VehicleLocation.fromJson(Map<String, dynamic> json) =>
      _$VehicleLocationFromJson(json);

  Map<String, dynamic> toJson() => _$VehicleLocationToJson(this);
}
