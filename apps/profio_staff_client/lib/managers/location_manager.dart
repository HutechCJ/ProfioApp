import 'dart:convert';

import 'package:geolocator/geolocator.dart';
import 'package:mqtt_client/mqtt_client.dart';
import 'package:profio_staff_client/models/vehicle_location.dart';
import 'package:profio_staff_client/providers/mqtt_provider.dart';

class LocationManager {
  static Future<Position> getPosition() async {
    bool serviceEnabled;
    LocationPermission permission;

    // Test if location services are enabled.
    serviceEnabled = await Geolocator.isLocationServiceEnabled();
    if (!serviceEnabled) {
      return Future.error('Location services are disabled.');
    }

    permission = await Geolocator.checkPermission();
    if (permission == LocationPermission.denied) {
      permission = await Geolocator.requestPermission();
      if (permission == LocationPermission.denied) {
        return Future.error('Location permissions are denied');
      }
    }

    if (permission == LocationPermission.deniedForever) {
      return Future.error(
          'Location permissions are permanently denied, we cannot request permissions.');
    }

    return await Geolocator.getCurrentPosition();
  }

  static Future<void> publishLocation(
      MqttProvider mqttProvider, Position position) async {
    const pubTopic = '/location';
    final builder = MqttClientPayloadBuilder();
    final location = VehicleLocation(
        id: "1234567890",
        latitude: position.latitude,
        longitude: position.longitude);
    builder.addString(jsonEncode(location.toJson()));

    print('MqttProvider::Publishing our topic');
    mqttProvider.publish(pubTopic, MqttQos.exactlyOnce, builder);
  }
}
