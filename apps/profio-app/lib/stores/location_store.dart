import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_polyline_points/flutter_polyline_points.dart';
import 'package:geolocator/geolocator.dart';
import 'package:mobx/mobx.dart';
import 'package:profio_staff_client/api/base_api.dart';
import 'package:profio_staff_client/enums/vehicle_type.dart';
import 'package:profio_staff_client/managers/location_manager.dart';
import 'package:profio_staff_client/providers/mqtt_provider.dart';
import 'package:profio_staff_client/stores/hub_store.dart';
import 'package:profio_staff_client/stores/vehicle_store.dart';
import 'package:provider/provider.dart';
import 'dart:convert' as convert;

part 'location_store.g.dart';

class LocationStore = LocationStoreBase with _$LocationStore;

abstract class LocationStoreBase with Store {
  late VehicleStore vehicleStore;
  late HubStore hubStore;
  late MqttProvider mqttProvider;
  final _baseAPI = BaseAPI();
  final String key = 'AIzaSyDAW0v16XSZI3GdNte36gFHDynsed4-cz0';
  @observable
  Position? selectedPosition;

  @computed
  bool get hasSelectedPosition => selectedPosition != null;

  @action
  Future<void> getCurrentLocation() async =>
      selectedPosition = await LocationManager.getPosition();

  @action
  Future<void> setCurrentLocation(Position position) async =>
      selectedPosition = position;

  @action
  void simulateVehicleMovement({String vehicleId = ''}) {
    if (!vehicleStore.hasSelectedVehicle) return;
    var vehicleSpeed = getVehicleSpeed(vehicleStore.selectedVehicle.type);

    var hubPosition = Position(
        latitude: hubStore.selectedHub.location.latitude,
        longitude: hubStore.selectedHub.location.longitude,
        accuracy: 0,
        altitude: 0,
        heading: 0,
        speed: 0,
        speedAccuracy: 0,
        timestamp: null,
        floor: 0,
        isMocked: false);

    if (!hasSelectedPosition) return;

    LocationManager.simulateCarMovement(
        mqttProvider, selectedPosition!, hubPosition, vehicleSpeed,
        onIntermediatePosition: (p) => {setCurrentLocation(p)},
        vehicleId: vehicleId);
  }

  double getVehicleSpeed(VehicleType type) {
    switch (type) {
      case VehicleType.motorcycle:
        return 0.004;
      case VehicleType.truck:
      case VehicleType.trailer:
      case VehicleType.van:
        return 0.006;
    }
  }

  void stopSimulation() {
    LocationManager.stopCarSimulation();
  }

  Future<Map<String, dynamic>> getDirections(
      String origin, String destination) async {
    final String url =
        'https://maps.googleapis.com/maps/api/directions/json?origin=$origin&destination=$destination&key=$key';

    var response = await _baseAPI.fetchData(url);
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

    print(results);

    return results;
  }

  void onInit(BuildContext context) {
    vehicleStore = context.read<VehicleStore>();
    hubStore = context.read<HubStore>();
    mqttProvider = context.read<MqttProvider>();
    mqttProvider.connect();
  }
}
