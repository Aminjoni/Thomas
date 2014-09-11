using UnityEngine;
using System.Collections;
using System;

namespace MocapiThomas
{

    public class InputSettings : MonoBehaviour
    {

        public static string assses;
        public static bool showError = false;
        public static bool showInfo = true;
        public static bool showInfoImg = false;
        public Texture2D imgJoyControls;

        /// <summary>
        /// Names of Avatar keyboard axis and buttons
        /// </summary>
        public static string keyMoveAxis = "keyboard Y";
        public static string keyTurnAxis = "keyboard X";
        public static KeyCode keyRunButton = KeyCode.LeftShift;
        public static KeyCode keyStrafeButton = KeyCode.LeftAlt;

        public static KeyCode keyT_long_masunaga = KeyCode.Alpha1;
        public static KeyCode keyT_maagStark = KeyCode.Alpha2;
        public static KeyCode keyT_hart = KeyCode.Alpha3;
        public static KeyCode keyT_blaas = KeyCode.Alpha4;

        public static KeyCode keyT_zwing = KeyCode.Alpha5;
        public static KeyCode keyT_lever = KeyCode.Alpha6;
        public static KeyCode keyT_kant = KeyCode.Alpha7;
        public static KeyCode keyT_behand = KeyCode.Alpha8;

        /// <summary>
        /// Names of Avatar joystick axis and buttons
        /// </summary>
        public static string joyMoveAxis = "joystick Y"; //inverted
        public static string joyTurnAxis = "joystick X"; //non-inverted
        public static string joySitButton = "joystick button 0";
        public static string joyLookButton = "joystick button 1";
        public static string joyStrafeButton = "joystick button 2";
        public static string joyAlertButton = "joystick button 3";

        /// <summary>
        /// Names of Avatar mouse axis
        /// Not used
        /// </summary>
        //public static string mouseMoveAxis = "Mouse Y";
        //public static string mouseTurnAxis = "Mouse X";
        //public static string mouseStrafeAxis = "Mouse X";  //probably not axis but (mouse) key + mouseTurnAxis

        /// <summary>
        /// Names of Camera control axis and buttons
        /// </summary>
        public static string joyCamSwitchButton = "joystick button 4";
        public static string joyCamResetButton = "joystick button 5";
        /// Names of Camera View joystick axis
        public static string joyDPadX = "d-pad X";
        public static string joyDPadY = "d-pad Y";
        public static string joyHatX = "hat X";
        public static string joyHatY = "hat Y";

        string[] inputAxisArray = new string[] { keyMoveAxis, keyTurnAxis, joyMoveAxis, joyTurnAxis, joyDPadX, joyDPadY, joyHatX, joyHatY };
        string[] inputButtonArray = new string[] { joySitButton, joyLookButton, joyStrafeButton, joyAlertButton, joyCamSwitchButton, joyCamResetButton };
        string[] inputKeyArray = new string[] { };

        //Axis availability test
        string axisName;
        bool IsAxisAvailable(string axisName)
        {
            try
            {
                Input.GetAxis(axisName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Button availability test
        string buttonName;
        bool IsButtonAvailable(string buttonName)
        {
            try
            {
                Input.GetAxis(buttonName);
                return true;
            }
            catch
            {
                return false;
            }
        }

    void Awake()
    {

        MocapiThomas.OnScreen.X360Controller = imgJoyControls;
        //Axis availability test
        foreach (var item in inputAxisArray)
        {
            axisName = item;
            if (IsAxisAvailable(axisName) == false)
            {
                showInfo = false;
                showError = true;
            }

        }

        //Button availability test
        foreach (var item in inputButtonArray)
        {
            buttonName = item;
            if (IsButtonAvailable(buttonName) == false)
            {
                showInfo = false;
                showError = true;
            }
        }
    }

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
	void Update () {
        // Toggle Info panel
        if (Input.GetKeyDown(KeyCode.H))
        {
            showInfo = !showInfo;
        }
	}

    void OnGUI()
    {

        // Toggle Info panel
        if (showInfo == true)
        {
            MocapiThomas.OnScreen.PanelInfo();

        }

        // Toggle Info Image panel
        if (showInfoImg == true)
        {
            MocapiThomas.OnScreen.PanelJoyInfo();
        }

        // Toggle Info Error panel
        if (showError == true)
        {
            MocapiThomas.OnScreen.ErrorInfo();
        }
    }

}
}