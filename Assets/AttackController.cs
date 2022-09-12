using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController {
    List<Unit> _units = new List<Unit>();
    Country _attackingCountry;
    Country _defendingCountry;
    public bool IsAttackOver { get { return _isAttackOver; } }
    private bool _isAttackOver = false;

    public AttackController(Country attackingCountry, Country defendingCountry, List<Unit> units) {
        _units = units;
        _attackingCountry = attackingCountry;
        _defendingCountry = defendingCountry;

        StartBattle();
    }

    void StartBattle() {
        if(!CountryHasUnits(_defendingCountry)) {
            EndBattle(true);
            return;
        }

        foreach(Unit unit in _units) {
            unit.OnDeathEvent.AddListener(OnDeath);
            unit.OnTargetDestroyed.AddListener(OnTargetDestroyed);
        }
        AssignTargets();
    }

    void AssignTargets() {
        //Get all units in the defending country
        List<Unit> units = _defendingCountry.GetReserveUnits();

        Debug.Log(units.Count);

        foreach(Unit unit in _units) {
            //Get a random unit from the defending country
            Unit target = units[Random.Range(0, units.Count)];

            //Assign the target to the unit
            unit.MoveToTarget(target.transform);
        }
    }

    void EndBattle(bool isWin) {
        _isAttackOver = true;
        
        if(isWin) {
            GameObject.Destroy(_defendingCountry.gameObject);
            RemoveListeners();

            foreach(Unit unit in _units) {
                UnitData data = BuildingDatabase.Instance.GetUnitFromId(unit.UnitId);
                unit.AttackTarget(null);
                unit.transform.position = _attackingCountry.transform.position + data.offset;

                _attackingCountry.MoveUnitBackToReserve(unit);
            }
        } else {
            _units = null;
        }
    }

    void OnDeath(Unit unit) {
        _units.Remove(unit);
        _attackingCountry.RemoveUnit(unit);
    }

    void OnTargetDestroyed(Unit unit) {
        if(!CountryHasUnits(_defendingCountry)) {
            //victory
            EndBattle(true);
        }
    }

    bool CountryHasUnits(Country country) {
        return country.GetReserveUnits().Count != 0;
    }

    void RemoveListeners() {
        foreach(Unit unit in _units) {
            unit.OnDeathEvent.RemoveListener(OnDeath);
            unit.OnTargetDestroyed.RemoveListener(OnTargetDestroyed);
        }
    }
}
