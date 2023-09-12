import 'package:profio_staff_client/providers/mqtt_provider.dart';
import 'package:profio_staff_client/stores/hub_store.dart';
import 'package:profio_staff_client/stores/location_store.dart';
import 'package:profio_staff_client/stores/vehicle_store.dart';
import 'package:provider/provider.dart';
import 'package:provider/single_child_widget.dart';

class ProviderManager {
  ProviderManager._();
  static List<SingleChildWidget> providers = [
    Provider<HubStore>(
      create: (_) => HubStore(),
    ),
    Provider<VehicleStore>(create: (_) => VehicleStore()),
    Provider<LocationStore>(create: (_) => LocationStore()),
    Provider<MqttProvider>(create: (_) => MqttProvider())
  ];
}
