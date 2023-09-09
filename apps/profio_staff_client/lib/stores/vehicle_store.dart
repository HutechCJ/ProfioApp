import 'package:mobx/mobx.dart';
import 'package:profio_staff_client/api/base_api.dart';
import 'package:profio_staff_client/api/paging.dart';
import 'package:profio_staff_client/api/result_model.dart';
import 'package:profio_staff_client/models/vehicle.dart';

part 'vehicle_store.g.dart';

class VehicleStore = VehicleStoreBase with _$VehicleStore;

abstract class VehicleStoreBase with Store {
  final _baseAPI = BaseAPI();
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
        .fetchData('https://profio-sv1.azurewebsites.net/api/v1/vehicles');
    var result = ResultModel.fromJson(data.object);
    var paging = Paging.fromJson(result.data);
    var vehicles = paging.items.map((item) => Vehicle.fromJson(item)).toList();
    vehicleList = vehicles;
  }
}
