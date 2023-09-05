import 'package:json_annotation/json_annotation.dart';
part 'vehicle_location.g.dart';

@JsonSerializable()
class VehicleLocation {
  String id;
  double laititude;
  double longtitude;
  VehicleLocation(
      {required this.id, required this.laititude, required this.longtitude});

  factory VehicleLocation.fromJson(Map<String, dynamic> json) =>
      _$VehicleLocationFromJson(json);

  Map<String, dynamic> toJson() => _$VehicleLocationToJson(this);
}
