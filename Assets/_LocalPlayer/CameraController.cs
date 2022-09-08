using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DM.WorldEditor {
    public class CameraController : MonoBehaviour {
        #region Properties
        //Both
        [SerializeField] private Transform orthoCam;
        [SerializeField] private Transform freeCam;
        private bool isFlyMode = false;

        //Ortho Camera
        [SerializeField] private float minZoom = 4;
        [SerializeField] private float maxZoom = 15;
        [SerializeField] private float zoomSpeed = .5f;
        [SerializeField] private float panSpeed = .5f;
        [Tooltip("If the pan speed should be affected by how zoomed in the player is")]
        [SerializeField] private bool useVariableScrollSpeed = true;
        private float originalPanSpeed;

        //Fly Camera
        [SerializeField] private float flySpeed = 10;
        [SerializeField] private float flyCamSpeed = 50;

        #endregion

        // Start is called before the first frame update
        void Start() {
            originalPanSpeed = panSpeed;
        }

        // Update is called once per frame
        void Update() {
            if(Input.GetKeyDown(KeyCode.LeftControl)) {
                isFlyMode = !isFlyMode;
                freeCam.gameObject.SetActive(isFlyMode);
                orthoCam.GetComponent<Camera>().enabled = !isFlyMode;
                orthoCam.GetComponent<AudioListener>().enabled = !isFlyMode;
            }

            if(isFlyMode) {
                orthoCam.transform.position = new Vector3(freeCam.transform.position.x, orthoCam.transform.position.y, freeCam.transform.position.z);

                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                float height = Input.GetAxis("Height");

                Vector3 move = ((freeCam.right * horizontal) + (freeCam.forward * vertical) + (freeCam.up * height)) * flySpeed * Time.deltaTime;

                freeCam.transform.position += move;

                if(Input.GetMouseButton(2)) {
                    freeCam.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), (Input.GetAxis("Mouse X"))) * flyCamSpeed * Time.deltaTime;
                }
            } else {
                if(!EventSystem.current.IsPointerOverGameObject()) {
                    float newZoom = Camera.main.orthographicSize + (-Input.mouseScrollDelta.y) * zoomSpeed;
                    Camera.main.orthographicSize = Mathf.Clamp(newZoom, minZoom, maxZoom);
                }

                if(useVariableScrollSpeed)
                    panSpeed = originalPanSpeed * (Camera.main.orthographicSize / 15);

                if(Input.GetMouseButton(2)) {
                    orthoCam.position += new Vector3((-Input.GetAxis("Mouse X")) * panSpeed, 0, (-Input.GetAxis("Mouse Y") * panSpeed));
                }
            }
        }
    }
}