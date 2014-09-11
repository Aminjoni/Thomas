using UnityEngine;
using System.Collections;

namespace MocapiThomas
{
    public class OnScreen : MonoBehaviour
    {

        public static void PanelInfo()
        {
            //style definitions
            Color guiOrangeColor = new Color32(232, 138, 27, 255);   

            GUIStyle mainStyle = new GUIStyle();
            mainStyle.normal.textColor = Color.white;
            mainStyle.fontSize = 13;
            mainStyle.alignment = TextAnchor.UpperLeft;
            mainStyle.wordWrap = false;

            GUIStyle mainStyleCentered = new GUIStyle();
            mainStyleCentered.normal.textColor = Color.white;
            mainStyleCentered.fontSize = mainStyle.fontSize;
            mainStyleCentered.alignment = TextAnchor.UpperCenter;
            mainStyleCentered.wordWrap = true;

            GUIStyle headerStyleCentered = new GUIStyle();
            headerStyleCentered.normal.textColor = guiOrangeColor;
            headerStyleCentered.fontSize = mainStyle.fontSize + 2;
            headerStyleCentered.fontStyle = FontStyle.Bold;
            headerStyleCentered.alignment = TextAnchor.UpperCenter;

            GUIStyle headerStyleSub = new GUIStyle();
            headerStyleSub.normal.textColor = guiOrangeColor;
            headerStyleSub.fontSize = mainStyle.fontSize + 2;
            headerStyleSub.fontStyle = FontStyle.Bold;
            headerStyleSub.alignment = TextAnchor.UpperLeft;

            //ui dimensions
            int uiWidth = 220;
            int uiHeight = Screen.height - 10;

            // Group 
            GUI.BeginGroup(new Rect(5, 5, uiWidth, uiHeight));

            // Box background.
            GUI.Box(new Rect(0, 0, uiWidth, uiHeight), "");

            // Contents
            GUI.Label(new Rect(0, 10, uiWidth, 100), MocapiCameraSwitcher.camActive.name, headerStyleCentered);
            GUI.Label(new Rect(0, 30, uiWidth, 100), "(C to change, H to hide)", mainStyleCentered);

            GUI.Label(new Rect(5, 70, uiWidth, 120), "Zoom: PgUp PgDn, +/-", mainStyle);
            GUI.Label(new Rect(5, 90, uiWidth, 120), "Ortho Views: Dpad, Keypad Arrows", mainStyle);
            GUI.Label(new Rect(5, 110, uiWidth, 120), "Reset Camera: Home, Keypad 5", mainStyle);

            GUI.Label(new Rect(5, 140, uiWidth, 120), "Gamepad", headerStyleSub);
            GUI.Label(new Rect(5, 160, uiWidth, 120), "Move: Stick", mainStyle);
            GUI.Label(new Rect(5, 180, uiWidth, 120), "Sidestep: Button 2 + Stick X", mainStyle);
            GUI.Label(new Rect(5, 200, uiWidth, 120), "Look Left/Right: Button 1 + Stick X", mainStyle);
            GUI.Label(new Rect(5, 220, uiWidth, 120), "Alert: Button 3", mainStyle);
            GUI.Label(new Rect(5, 240, uiWidth, 120), "Sit Down: Button 0", mainStyle);
            GUI.Label(new Rect(5, 260, uiWidth, 120), "Stop from Run: Alt", mainStyle);

            GUI.Label(new Rect(5, 290, uiWidth, 120), "Keyboard", headerStyleSub);
            GUI.Label(new Rect(5, 310, uiWidth, 120), "Move: Arrows, AWSD", mainStyle);
            GUI.Label(new Rect(5, 330, uiWidth, 120), "Run: Shift + Arrows", mainStyle);
            GUI.Label(new Rect(5, 350, uiWidth, 120), "SideStep: Alt + Arrows", mainStyle);
            GUI.Label(new Rect(5, 370, uiWidth, 120), "Look L/R: Ctrl + Arrows", mainStyle);
            GUI.Label(new Rect(5, 390, uiWidth, 120), "Alert: Z", mainStyle);
            GUI.Label(new Rect(5, 410, uiWidth, 120), "Sit Down: X", mainStyle);
            GUI.Label(new Rect(5, 430, uiWidth, 120), "Stop from Run: Alt", mainStyle);

            if (GUI.Button(new Rect(5, uiHeight-50, uiWidth-10, 40), "Controls Image"))
                MocapiThomas.InputSettings.showInfoImg = !MocapiThomas.InputSettings.showInfoImg;

            // End the group we started above
            GUI.EndGroup();
        }

        public static Texture2D X360Controller;

        //Format Joystick image panel
        public static void PanelJoyInfo()
        {
            //ui dimensions
            int uiWidth = 768;
            int uiHeight = 384;

            GUIStyle boxStyle = new GUIStyle();

            // Group 
            GUI.BeginGroup(new Rect(Screen.width / 2 - uiWidth / 2, Screen.height / 2 - uiHeight / 2, uiWidth, uiHeight));

            // Box background and contents.
            GUI.Box(new Rect(0, 0, uiWidth, uiHeight), X360Controller, boxStyle);

            if (GUI.Button(new Rect(uiWidth/2-50, uiHeight-60, 100, 40), "Close"))
                MocapiThomas.InputSettings.showInfoImg = false;

            // End the group we started above
            GUI.EndGroup();
        }


        //Format Error Info panel
        public static void ErrorInfo()
        {
            //ui dimensions
            int uiWidth = Screen.width - Screen.width / 8; 
            int uiHeight = 200;

            //ui style definitions
            Color guiOrangeColor = new Color32(232, 138, 27, 255);
            
            GUIStyle headerStyleCentered = new GUIStyle();
            headerStyleCentered.normal.textColor = guiOrangeColor;
            headerStyleCentered.fontSize = 22;
            headerStyleCentered.fontStyle = FontStyle.Bold;
            headerStyleCentered.alignment = TextAnchor.UpperCenter;

            GUIStyle mainStyleCentered = new GUIStyle();
            mainStyleCentered.normal.textColor = Color.white;
            mainStyleCentered.fontSize = 18;
            mainStyleCentered.alignment = TextAnchor.MiddleCenter;
            mainStyleCentered.wordWrap = true;

            // Group 
            GUI.BeginGroup(new Rect(Screen.width / 2 - uiWidth/2, Screen.height / 2 - uiHeight/2, uiWidth, uiHeight));

            // Box background.
            GUI.Box(new Rect(0, 0, uiWidth, uiHeight), "");
            GUI.Box(new Rect(6, 6, uiWidth-12, uiHeight-12), "");

            GUI.Label(new Rect(0, 20, uiWidth, 20), "Input Manager Error.", headerStyleCentered);
            GUI.Label(new Rect(0, 40, uiWidth, uiHeight - 40), "Please open included InputManager.zip \n and replace \\ProjectSettings\\InputManager.asset with the one from the ZIP. \n Backup your InputManager.asset if necessary.", mainStyleCentered);

            // End the group we started above
            GUI.EndGroup();
        }
    
    }
}