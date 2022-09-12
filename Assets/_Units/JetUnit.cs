using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/// <summary>
/// Controlelr for the Jet unit
/// </summary>
public class JetUnit : Unit {
    public Transform Renderer;
    private bool _isFlying = false;

    //We want to use the jet positioner to control the position of the jet instead of moving it straight towards the target
    public override void OnTargetChanged(Transform target) {
        JetPositioner positioner = GetComponentInChildren<JetPositioner>();

        positioner.Target = target;
        GetComponent<AIDestinationSetter>().target = positioner.PositionReplication;
        
        if(!_isFlying)
            StartCoroutine(Takeoff());
    }

    IEnumerator Takeoff() {
        _isFlying = true;

        for(int i = 0; i < 100; i++) {
            Renderer.position += Vector3.up * 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Land() {
        _isFlying = false;

        for(int i = 0; i < 100; i++) {
            Renderer.position -= Vector3.up * 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}