using UnityEngine;
using System.Collections;

namespace MocapiThomas
{

    public class FixedCamLocal : MonoBehaviour
    {


        private GameObject targetFollow;     //Runtime object that will follow the target and work as camera aim)
        public Transform target;            //the target that we follow

        //damping parameters
        private float smoothTime = .3F;
        private float xVelocity = 0.0F;
        private float yVelocity = 0.0F;
        private float zVelocity = 0.0F;
        private float maxSpeed = Mathf.Infinity;


        private float cameraRight = 0f;
        private float cameraForward = 0f;
        private float cameraUp = 0f;

        void Start()
        {

            targetFollow = new GameObject("targetFollow");              // Create Runtime object that will follow the target and work as camera aim)
            if (target == null) { target = GameObject.Find("Hips").transform; }                 // Get target avatar's transform


        }

        // Update is called once per frame
        void Update()
        {

            //Damp position and velocity of the Camera Aim
            float newPositionX = Mathf.SmoothDamp(targetFollow.transform.position.x, target.position.x, ref xVelocity, smoothTime);
            float newPositionY = Mathf.SmoothDamp(targetFollow.transform.position.y, target.position.y, ref yVelocity, smoothTime);
            float newPositionZ = Mathf.SmoothDamp(targetFollow.transform.position.z, target.position.z, ref zVelocity, smoothTime);

            targetFollow.transform.position = new Vector3(newPositionX, newPositionY, newPositionZ);
            transform.LookAt(targetFollow.transform, Vector3.up);


            //Change Camera Poition
            GuiInput();
            //camPos = new Vector3(camPosX, camPosY, camPosZ);
            //transform.position = camPos;
            //transform.localPosition = new Vector3(camPosX, camPosY, camPosZ);

            transform.Translate(Vector3.forward * cameraForward);
            transform.Translate(Vector3.right * cameraRight);
            transform.Translate(Vector3.up * cameraUp);


        }

        //Process GUI input booleans
        void GuiInput()
        {

            ////move camera up
            //if (MocapiLiveStream.CameraGUI.High == true) { camPosY = camPosY + 0.1f; }

            ////move camera down
            //if (MocapiLiveStream.CameraGUI.Low == true) { camPosY = camPosY - 0.1f; }

            //move camera up or down
            if (MocapiThomas.CameraGUI.High == true) { cameraUp = cameraUp + (.5f * Time.deltaTime); }
            else if (MocapiThomas.CameraGUI.Low == true) { cameraUp = cameraUp + (-.5f * Time.deltaTime); }
            else { cameraUp = 0f; }

            ////move camera up or down Experimental
            //if (MocapiLiveStream.CameraGUI.High == true) { transform.RotateAround(targetFollow.transform.position, Vector3.left, 200 * Time.deltaTime); }
            //else if (MocapiLiveStream.CameraGUI.Low == true) { transform.RotateAround(targetFollow.transform.position, Vector3.right, -200 * Time.deltaTime); }
            //else { cameraUp = 0f; }

            //move camera closer or away
            if (MocapiThomas.CameraGUI.Up == true) { cameraForward = cameraForward + (.5f * Time.deltaTime); }
            else if (MocapiThomas.CameraGUI.Down == true) { cameraForward = cameraForward + (-.5f * Time.deltaTime); }
            else { cameraForward = 0f; }


            //move camera right or left
            if (MocapiThomas.CameraGUI.Right == true) { transform.RotateAround(targetFollow.transform.position, Vector3.up, 200 * Time.deltaTime); }
            else if (MocapiThomas.CameraGUI.Left == true) { transform.RotateAround(targetFollow.transform.position, Vector3.up, -200 * Time.deltaTime); }
            else { cameraRight = 0f; }



        }

    }
}