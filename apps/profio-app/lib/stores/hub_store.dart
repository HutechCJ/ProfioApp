import 'package:mobx/mobx.dart';
import 'package:profio_staff_client/api/base_api.dart';
import 'package:profio_staff_client/api/paging.dart';
import 'package:profio_staff_client/api/result_model.dart';
import 'package:profio_staff_client/constants/profio.dart';
import 'package:profio_staff_client/models/hub.dart';

part 'hub_store.g.dart';

class HubStore = HubStoreBase with _$HubStore;

abstract class HubStoreBase with Store {
  final _baseAPI = BaseAPI();
  @observable
  List<Hub> hubList = [];

  @observable
  Hub selectedHub = Hub();

  @computed
  bool get hasSelectedHub => selectedHub.id != '';

  @action
  void setHub(Hub hub) => selectedHub = hub;

  @action
  void setNullHub() => selectedHub = Hub();

  @action
  Future<void> getNextHub(String vehicleId) async {
    var data = await _baseAPI.fetchData(
        '${Profio.baseUrl}/v1/${Profio.vehicleEndpoints}/$vehicleId/${Profio.hubEndpoints}/next');
    if (data.apiStatus == ApiStatus.failed) {
      setNullHub();
    }
    var result = ResultModel.fromJson(data.object);
    var hub = Hub.fromJson(result.data);
    selectedHub = hub;
  }

  @action
  Future<void> fetchHubs() async {
    var data = await _baseAPI
        .fetchData('https://profioapp.azurewebsites.net/api/v1/hubs');
    var result = ResultModel.fromJson(data.object);
    var paging = Paging.fromJson(result.data);
    var hubs = paging.items.map((item) => Hub.fromJson(item)).toList();
    hubList = hubs;
  }
}
