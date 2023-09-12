import 'package:flutter/material.dart';
import 'package:mqtt_client/mqtt_client.dart';
import 'package:profio_staff_client/providers/mqtt_provider.dart';
import 'package:provider/provider.dart';

class MQTTConnectionButton extends StatefulWidget {
  const MQTTConnectionButton({super.key});

  @override
  State<MQTTConnectionButton> createState() => _MQTTConnectionButtonState();
}

class _MQTTConnectionButtonState extends State<MQTTConnectionButton> {
  late MqttProvider mqttProvider;
  bool isConnected = false;

  @override
  void initState() {
    mqttProvider = context.read<MqttProvider>();
    super.initState();
  }

  Future<void> checkConnectionStatus() async {
    await mqttProvider.connect();
    setState(() {
      isConnected = mqttProvider.client.connectionStatus!.state ==
          MqttConnectionState.connected;
    });
  }

  Future<void> connectToMQTT() async {
    await checkConnectionStatus();
  }

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: <Widget>[
          Text(
            isConnected
                ? 'Connected to MQTT Broker'
                : 'Not connected to MQTT Broker',
            style: TextStyle(
              fontSize: 18,
              fontWeight: FontWeight.bold,
              color: isConnected ? Colors.green : Colors.red,
            ),
          ),
          const SizedBox(height: 20),
          ElevatedButton(
            onPressed: () async {
              await connectToMQTT();
            },
            child: const Text('Connect to MQTT Broker'),
          ),
        ],
      ),
    );
  }
}
