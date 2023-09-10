// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'location_store.dart';

// **************************************************************************
// StoreGenerator
// **************************************************************************

// ignore_for_file: non_constant_identifier_names, unnecessary_brace_in_string_interps, unnecessary_lambdas, prefer_expression_function_bodies, lines_longer_than_80_chars, avoid_as, avoid_annotating_with_dynamic, no_leading_underscores_for_local_identifiers

mixin _$LocationStore on LocationStoreBase, Store {
  Computed<bool>? _$hasSelectedPositionComputed;

  @override
  bool get hasSelectedPosition => (_$hasSelectedPositionComputed ??=
          Computed<bool>(() => super.hasSelectedPosition,
              name: 'LocationStoreBase.hasSelectedPosition'))
      .value;

  late final _$selectedPositionAtom =
      Atom(name: 'LocationStoreBase.selectedPosition', context: context);

  @override
  Position? get selectedPosition {
    _$selectedPositionAtom.reportRead();
    return super.selectedPosition;
  }

  @override
  set selectedPosition(Position? value) {
    _$selectedPositionAtom.reportWrite(value, super.selectedPosition, () {
      super.selectedPosition = value;
    });
  }

  late final _$getCurrentLocationAsyncAction =
      AsyncAction('LocationStoreBase.getCurrentLocation', context: context);

  @override
  Future<void> getCurrentLocation() {
    return _$getCurrentLocationAsyncAction
        .run(() => super.getCurrentLocation());
  }

  late final _$LocationStoreBaseActionController =
      ActionController(name: 'LocationStoreBase', context: context);

  @override
  void simulateVehicleMovement() {
    final _$actionInfo = _$LocationStoreBaseActionController.startAction(
        name: 'LocationStoreBase.simulateVehicleMovement');
    try {
      return super.simulateVehicleMovement();
    } finally {
      _$LocationStoreBaseActionController.endAction(_$actionInfo);
    }
  }

  @override
  String toString() {
    return '''
selectedPosition: ${selectedPosition},
hasSelectedPosition: ${hasSelectedPosition}
    ''';
  }
}
