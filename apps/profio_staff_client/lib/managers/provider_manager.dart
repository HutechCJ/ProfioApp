import 'package:profio_staff_client/stores/vehicle_store.dart';
import 'package:provider/provider.dart';
import 'package:provider/single_child_widget.dart';

class ProviderManager {
  ProviderManager._();
  static List<SingleChildWidget> providers = [
    Provider<VehicleStore>(create: (_) => VehicleStore()),
  ];
}
