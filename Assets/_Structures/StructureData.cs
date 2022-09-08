using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StructureData {
    public string structureId;
    public int structureCost;
    public GameObject structurePrefab;
    public int maxType;
    public Vector3 offset;
}
