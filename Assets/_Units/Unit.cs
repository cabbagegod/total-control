using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Events;

[RequireComponent(typeof(AIPath), typeof(AIDestinationSetter))]
public class Unit : MonoBehaviour {
    public UnityEvent<Unit> OnDeathEvent = new UnityEvent<Unit>();
    public UnityEvent<Unit> OnTargetDestroyed = new UnityEvent<Unit>();
    public Unit Target { get { return _target; } }
    public int Speed { get { return _speed; } }
    public int Health { get { return _health; } }
    public int Damage { get { return _damage; } }
    public string UnitId { get { return _unitId; } set { _unitId = value; } }
    public Country Country { get { return _country; } set { _country = value; } }


    [SerializeField] private int _speed;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown = 1f;
    private float _cooldownStart;
    private Unit _target;
    private Country _country;
    private string _unitId;

    public void AttackTarget(Unit unit) {
        _target = unit;

        if(_target == null)
            MoveToTarget(null);
        else
            MoveToTarget(unit.transform);
    }

    public void MoveToTarget(Transform target) {
        //Set target
        GetComponent<AIDestinationSetter>().target = target;
        //Invoke for custom implementation
        if(target != null)
            OnTargetChanged(target);
    }

    public void DealDamage(Unit unit) {
        if(_cooldownStart + _cooldown >= Time.time)
            return;
        _cooldownStart = Time.time;

        unit.TakeDamage(_damage);
    }

    public bool TakeDamage(int damage) {
        _health -= damage;

        if(Health <= 0) {
            OnDeathEvent?.Invoke(this);
            StartCoroutine(DestroyOnDelay());
            return true;
        }
        return false;
    }

    IEnumerator DestroyOnDelay() {
        yield return new WaitForEndOfFrame();

        Destroy(gameObject);
    }

    public virtual void OnTargetChanged(Transform target) { }
}