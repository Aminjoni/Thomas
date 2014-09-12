using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace MocapiThomas
{

    public class CameraGUI : MonoBehaviour
    {

        //Gui elements
        public Texture2D controlTex_Up;
        public Texture2D controlTex_Down;
        public Texture2D controlTex_Left;
        public Texture2D controlTex_Right;
        public Texture2D controlTex_High;
        public Texture2D controlTex_Low;
        private int buttWH = 48;

        //Values
        public GUISkin MocapiSkin = null;
        int bottomMargin = 21;
        int leftMargin = 5;

        //make values available in other scripts
        public static bool Up = false;
        public static bool Left = false;
        public static bool Right = false;
        public static bool Down = false;
        public static bool High = false;
        public static bool Low = false;

        //Current Motion Label
        private string MotionLabel;
        private string hover;

        void OnGUI()
        {

            //Load the skin
            GUI.skin = MocapiSkin;
            MotionLabel = MocapiThomas.CharacterControlThomas.MotionLabel;
            // Viewport Label
            GUI.Label(new Rect(0, 10, Screen.width, 100), MotionLabel);

            // button up - camera forward
            if (GUI.RepeatButton(new Rect(leftMargin + buttWH, Screen.height - bottomMargin - (buttWH * 3), buttWH, buttWH), controlTex_Up)) { Up = true; }
            else Up = false;

            // button down - camera back
            if (GUI.RepeatButton(new Rect(leftMargin + buttWH, Screen.height - bottomMargin - buttWH, buttWH, buttWH), controlTex_Down)) { Down = true; }
            else Down = false;

            // button left - camera left
            if (GUI.RepeatButton(new Rect(leftMargin, Screen.height - bottomMargin - (buttWH*2), buttWH, buttWH), controlTex_Left)) { Left = true; }
            else Left = false;

            // button right - camera right
            if (GUI.RepeatButton(new Rect(leftMargin + (buttWH * 2), Screen.height - bottomMargin - (buttWH * 2), buttWH, buttWH), controlTex_Right)) { Right = true; }
            else Right = false;


            // button higher - camera up
            if (GUI.RepeatButton(new Rect(leftMargin + (buttWH * 3), Screen.height - bottomMargin - (buttWH * 3), buttWH, buttWH), controlTex_High)) { High = true; }
            else High = false;

            // button lower - camera down
            if (GUI.RepeatButton(new Rect(leftMargin + (buttWH * 3), Screen.height - bottomMargin - buttWH, buttWH, buttWH), controlTex_Low)) { Low = true; }
            else Low = false;

            // Button Group - all motions
            GUI.BeginGroup(rectMenuContainer);
            GUI.Box(new Rect(0, 0, rectMenuContainer.width, rectMenuContainer.height), "", styleTransp); //"Shiatsu Motions"
            //create all buttons from array
            int b = 0;
            while (b < dictMotions.Count)
            {
                GUI.Button(new Rect(0, (b * (buttMotionsH + buttMotionsSpacing)), buttMotionsW, buttMotionsH), dictMotions[b], styleFlatButton);
                ++b;
            }
            // End the group we started above. This is very important to remember!
            GUI.EndGroup();

            //Make motion buttons visible on hover
            Event eButtons = Event.current;
            if (eButtons.mousePosition.x > rectMenuContainer.xMin &&
                eButtons.mousePosition.y > rectMenuContainer.yMin &&
                eButtons.mousePosition.x < rectMenuContainer.xMax &&
                eButtons.mousePosition.y < rectMenuContainer.yMax)
            {
                alphaTarget = 250f;
                styleFlatButton.normal.background = textureNorm;
            }
            else
            {
                alphaTarget = 20f;
                styleFlatButton.normal.background = textureNormWeak;
            }

        }

        
        private int buttMotionsH = 20;
        private int buttMotionsW = 98;
        private int buttMotionsSpacing = 2;
        private Dictionary<int, string> dictMotions = new Dictionary<int, string>();
        private Rect rectMenuContainer;
        private GUIStyle styleFlatButton;
        private GUIStyle styleTransp;
        private float alphaText;
        private float alphaTarget;
        private Color32 color32text;
        private Texture2D textureNormWeak;
        private Texture2D textureNorm;


        void Start()
        {

            //OK
            dictMotions.Add(0, " Long Masunaga ");
            dictMotions.Add(1, " Maag Starking ");
            dictMotions.Add(2, " Hart ");
            dictMotions.Add(3, " Blaas ");
            dictMotions.Add(4, " Zwing ");
            dictMotions.Add(5, " Lever ");
            dictMotions.Add(6, " Kant Streking ");
            dictMotions.Add(7, " Behandeling ");
            dictMotions.Add(8, " Masunaga 2 ");
            dictMotions.Add(9, " Masunaga 3 ");
            dictMotions.Add(10, " Masunaga 4 ");
            dictMotions.Add(11, " Warming Up ");
            dictMotions.Add(12, " Long ");
            dictMotions.Add(13, " Maag ");
            dictMotions.Add(14, " Nier ");
            dictMotions.Add(15, " Warmer ");
            dictMotions.Add(16, " Hart ");
            dictMotions.Add(17, " Rug Sterking  ");
            dictMotions.Add(18, " Lever Sterking  ");
            dictMotions.Add(19, " Behandeling ");
            dictMotions.Add(20, " Behandeling 2 ");
            
            //GUI Container for the motion menu
            rectMenuContainer = new Rect(Screen.width - buttMotionsW - leftMargin, Screen.height - dictMotions.Count*(buttMotionsH+buttMotionsSpacing) - (bottomMargin/2), buttMotionsW, dictMotions.Count*(buttMotionsH+buttMotionsSpacing));

            //color definitions
            textureNormWeak = new Texture2D(128, 128);
            textureNorm = new Texture2D(128, 128);
            Texture2D textureHover = new Texture2D(128, 128);
            Color32 color32NormWeak = new Color32(184, 195, 201, 20);
            Color32 color32Norm = new Color32(184, 195, 201, 90);
            Color32 color32Hover = new Color32(222, 135, 170, 90);
            color32text = new Color32(94, 116, 123, 0);


            //create background textures from colors
            int y = 0;
            while (y < textureNorm.height)
            {
                int x = 0;
                while (x < textureNorm.width)
                {
                    textureNormWeak.SetPixel(x, y, color32NormWeak);
                    textureNorm.SetPixel(x, y, color32Norm);
                    textureHover.SetPixel(x, y, color32Hover);

                    ++x;
                }
                ++y;
            }
            textureNormWeak.Apply();
            textureNorm.Apply();
            textureHover.Apply();

            //define style for transparent elements
            styleTransp = new GUIStyle();
            styleTransp.normal.background = null;

            
            //define style for flat elements
            styleFlatButton = new GUIStyle();
            styleFlatButton.normal.background = textureNorm;
            styleFlatButton.hover.background = textureHover;
            styleFlatButton.normal.textColor = color32text;
            styleFlatButton.hover.textColor = Color.black;
            styleFlatButton.fontSize = 12;
            styleFlatButton.fontStyle = FontStyle.Normal;
            styleFlatButton.alignment = TextAnchor.MiddleCenter;

        }

        void Update() 
        {
            alphaText = Mathf.Lerp(alphaText, alphaTarget, Time.deltaTime * 5f);
            byte inOut =  (byte)alphaText;
            color32text.a = inOut;
            styleFlatButton.normal.textColor = color32text;
        }
    }
}