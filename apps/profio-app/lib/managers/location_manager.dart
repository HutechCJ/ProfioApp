import 'dart:async';
import 'dart:convert';
import 'dart:math';

import 'package:flutter_polyline_points/flutter_polyline_points.dart';
import 'package:geolocator/geolocator.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:mqtt_client/mqtt_client.dart';
import 'package:profio_staff_client/api/base_api.dart';
import 'package:profio_staff_client/models/vehicle_location.dart';
import 'package:profio_staff_client/providers/mqtt_provider.dart';
import 'package:shared_preferences/shared_preferences.dart';

class LocationManager {
  static const String locationTopic = '/location';
  static bool stopSimulation = false;
  static Map<String, dynamic>? cachedDirections;
  static List<LatLng> routePoints = [];

  static Future<Map<String, dynamic>> getDirections(
      String origin, String destination) async {
    const String key = 'AIzaSyDAW0v16XSZI3GdNte36gFHDynsed4-cz0';

    final SharedPreferences preferences = await SharedPreferences.getInstance();
    var jsonString = preferences.getString('direction_result');

    print('Origin: ' + origin);
    print('Destination: ' + destination);
    dynamic json;

    if (jsonString == null) {
      try {
        final String url =
            'https://maps.googleapis.com/maps/api/directions/json?origin=$origin&destination=$destination&key=$key';

        var response = await BaseAPI().fetchData(url);

        json = response.object;
        print('Stored json: ' + json.toString());
        preferences.setString('direction_result', jsonEncode(json));
      } catch (e) {
        preferences.remove('direction_result');
        rethrow;
      }
    } else {
      json = jsonDecode(jsonString);
    }
    print('Json: ' + json.toString());
    var results = {
      'bounds_ne': json['routes'][0]['bounds']['northeast'],
      'bounds_sw': json['routes'][0]['bounds']['southwest'],
      'start_location': json['routes'][0]['legs'][0]['start_location'],
      'end_location': json['routes'][0]['legs'][0]['end_location'],
      'polyline': json['routes'][0]['overview_polyline']['points'],
      'polyline_decoded': PolylinePoints()
          .decodePolyline(json['routes'][0]['overview_polyline']['points']),
    };

    if (routePoints.isEmpty) {
      for (var point in results['polyline_decoded']) {
        routePoints.add(LatLng(point.latitude, point.longitude));
      }
    }

    cachedDirections = results;
    return results;

    // Cache the directions data
  }

