import 'package:flutter/material.dart';
import 'package:mobx/mobx.dart';
import 'package:profio_staff_client/api/base_api.dart';
import 'package:profio_staff_client/api/paging.dart';
import 'package:profio_staff_client/api/result_model.dart';
import 'package:profio_staff_client/constants/profio.dart';
import 'package:profio_staff_client/models/vehicle.dart';
import 'package:profio_staff_client/stores/hub_store.dart';
import 'package:provider/provider.dart';

part 'vehicle_store.g.dart';

class VehicleStore = VehicleStoreBase with _$VehicleStore;

abstract class VehicleStoreBase with Store {
  final _baseAPI = BaseAPI();

  late HubStore hubStore;

  @observable
  List<Vehicle> vehicleList = [];

  @observable
  Vehicle selectedVehicle = Vehicle();

  @computed
  bool get hasSelectedVehicle => selectedVehicle.id != '';

  @action
  void setVehicle(Vehicle vehicle) {
    selectedVehicle = vehicle;
  }

  @action
  Future<void> fetchVehicles() async {
    var data = await _baseAPI
        .fetchData('${Profio.baseUrl}/v1/${Profio.vehicleEndpoints}');
    var result = ResultModel.fromJson(data.object);
    var paging = Paging.fromJson(result.data);
    var vehicles = paging.items.map((item) => Vehicle.fromJson(item)).toList();
    vehicleList = vehicles;
  }

  @action
  Future<void> visit() async {
    if (!hasSelectedVehicle) return;
    var vehicleId = selectedVehicle.id;
    if (!hubStore.hasSelectedHub) return;
    var hubId = hubStore.selectedHub.id;
    await _baseAPI.fetchData(
        '${Profio.baseUrl}/v1/${Profio.vehicleEndpoints}/$vehicleId/${Profio.hubEndpoints}/$hubId/visit',
        method: ApiMethod.POST);
  }

  void onInit(BuildContext context) {
    hubStore = context.read<HubStore>();
  }
}
