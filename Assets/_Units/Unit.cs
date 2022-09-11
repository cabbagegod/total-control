using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath), typeof(AIDestinationSetter))]
public class Unit : MonoBehaviour {
    public string UnitId { get; set; }
    public int Speed { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }

    public void WalkToTarget(Transform target) {
        //Set target
        GetComponent<AIDestinationSetter>().target = target;
        //Invoke for custom implementation
        OnTargetChanged(target);
    }

    public virtual void OnTargetChanged(Transform target) { }
}