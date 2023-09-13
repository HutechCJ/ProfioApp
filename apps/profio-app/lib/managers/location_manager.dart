import 'dart:async';
import 'dart:convert';
import 'dart:math';

import 'package:flutter_polyline_points/flutter_polyline_points.dart';
import 'package:geolocator/geolocator.dart';
import 'package:mqtt_client/mqtt_client.dart';
import 'package:profio_staff_client/api/base_api.dart';
import 'package:profio_staff_client/models/vehicle_location.dart';
import 'package:profio_staff_client/providers/mqtt_provider.dart';

class LocationManager {
  static const String locationTopic = '/location';
  static bool stopSimulation = false;
  static Map<String, dynamic>? cachedDirections;

  static Future<Map<String, dynamic>> getDirections(
      String origin, String destination) async {
    const String key = 'AIzaSyDAW0v16XSZI3GdNte36gFHDynsed4-cz0';
    final String url =
        'https://maps.googleapis.com/maps/api/directions/json?origin=$origin&destination=$destination&key=$key';

    var response = await BaseAPI().fetchData(url);
    var json = response.object;

    var results = {
      'bounds_ne': json['routes'][0]['bounds']['northeast'],
      'bounds_sw': json['routes'][0]['bounds']['southwest'],
      'start_location': json['routes'][0]['legs'][0]['start_location'],
      'end_location': json['routes'][0]['legs'][0]['end_location'],
      'polyline': json['routes'][0]['overview_polyline']['points'],
      'polyline_decoded': PolylinePoints()
          .decodePolyline(json['routes'][0]['overview_polyline']['points']),
    };

    // Cache the directions data
    cachedDirections = results;

    return results;
  }

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
      MqttProvider mqttProvider, Position position,
      {String vehicleId = ''}) async {
    const pubTopic = locationTopic;
    final builder = MqttClientPayloadBuilder();
    final location = VehicleLocation(
        id: vehicleId,
        latitude: position.latitude,
        longitude: position.longitude);
    builder.addString(jsonEncode(location.toJson()));

    print('MqttProvider::Publishing our topic');
    mqttProvider.publish(pubTopic, MqttQos.exactlyOnce, builder);
  }

  static void simulateCarMovement(MqttProvider mqttProvider,
      Position startLocation, Position endLocation, double vehicleSpeed,
      {Function(Position)? onIntermediatePosition, String vehicleId = ''}) {
    const pubTopic = locationTopic;

    // Calculate the total distance between start and end locations
    final totalDistance = calculateDistance(startLocation, endLocation);

    // Calculate the time to travel the entire distance at the given speed
    final totalTime = totalDistance / vehicleSpeed;

    // Timer interval (adjust as needed)
    const timerInterval = Duration(seconds: 1);

    Timer.periodic(timerInterval, (timer) async {
      if (stopSimulation) {
        timer.cancel();
        print('Simulation stopped.');
        stopSimulation = false;
        return;
      }
      // Calculate the distance to travel in this time interval
      final distanceToTravel = vehicleSpeed * timerInterval.inSeconds;

      // Calculate the new intermediate position
      final intermediatePosition = calculateIntermediatePosition(
          startLocation, endLocation, distanceToTravel);

      // Publish the intermediate position (simulating vehicle movement)
      print(
          'Intermediate Position: ${intermediatePosition.latitude}, ${intermediatePosition.longitude}');

      onIntermediatePosition?.call(intermediatePosition);

      // Update the startLocation for the next iteration
      startLocation = intermediatePosition;

      // Check if the destination is reached
      if (startLocation.latitude == endLocation.latitude &&
          startLocation.longitude == endLocation.longitude) {
        timer.cancel();
        print('Destination reached!');
      } else {
        // Publish the intermediate position to MQTT
        await publishLocation(mqttProvider, intermediatePosition,
            vehicleId: vehicleId);
      }
    });
  }

  static double calculateDistance(Position position1, Position position2) {
    const double earthRadius = 6371; // Radius of the Earth in kilometers

    // Convert latitude and longitude from degrees to radians
    final double lat1 = position1.latitude * (pi / 180);
    final double lon1 = position1.longitude * (pi / 180);
    final double lat2 = position2.latitude * (pi / 180);
    final double lon2 = position2.longitude * (pi / 180);

    // Haversine formula
    final double dLat = lat2 - lat1;
    final double dLon = lon2 - lon1;
    final double a = sin(dLat / 2) * sin(dLat / 2) +
        cos(lat1) * cos(lat2) * sin(dLon / 2) * sin(dLon / 2);
    final double c = 2 * atan2(sqrt(a), sqrt(1 - a));
    final double distance = earthRadius * c;

    return distance;
  }

  static Position calculateIntermediatePosition(
      Position start, Position end, double distance) {
    const double earthRadius = 6371; // Radius of the Earth in kilometers

    // Convert latitude and longitude from degrees to radians
    final double lat1 = start.latitude * (pi / 180);
    final double lon1 = start.longitude * (pi / 180);
    final double lat2 = end.latitude * (pi / 180);
    final double lon2 = end.longitude * (pi / 180);

    // Haversine formula
    final double dLat = lat2 - lat1;
    final double dLon = lon2 - lon1;
    final double a = sin(dLat / 2) * sin(dLat / 2) +
        cos(lat1) * cos(lat2) * sin(dLon / 2) * sin(dLon / 2);
    final double c = 2 * atan2(sqrt(a), sqrt(1 - a));

    // Calculate the bearing (direction) from start to end
    final double bearing = atan2(sin(dLon) * cos(lat2),
        cos(lat1) * sin(lat2) - sin(lat1) * cos(lat2) * cos(dLon));

    // Calculate the new latitude and longitude based on the given distance
    final double newLat = asin(sin(lat1) * cos(distance / earthRadius) +
        cos(lat1) * sin(distance / earthRadius) * cos(bearing));
    final double newLon = lon1 +
        atan2(sin(bearing) * sin(distance / earthRadius) * cos(lat1),
            cos(distance / earthRadius) - sin(lat1) * sin(newLat));

    // Convert the new latitude and longitude from radians to degrees
    final double newLatDegrees = newLat * (180 / pi);
    final double newLonDegrees = newLon * (180 / pi);

    // Create a new Position object with the updated latitude and longitude
    final newPosition = Position(
      latitude: newLatDegrees,
      longitude: newLonDegrees,
      timestamp: null, // You can set a timestamp if needed
      altitude: 0.0, // You can set altitude if needed
      accuracy: 0.0, // You can set accuracy if needed
      heading: 0.0, // You can set heading if needed
      speed: 0.0, // You can set speed if needed
      speedAccuracy: 0.0, // You can set speed accuracy if needed
      floor: null, // You can set the floor if needed
      isMocked: false, // You can set isMocked if needed
    );

    return newPosition;
  }

  static void stopCarSimulation() {
    stopSimulation = true;
  }
}