  List<LatLng> get currentRoutePoints => routePoints;

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
      {String vehicleId = '', VehicleLocation? vehicleLocation}) async {
    const pubTopic = locationTopic;
    final builder = MqttClientPayloadBuilder();
    if (vehicleLocation != null) {
      if (vehicleId != '') {
        vehicleLocation.id = vehicleId;
      }
      vehicleLocation.latitude = position.latitude;
      vehicleLocation.longitude = position.longitude;
      builder.addString(jsonEncode(vehicleLocation.toJson()));
    } else {
      final location = VehicleLocation(
          id: vehicleId,
          latitude: position.latitude,
          longitude: position.longitude,
          orderIds: [
            "01HB0Q3XZ5K80BR907M6R05K5A",
            "01HAR5JFN6Y30YSXSDK2964S3D"
          ]);
      builder.addString(jsonEncode(location.toJson()));
    }

    print('MqttProvider::Publishing our topic');
    mqttProvider.publish(pubTopic, MqttQos.exactlyOnce, builder);
  }

  static void simulateCarMovement(MqttProvider mqttProvider,
      Position startLocation, Position endLocation, double vehicleSpeed,
      {Function(Position)? onIntermediatePosition,
      String vehicleId = '',
      VehicleLocation? vehicleLocation}) {
    const pubTopic = locationTopic;

    // Calculate the total distance between start and end locations
    final totalDistance = calculateDistance(
      LatLng(startLocation.latitude, startLocation.longitude),
      LatLng(endLocation.latitude, endLocation.longitude),
    );

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

      // Check if the vehicle has reached the end position or gone beyond it
      final distanceToEnd = calculateDistance(
        LatLng(intermediatePosition.latitude, intermediatePosition.longitude),
        LatLng(endLocation.latitude, endLocation.longitude),
      );
      if (distanceToEnd <= distanceToTravel) {
        timer.cancel();
        print('Destination reached!');
        // You may want to publish the final position here
        await publishLocation(mqttProvider, endLocation,
            vehicleId: vehicleId, vehicleLocation: vehicleLocation);
      } else {
        // Publish the intermediate position to MQTT
        await publishLocation(mqttProvider, intermediatePosition,
            vehicleId: vehicleId, vehicleLocation: vehicleLocation);
      }

      // // Check if the destination is reached
      // if (startLocation.latitude == endLocation.latitude &&
      //     startLocation.longitude == endLocation.longitude) {
      //   timer.cancel();
      //   print('Destination reached!');
      // } else {
      //   // Publish the intermediate position to MQTT
      //   await publishLocation(mqttProvider, intermediatePosition,
      //       vehicleId: vehicleId, vehicleLocation: vehicleLocation);
      // }
    });
  }

  static double calculateDistance(LatLng position1, LatLng position2) {
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

    // Generate a random factor for randomness between -1.0 and 1.0
    final randomness = (Random().nextDouble() * 50.0) - 1.0;

    // Add randomness to the bearing
    final double randomBearing = bearing + (randomness * (pi / 180.0));

    // Calculate the new latitude and longitude based on the given distance
    final double newLat = asin(sin(lat1) * cos(distance / earthRadius) +
        cos(lat1) * sin(distance / earthRadius) * cos(randomBearing));
    final double newLon = lon1 +
        atan2(sin(randomBearing) * sin(distance / earthRadius) * cos(lat1),
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

  static void simulateDirections(
      MqttProvider mqttProvider, List<LatLng> routePoints, double vehicleSpeed,
      {Function(Position)? onIntermediatePosition,
      String vehicleId = '',
      VehicleLocation? vehicleLocation}) {
    const pubTopic = locationTopic;

    // Calculate the total distance of the route
    double totalDistance = 0;
    for (int i = 0; i < routePoints.length - 1; i++) {
      totalDistance += calculateDistance(
        LatLng(routePoints[i].latitude, routePoints[i].longitude),
        LatLng(routePoints[i + 1].latitude, routePoints[i + 1].longitude),
      );
    }

    // Timer interval (adjust as needed)
    const timerInterval = Duration(seconds: 1);
    int currentRouteIndex = 0;
    double currentDistance = 0;

    Timer.periodic(timerInterval, (timer) async {
      if (stopSimulation) {
        timer.cancel();
        print('Simulation stopped.');
        stopSimulation = false;
        return;
      }

      // Calculate the distance to travel in this time interval
      final distanceToTravel = vehicleSpeed * timerInterval.inSeconds;

      while (currentRouteIndex < routePoints.length - 1) {
        // Calculate the distance to the next point
        final distanceToNextPoint = calculateDistance(
          LatLng(routePoints[currentRouteIndex].latitude,
              routePoints[currentRouteIndex].longitude),
          LatLng(routePoints[currentRouteIndex + 1].latitude,
              routePoints[currentRouteIndex + 1].longitude),
        );

        if (currentDistance + distanceToNextPoint <= distanceToTravel) {
          // Move to the next point
          currentDistance += distanceToNextPoint;
          currentRouteIndex++;
        } else {
          // Calculate the intermediate position between the current and next points
          final ratio =
              (distanceToTravel - currentDistance) / distanceToNextPoint;
          final intermediatePosition = Position(
              latitude: routePoints[currentRouteIndex].latitude +
                  (routePoints[currentRouteIndex + 1].latitude -
                          routePoints[currentRouteIndex].latitude) *
                      ratio,
              longitude: routePoints[currentRouteIndex].longitude +
                  (routePoints[currentRouteIndex + 1].longitude -
                          routePoints[currentRouteIndex].longitude) *
                      ratio,
              accuracy: 0,
              altitude: 0,
              heading: 0,
              speed: 0,
              speedAccuracy: 0,
              timestamp: null,
              floor: 0,
              isMocked: false);

          // Publish the intermediate position (simulating vehicle movement)
          print(
              'Intermediate Position: ${intermediatePosition.latitude}, ${intermediatePosition.longitude}');

          onIntermediatePosition?.call(intermediatePosition);

          // Update the current distance
          currentDistance = 0;

          // Check if the vehicle has reached the end of the route
          if (currentRouteIndex == routePoints.length - 1) {
            timer.cancel();
            print('Destination reached!');
            // You may want to publish the final position here
            await publishLocation(mqttProvider, intermediatePosition,
                vehicleId: vehicleId, vehicleLocation: vehicleLocation);
          } else {
            // Publish the intermediate position to MQTT
            await publishLocation(mqttProvider, intermediatePosition,
                vehicleId: vehicleId, vehicleLocation: vehicleLocation);
          }

          break; // Exit the loop to wait for the next timer tick
        }
      }
    });
  }

  static void stopCarSimulation() {
    stopSimulation = true;
  }
}
