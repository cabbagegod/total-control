using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller for the Tank Unit
/// </summary>
public class TankUnit : Unit {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(Target == null)
            return;

        if(Vector3.Distance(transform.position, Target.transform.position) <= 6) {
            DealDamage(Target);
        }
    }
}
