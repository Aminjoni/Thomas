using UnityEngine;
using System.Collections;

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

            //////test1 OK
            //Event e = Event.current;
            //if (e.mousePosition.x > Screen.width-20 && 
            //    e.mousePosition.y > 0 && 
            //    e.mousePosition.x < Screen.width && 
            //    e.mousePosition.y < 20)
            //    Debug.Log("Mouse Here!" + e.mousePosition);


            ////test2 OK
            //GUILayout.Button("My button");
            //if (Event.current.type == EventType.Repaint && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
            //    GUILayout.Label("Mouse over!");
            //else
            //    GUILayout.Label("Mouse somewhere else");

            // Make a group on the center of the screen
            GUI.BeginGroup(rectContainer);
            // All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

            // We'll make a box so you can see where the group is on-screen.
            GUI.Box(new Rect(0, 0, rectContainer.width, rectContainer.height), "Group is here", styleFlat);
            GUI.Button(new Rect(10, 40, 80, 30), "Click me", styleFlat);

            // End the group we started above. This is very important to remember!
            GUI.EndGroup();

        }

        private Rect rectContainer = new Rect(Screen.width-200, 20, 180, 100);

        private GUIStyle styleFlat;
        //Texture2D textureNorm = new Texture2D(128, 128);

        void Start()
        {
            ////color as texture
            //Color32 color32 = new Color32(25, 25, 25, 128);
            //Texture2D texture2 = new Texture2D(128, 128);
            ////texture2.SetPixels(Color32);
            //texture2.Apply();

            //BGTextNorm();
            //BGTextHover();

            //Color32 colorNorm = Color.white;
            //Color32 colorHover = Color.cyan;



            ////color as texture
            Texture2D texture = new Texture2D(128, 128);
            Texture2D textureNorm = new Texture2D(128, 128);
            Texture2D textureHover = new Texture2D(128, 128);

            int y = 0;
            while (y < texture.height)
            {
                int x = 0;
                while (x < texture.width)
                {
                    Color color = Color.magenta;
                    texture.SetPixel(x, y, color);
                    ++x;
                }
                ++y;
            }
            texture.Apply();

            

            styleFlat = new GUIStyle();

            styleFlat.normal.background = texture;
            //styleFlat.normal.background = textureNorm;
            //styleFlat.hover.background = textureHover;            
            styleFlat.normal.textColor = Color.cyan;
            styleFlat.fontSize = 14;
            styleFlat.fontStyle = FontStyle.Bold;
                }

            //Texture2D textureNorm = new Texture2D(128, 128);

            //void BGTextNorm()
            //{
            //    //ButtonNormal color as texture
            //    int y = 0;
            //    while (y < textureNorm.height)
            //    {
            //        int x = 0;
            //        while (x < textureNorm.width)
            //        {
            //            Color color = Color.white;
            //            textureNorm.SetPixel(x, y, color);
            //            ++x;
            //        }
            //        ++y;
            //    }
            //    textureNorm.Apply();
            //}

            //Texture2D textureHover = new Texture2D(128, 128);

            //void BGTextHover()
            //{
            //    //ButtonNormal color as texture
            //    int y = 0;
            //    while (y < textureHover.height)
            //    {
            //        int x = 0;
            //        while (x < textureHover.width)
            //        {
            //            Color color = Color.cyan;
            //            textureHover.SetPixel(x, y, color);
            //            ++x;
            //        }
            //        ++y;
            //    }
            //    textureHover.Apply();

            //}

        void Update()
        {

                //Debug.Log(hover);
        }
    }
}