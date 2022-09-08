using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to get unit data from a unique ID
/// A more efficient system should read from resources/addressables so that all data is not constantly in memory
/// </summary>
public class BuildingDatabase : Singleton<BuildingDatabase> {
    [SerializeField] private List<UnitData> _units = new List<UnitData>();
    [SerializeField] private List<StructureData> _structures = new List<StructureData>();

    public UnitData GetUnitFromId(string id) {
        id = id.ToLower();

        foreach(UnitData unit in _units) {
            if(unit.unitId == id) {
                return unit;
            }
        }

        Debug.LogError($"Attempted to get unit from invalid id: {id}");
        return null;
    }

    public StructureData GetStructureFromId(string id) {
        id = id.ToLower();

        foreach(StructureData structure in _structures) {
            if(structure.structureId == id) {
                return structure;
            }
        }

        Debug.LogError($"Attempted to get structure from invalid id: {id}");
        return null;
    }
}