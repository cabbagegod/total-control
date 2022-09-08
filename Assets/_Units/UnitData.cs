using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitData {
    public string unitId;
    public int unitCost;
    public GameObject unitPrefab;
    public string requiresStructure;
    public Vector3 offset;
}
