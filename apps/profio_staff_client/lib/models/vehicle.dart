import 'package:json_annotation/json_annotation.dart';
import 'package:profio_staff_client/enums/vehicle_status.dart';
import 'package:profio_staff_client/enums/vehicle_type.dart';
import 'package:profio_staff_client/models/staff.dart';

part 'vehicle.g.dart';

@JsonSerializable()
class Vehicle {
  String id;
  String zipCodeCurrent;
  String licensePlate;
  VehicleType type;
  VehicleStatus status;
  Staff? staff;
  Vehicle(
      {this.id = '',
      this.zipCodeCurrent = '',
      this.licensePlate = '',
      this.type = VehicleType.truck,
      this.status = VehicleStatus.idle,
      this.staff});
  factory Vehicle.fromJson(Map<String, dynamic> json) =>
      _$VehicleFromJson(json);

  Map<String, dynamic> toJson() => _$VehicleToJson(this);
}
