import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter_mobx/flutter_mobx.dart';
import 'package:flutter_polyline_points/flutter_polyline_points.dart';
import 'package:geolocator/geolocator.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:profio_staff_client/managers/location_manager.dart';
import 'package:profio_staff_client/stores/hub_store.dart';
import 'package:profio_staff_client/stores/location_store.dart';
import 'package:provider/provider.dart';

class MapPage extends StatefulWidget {
  const MapPage({
    Key? key,
  }) : super(key: key);

  @override
  State<MapPage> createState() => _MapPageState();
}

class _MapPageState extends State<MapPage> {
  late LocationStore locationStore;
  late HubStore hubStore;
  final Completer<GoogleMapController> _controller =
      Completer<GoogleMapController>();
  final Set<Polyline> _polylines = <Polyline>{};
  final Set<Marker> _markers = <Marker>{};
  int _polylineIdCounter = 1;

  CameraPosition _cameraPosition = const CameraPosition(target: LatLng(0, 0));

  @override
  void initState() {
    locationStore = context.read<LocationStore>();
    hubStore = context.read<HubStore>();
    locationStore.onInit(context);
    if (locationStore.hasSelectedPosition && hubStore.hasSelectedHub) {
      _cameraPosition = CameraPosition(
          target: LatLng(locationStore.selectedPosition!.latitude,
              locationStore.selectedPosition!.longitude),
          zoom: 14.4746);
    }
    super.initState();
  }

  void _setMarker(LatLng point) {
    setState(() {
      _markers.add(
        Marker(
          markerId: const MarkerId('marker'),
          position: point,
        ),
      );
    });
  }

  void _setPolyline(List<PointLatLng> points) {
    final String polylineIdVal = 'polyline_$_polylineIdCounter';
    _polylineIdCounter++;

    _polylines.add(
      Polyline(
        polylineId: PolylineId(polylineIdVal),
        width: 2,
        color: Colors.blue,
        points: points
            .map(
              (point) => LatLng(point.latitude, point.longitude),
            )
            .toList(),
      ),
    );
  }

  // static const CameraPosition _kGooglePlex = CameraPosition(
  //   target: LatLng(37.42796133580664, -122.085749655962),
  //   zoom: 14.4746,
  // );

  // static const CameraPosition _kLake = CameraPosition(
  //     bearing: 192.8334901395799,
  //     target: LatLng(37.43296265331129, -122.08832357078792),
  //     tilt: 59.440717697143555,
  //     zoom: 19.151926040649414);
  void _currentLocation(BuildContext context) async {
    final GoogleMapController controller = await _controller.future;
    if (locationStore.hasSelectedPosition) {
      Position currentLocation = locationStore.selectedPosition as Position;

      _setMarker(LatLng(currentLocation.latitude, currentLocation.longitude));

      controller.animateCamera(CameraUpdate.newCameraPosition(
        CameraPosition(
          bearing: 0,
          target: LatLng(currentLocation.latitude, currentLocation.longitude),
          zoom: 17.0,
        ),
      ));
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Map')),
      body: Column(
        children: [
          IconButton(
            onPressed: () async {
              var directions = await locationStore.getDirections(
                '${locationStore.selectedPosition!.latitude},${locationStore.selectedPosition!.longitude}',
                '${hubStore.selectedHub.location.latitude},${hubStore.selectedHub.location.longitude}',
              );
              _goToPlace(
                directions['start_location']['lat'],
                directions['start_location']['lng'],
                directions['bounds_ne'],
                directions['bounds_sw'],
              );

              _setPolyline(directions['polyline_decoded']);
            },
            icon: const Icon(Icons.line_axis),
          ),
          Observer(
            builder: (_) {
              if (locationStore.selectedPosition != null) {
                _currentLocation(context);
              }
              return const SizedBox(); // This widget doesn't need to render anything
            },
          ),
          Expanded(
            child: GoogleMap(
              mapType: MapType.normal,
              initialCameraPosition: _cameraPosition,
              markers: _markers,
              polylines: _polylines,
              onMapCreated: (GoogleMapController controller) {
                _controller.complete(controller);
              },
            ),
          ),
        ],
      ),
    );
  }

  Future<void> _goToPlace(
    // Map<String, dynamic> place,
    double lat,
    double lng,
    Map<String, dynamic> boundsNe,
    Map<String, dynamic> boundsSw,
  ) async {
    // final double lat = place['geometry']['location']['lat'];
    // final double lng = place['geometry']['location']['lng'];

    final GoogleMapController controller = await _controller.future;
    controller.animateCamera(
      CameraUpdate.newCameraPosition(
        CameraPosition(target: LatLng(lat, lng), zoom: 12),
      ),
    );

    controller.animateCamera(
      CameraUpdate.newLatLngBounds(
          LatLngBounds(
            southwest: LatLng(boundsSw['lat'], boundsSw['lng']),
            northeast: LatLng(boundsNe['lat'], boundsNe['lng']),
          ),
          25),
    );
    _setMarker(LatLng(lat, lng));
  }
}
