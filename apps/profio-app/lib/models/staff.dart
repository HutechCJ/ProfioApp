import 'package:json_annotation/json_annotation.dart';
import 'package:profio_staff_client/enums/staff_position.dart';

part 'staff.g.dart';

@JsonSerializable()
class Staff {
  String id;
  String name;
  String phone;
  StaffPosition position;
  Staff(
      {this.id = '',
      this.name = '',
      this.phone = '',
      this.position = StaffPosition.driver});

  factory Staff.fromJson(Map<String, dynamic> json) =>
      _$StaffFromJson(json);

  Map<String, dynamic> toJson() => _$StaffToJson(this);
}
