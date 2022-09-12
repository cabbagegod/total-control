using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPositioner : MonoBehaviour {
    //This is what the plane's destination setter targets
    [System.NonSerialized] public Transform PositionReplication;
    //This is the actual target that the plane should be flying towards/around
    public Transform Target;
    //How often we update the target position
    public float UpdateSpeed = 1;
    //Adjusts speed of the rotation lerp
    public float RotationSpeed = 10;
    public float forwardDistance = 20;

    private Vector3 _originalPosition = Vector3.zero;
    private Vector3 _targetPosition;
    private float _time = 0;

    void Start() {
        StartCoroutine(UpdatePosition());
        PositionReplication = new GameObject().transform;
        PositionReplication.gameObject.name = "Jet Position";
    }

    void Update() {
        if(Target == null) {
            return;
        }

        //Reset to center of plane and then face the target
        transform.localPosition = Vector3.zero;
        transform.LookAt(_targetPosition);

        //Calculate lerp to face the target
        transform.position = Vector3.Lerp(_originalPosition, _targetPosition, _time / RotationSpeed);
        _time += Time.deltaTime;

        PositionReplication.transform.position = transform.position;
    }

    //Sets the position of the target
    IEnumerator UpdatePosition() {
        while(true) {
            if(Target == null) {
                yield return new WaitForSeconds(1);
                continue;
            }
            if(UpdateSpeed == 0) {
                Debug.LogError("Update speed is too low");
                yield return new WaitForSeconds(1);
                continue;
            }

            _originalPosition = transform.position;
            _time = 0;

            //Reset to center of plane and then face the target
            transform.localPosition = Vector3.zero;
            transform.LookAt(Target.position);

            transform.position = transform.forward * forwardDistance;

            _targetPosition = new Vector3(transform.position.x, 0, transform.position.z);

            yield return new WaitForSeconds(UpdateSpeed);
        }
    }
}
