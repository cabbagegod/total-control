using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath), typeof(AIDestinationSetter))]
public class Unit : MonoBehaviour {
    public string UnitId { get { return _unitId; } set { _unitId = value; } }
    public int Speed { get { return _speed; } set { _speed = value; } }
    public int Health { get { return _health; } set { _health = value; } }
    public int Damage { get { return _damage; } set { _damage = value; } }

    private string _unitId;
    [SerializeField] private int _speed;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    public void WalkToTarget(Transform target) {
        //Set target
        GetComponent<AIDestinationSetter>().target = target;
        //Invoke for custom implementation
        OnTargetChanged(target);
    }

    public virtual void OnTargetChanged(Transform target) { }
}