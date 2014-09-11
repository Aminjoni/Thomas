using UnityEngine;
using System.Collections;

namespace MocapiThomas
{
    public class MocapiCameraScrolling : MonoBehaviour
    {
        public float smooth = 3f;		// a public variable to adjust smoothing of camera motion
        public float camZoom = 60f;         //camera FieldOfView

        Vector3 cameraOffset;
        public Transform avatarTransf;

        /// Names of Camera control axis and buttons
        //string joyCameraLeftRight = Mocapianimation.InputSettings.joyCameraLeftRight;
        //string joyCameraFrontBack = Mocapianimation.InputSettings.joyCameraFrontBack;
        string joyCamResetButton = MocapiThomas.InputSettings.joyCamResetButton;

        void Start()
        {

            //Get camera target


            if (avatarTransf == null)
            {

                if (GameObject.Find("MocapiMan/MocapiMan_Root/Hips") != null)
                {
                    avatarTransf = GameObject.Find("MocapiMan/MocapiMan_Root/Hips").transform;  //get target avatar's transform
                }
                else
                {
                    Debug.Log("No camera target assigned. Trying to find alternative");          // Error if no camera target assigned
                    avatarTransf = GameObject.Find("Hips").transform;  //get target avatar's transform
                }

            }

        }

        void Update()
        {
            PositionChange();

            cameraOffset = new Vector3(0f, 1f, -3f);

            // set the camera position and direction
            transform.position = Vector3.Lerp(transform.position, avatarTransf.position + cameraOffset, Time.deltaTime * smooth);
        }

        //Camera Placement Control
        void PositionChange()
        {
            //Camera Zoom
            camera.fieldOfView = camZoom;
            if (Input.GetKey(KeyCode.PageUp) || Input.GetKey(KeyCode.KeypadMinus))
            {
                camZoom = camZoom - (10 * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.PageDown) || Input.GetKey(KeyCode.KeypadPlus))
            {
                camZoom = camZoom + (10 * Time.deltaTime);
            }

            //Reset Camera
            if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Keypad5) || Input.GetButtonDown(joyCamResetButton))
            {
                camZoom = 60f;
            }
        }
    }
}
