using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/// <summary>
/// Controlelr for the Jet unit
/// </summary>
public class JetUnit : Unit {
    //We want to use the jet positioner to control the position of the jet instead of moving it straight towards the target
    public override void OnTargetChanged(Transform target) {
        JetPositioner positioner = GetComponentInChildren<JetPositioner>();

        positioner.Target = target;
        GetComponent<AIDestinationSetter>().target = positioner.PositionReplication;
    }
}