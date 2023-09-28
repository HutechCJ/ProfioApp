import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter_polyline_points/flutter_polyline_points.dart';
import 'package:geolocator/geolocator.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
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

  void _setSimulateMarker(LatLng point) {
    setState(() {
      _markers.add(Marker(
          markerId: const MarkerId('simulateMarker'),
          position: point,
          icon: BitmapDescriptor.defaultMarkerWithHue(
              BitmapDescriptor.hueAzure)));
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

  void _currentLocation(BuildContext context) async {
    final GoogleMapController controller = await _controller.future;
    if (locationStore.hasSelectedPosition) {
      Position currentLocation = locationStore.selectedPosition as Position;

      _setSimulateMarker(
          LatLng(currentLocation.latitude, currentLocation.longitude));

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
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            children: [
              IconButton(
                onPressed: () async {
                  var directions = await locationStore.getDirections(
                    '${locationStore.selectedPosition!.latitude},${locationStore.selectedPosition!.longitude}',
                    '${hubStore.selectedHub.location.latitude},${hubStore.selectedHub.location.longitude}',
                  );
                  _goToPlace(
                    directions['end_location']['lat'],
                    directions['end_location']['lng'],
                    directions['bounds_ne'],
                    directions['bounds_sw'],
                  );

                  _setPolyline(directions['polyline_decoded']);
                },
                icon: const Icon(Icons.drive_eta),
              ),
              IconButton(
                onPressed: () async {
                  _currentLocation(context);
                },
                icon: const Icon(Icons.spatial_tracking),
              ),
            ],
          ),
          // Observer(
          //   builder: (_) {
          //     if (locationStore.selectedPosition != null) {
          //       _currentLocation(context);
          //     }
          //     return const SizedBox(); // This widget doesn't need to render anything
          //   },
          // ),
          Expanded(
            child: GoogleMap(
              myLocationButtonEnabled: false,
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
