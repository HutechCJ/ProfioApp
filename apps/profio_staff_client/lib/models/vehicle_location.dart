import 'package:json_annotation/json_annotation.dart';
part 'vehicle_location.g.dart';

@JsonSerializable()
class VehicleLocation {
  String id;
  double latitude;
  double longitude;
  VehicleLocation(
      {required this.id, required this.latitude, required this.longitude});

  factory VehicleLocation.fromJson(Map<String, dynamic> json) =>
      _$VehicleLocationFromJson(json);

  Map<String, dynamic> toJson() => _$VehicleLocationToJson(this);
}
