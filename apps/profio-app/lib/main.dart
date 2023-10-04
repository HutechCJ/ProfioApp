import 'dart:async';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:geocoding/geocoding.dart';
import 'package:geolocator/geolocator.dart';
import 'package:profio_staff_client/pages/app/app.dart';

Future<int> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  // final mqttProvider = MqttProvider();

  // await mqttProvider.connect();

  // if (mqttProvider.client.connectionStatus!.state ==
  //     MqttConnectionState.connected) {
  //   print('MqttProvider::Mosquitto client connected');
  // } else {
  //   print('MqttProvider::ERROR Mosquitto client connection failed');
  //   return -1;
  // }

  // final Position currentPosition = await LocationManager.getPosition();

  // LocationManager.simulateCarMovement(
  //     mqttProvider,
  //     currentPosition,
  //     RandomLocationGenerator.generatePositionNearby(currentPosition, 1000),
  //     20);

  SystemChrome.setPreferredOrientations([
    DeviceOrientation.portraitUp,
    DeviceOrientation.portraitDown,
  ]).then((value) => runApp(const App()));
  return 0;
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Driver App',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
        useMaterial3: true,
      ),
      home: const MyHomePage(title: 'Welcome to Profio Logistics'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key, required this.title});
  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  int _counter = 0;
  Position? _position;
  String _address = 'Address';

  void _incrementCounter() {
    setState(() {
      _counter++;
    });
  }

  Future<Position> _determinePosition() async {
    bool serviceEnabled;
    LocationPermission permission;
    serviceEnabled = await Geolocator.isLocationServiceEnabled();
    if (!serviceEnabled) {
      return Future.error('Location services are disabled.');
    }

    permission = await Geolocator.checkPermission();
    if (permission == LocationPermission.denied) {
      permission = await Geolocator.requestPermission();
      if (permission == LocationPermission.denied) {
        return Future.error('Location permissions are denied');
      }
    }

    if (permission == LocationPermission.deniedForever) {
      return Future.error(
          'Location permissions are permanently denied, we cannot request permissions.');
    }

    return await Geolocator.getCurrentPosition();
  }

  _getAddressFromCoordinates() async {
    try {
      List<Placemark> placemarks = await placemarkFromCoordinates(
          _position!.latitude, _position!.longitude);

      Placemark place = placemarks[0];

      setState(() {
        _address = '${place.locality}, ${place.country}';
      });
    } catch (e) {
      if (kDebugMode) {
        print(e);
      }
    }
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((timeStamp) async {
      setState(() async {
        _position = await _determinePosition();
        // await _getAddressFromCoordinates();
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Theme.of(context).colorScheme.inversePrimary,
        title: Text(widget.title),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            const Text(
              'You have pushed the button this many times:',
            ),
            Text(
              '$_counter',
              style: Theme.of(context).textTheme.headlineMedium,
            ),
            Text('Address: $_address'),
            Text(
                'Current Position: ${_position?.latitude} - ${_position?.longitude}'),
            ElevatedButton(
                onPressed: () async {
                  await _getAddressFromCoordinates();
                },
                child: const Text('Get location'))
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _incrementCounter,
        tooltip: 'Increment',
        child: const Icon(Icons.add),
      ),
    );
  }
}
