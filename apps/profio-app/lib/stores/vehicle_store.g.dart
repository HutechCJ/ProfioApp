// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'vehicle_store.dart';

// **************************************************************************
// StoreGenerator
// **************************************************************************

// ignore_for_file: non_constant_identifier_names, unnecessary_brace_in_string_interps, unnecessary_lambdas, prefer_expression_function_bodies, lines_longer_than_80_chars, avoid_as, avoid_annotating_with_dynamic, no_leading_underscores_for_local_identifiers

mixin _$VehicleStore on VehicleStoreBase, Store {
  Computed<bool>? _$hasSelectedVehicleComputed;

  @override
  bool get hasSelectedVehicle => (_$hasSelectedVehicleComputed ??=
          Computed<bool>(() => super.hasSelectedVehicle,
              name: 'VehicleStoreBase.hasSelectedVehicle'))
      .value;

  late final _$vehicleListAtom =
      Atom(name: 'VehicleStoreBase.vehicleList', context: context);

  @override
  ObservableList<Vehicle> get vehicleList {
    _$vehicleListAtom.reportRead();
    return super.vehicleList;
  }

  @override
  set vehicleList(ObservableList<Vehicle> value) {
    _$vehicleListAtom.reportWrite(value, super.vehicleList, () {
      super.vehicleList = value;
    });
  }

  late final _$selectedVehicleAtom =
      Atom(name: 'VehicleStoreBase.selectedVehicle', context: context);

  @override
  Vehicle get selectedVehicle {
    _$selectedVehicleAtom.reportRead();
    return super.selectedVehicle;
  }

  @override
  set selectedVehicle(Vehicle value) {
    _$selectedVehicleAtom.reportWrite(value, super.selectedVehicle, () {
      super.selectedVehicle = value;
    });
  }

  late final _$startHubAtom =
      Atom(name: 'VehicleStoreBase.startHub', context: context);

  @override
  Hub get startHub {
    _$startHubAtom.reportRead();
    return super.startHub;
  }

  @override
  set startHub(Hub value) {
    _$startHubAtom.reportWrite(value, super.startHub, () {
      super.startHub = value;
    });
  }

  late final _$endHubAtom =
      Atom(name: 'VehicleStoreBase.endHub', context: context);

  @override
  Hub get endHub {
    _$endHubAtom.reportRead();
    return super.endHub;
  }

  @override
  set endHub(Hub value) {
    _$endHubAtom.reportWrite(value, super.endHub, () {
      super.endHub = value;
    });
  }

  late final _$fetchVehiclesAsyncAction =
      AsyncAction('VehicleStoreBase.fetchVehicles', context: context);

  @override
  Future<void> fetchVehicles() {
    return _$fetchVehiclesAsyncAction.run(() => super.fetchVehicles());
  }

  late final _$fetchHubPathAsyncAction =
      AsyncAction('VehicleStoreBase.fetchHubPath', context: context);

  @override
  Future<void> fetchHubPath() {
    return _$fetchHubPathAsyncAction.run(() => super.fetchHubPath());
  }

  late final _$visitAsyncAction =
      AsyncAction('VehicleStoreBase.visit', context: context);

  @override
  Future<void> visit() {
    return _$visitAsyncAction.run(() => super.visit());
  }

  late final _$VehicleStoreBaseActionController =
      ActionController(name: 'VehicleStoreBase', context: context);

  @override
  void setVehicle(Vehicle vehicle) {
    final _$actionInfo = _$VehicleStoreBaseActionController.startAction(
        name: 'VehicleStoreBase.setVehicle');
    try {
      return super.setVehicle(vehicle);
    } finally {
      _$VehicleStoreBaseActionController.endAction(_$actionInfo);
    }
  }

  @override
  String toString() {
    return '''
vehicleList: ${vehicleList},
selectedVehicle: ${selectedVehicle},
startHub: ${startHub},
endHub: ${endHub},
hasSelectedVehicle: ${hasSelectedVehicle}
    ''';
  }
}
