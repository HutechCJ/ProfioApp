import 'package:flutter/material.dart';
import 'package:geolocator/geolocator.dart';
import 'package:mobx/mobx.dart';
import 'package:profio_staff_client/api/base_api.dart';
import 'package:profio_staff_client/api/paging.dart';
import 'package:profio_staff_client/api/result_model.dart';
import 'package:profio_staff_client/constants/profio.dart';
import 'package:profio_staff_client/models/hub.dart';
import 'package:profio_staff_client/models/order.dart';
import 'package:profio_staff_client/models/vehicle.dart';
import 'package:profio_staff_client/stores/hub_store.dart';
import 'package:profio_staff_client/stores/location_store.dart';
import 'package:provider/provider.dart';

part 'vehicle_store.g.dart';

class VehicleStore = VehicleStoreBase with _$VehicleStore;

abstract class VehicleStoreBase with Store {
  final _baseAPI = BaseAPI();

  late HubStore hubStore;
  late LocationStore locationStore;

  @observable
  ObservableList<Vehicle> vehicleList = ObservableList();

  @observable
  Vehicle selectedVehicle = Vehicle();

  @observable
  Hub startHub = Hub();

  @observable
  Hub endHub = Hub();

  @computed
  bool get hasSelectedVehicle => selectedVehicle.id != '';

  @action
  void setVehicle(Vehicle vehicle) => selectedVehicle = vehicle;

  @action
  Future<void> fetchVehicles() async {
    var data = await _baseAPI
        .fetchData('${Profio.baseUrl}/v1/${Profio.vehicleEndpoints}');
    var result = ResultModel.fromJson(data.object);
    var paging = Paging.fromJson(result.data);
    var vehicles = paging.items.map((item) => Vehicle.fromJson(item)).toList();
    vehicleList = ObservableList.of(vehicles);
  }

  @action
  Future<void> fetchHubPath() async {
    if (!hasSelectedVehicle) return;
    var data = await _baseAPI.fetchData(
        '${Profio.baseUrl}/v1/${Profio.vehicleEndpoints}/${selectedVehicle.id}/${Profio.hubEndpoints}/path');
    var result = ResultModel.fromJson(data.object);
    var paging = Paging.fromJson(result.data);
    var hubs = paging.items.map((item) => Hub.fromJson(item)).toList();
    if (hubs.length == 2) {
      startHub = hubs[0];
      locationStore.setCurrentLocation(Position(
        latitude: startHub.location.latitude,
        longitude: startHub.location.longitude,
        accuracy: 0,
        altitude: 0,
        heading: 0,
        speed: 0,
        speedAccuracy: 0,
        timestamp: null,
        floor: 0,
        isMocked: false,
        // altitudeAccuracy: 0,
        // headingAccuracy: 0
      ));
      endHub = hubs[1];
    }
  }

  @action
  Future<void> visit() async {
    if (!hasSelectedVehicle) return;
    var vehicleId = selectedVehicle.id;
    if (!hubStore.hasSelectedHub) return;
    var hubId = hubStore.selectedHub.id;
    await _baseAPI.fetchData(
        '${Profio.baseUrl}/v1/${Profio.vehicleEndpoints}/$vehicleId/${Profio.hubEndpoints}/$hubId/visit',
        method: ApiMethod.post);
  }

  Future<List<Order>> fetchOrders() async {
    if (hasSelectedVehicle) {
      var data = await _baseAPI.fetchData(
          '${Profio.baseUrl}/v1/${Profio.vehicleEndpoints}/${selectedVehicle.id}/${Profio.orderEndpoints}/current');
      var result = ResultModel.fromJson(data.object);
      var paging = Paging.fromJson(result.data);
      var orders = paging.items.map((item) => Order.fromJson(item)).toList();
      return orders;
    }
    return [];
  }

  void onInit(BuildContext context) {
    hubStore = context.read<HubStore>();
    locationStore = context.read<LocationStore>();
  }

  void onDispose() {
    vehicleList = ObservableList();
    selectedVehicle = Vehicle();
  }
}
