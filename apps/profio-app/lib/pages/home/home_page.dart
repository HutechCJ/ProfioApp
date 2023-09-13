import 'package:flutter/material.dart';
import 'package:profio_staff_client/managers/router_manager.dart';
import 'package:profio_staff_client/widgets/hub_info.dart';
import 'package:profio_staff_client/widgets/mqtt_connection_button.dart';
import 'package:profio_staff_client/widgets/simulate_button.dart';
import 'package:profio_staff_client/widgets/vehicle_list.dart';
import 'package:profio_staff_client/widgets/visit_button.dart';

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
      body: const Column(children: [
        Expanded(
          child: MQTTConnectionButton(),
        ),
        VehicleList(),
        HubInfo(),
        VisitButton(),
        SizedBox(
          height: 100,
        ),
        Expanded(child: SimulateButton()),
      ]),
    );
  }
}
