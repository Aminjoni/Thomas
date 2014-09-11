using UnityEngine;
using System.Collections;

namespace MocapiThomas
{
    public class MocapiCameraSwitcher : MonoBehaviour
    {
        public static Camera camActive;

        /// Name of the joystick buttons
        string joyCamSwitchButton=MocapiThomas.InputSettings.joyCamSwitchButton; // camera switch

        // Use this for initialization
        void Start()
        {

            Camera[] allCams = FindObjectsOfType(typeof(Camera)) as Camera[];

            // Set initial camera
            foreach (Camera cam in allCams)
            {
                cam.enabled = false;
                //Debug.Log(cam.name);
            }
            allCams[1].enabled = true;
            camActive = allCams[1];

        }

        // Update is called once per frame
        void Update()
        {

            //Process Joystick button
            if (Input.GetButtonDown(joyCamSwitchButton))
            {
                CamSwitch();
            }

        }

        void CamSwitch()
        {
            Camera[] allCams = FindObjectsOfType(typeof(Camera)) as Camera[];

            for (int i = 0; i < allCams.Length; i++)
            {
                if (allCams[i].enabled == true)
                {
                    allCams[i].enabled = false;
                    if (i == allCams.Length - 1)
                    {
                        allCams[0].enabled = true;
                        camActive = allCams[0];
                    }
                    else
                    {
                        allCams[i + 1].enabled = true;
                        camActive = allCams[i + 1];
                    }
                    break;
                }
            }
        }

    }
}
