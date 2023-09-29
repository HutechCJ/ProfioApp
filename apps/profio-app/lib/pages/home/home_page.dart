import 'package:flutter/material.dart';
import 'package:profio_staff_client/managers/router_manager.dart';
import 'package:profio_staff_client/widgets/hub_info.dart';
import 'package:profio_staff_client/widgets/mqtt_connection_button.dart';
import 'package:profio_staff_client/widgets/simulate_button.dart';
import 'package:profio_staff_client/widgets/vehicle_list.dart';
import 'package:profio_staff_client/widgets/visit_button.dart';
import 'package:shared_preferences/shared_preferences.dart';

class HomePage extends StatelessWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Home')),
      floatingActionButton: IconButton(
        icon: const Icon(Icons.map),
        onPressed: () {
          Navigator.pushNamed(context, RouteManager.map);
        },
      ),
      body: Column(children: [
        const Expanded(
          child: MQTTConnectionButton(),
        ),
        const VehicleList(),
        const HubInfo(),
        const VisitButton(),
        ElevatedButton(
            onPressed: () async {
              final SharedPreferences preferences =
                  await SharedPreferences.getInstance();
              preferences.clear();
            },
            child: const Text('Clear the Direction cache')),
        const SizedBox(
          height: 50,
        ),
        const Expanded(child: SimulateButton()),
      ]),
    );
  }
}
