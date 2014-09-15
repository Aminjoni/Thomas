using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MocapiThomas
{ 

	/// <summary>
    /// Controller for a character.Direction
	/// This class will process user inputs and translate it to animation.
	/// The character will be animated based on predefined animations
	/// by a Mecanim animator.
	/// </summary> 
	public class CharacterControlThomas : MonoBehaviour 
	{
        /// <summary>
        /// Multiple Idle states
        /// list of available idle animations' vectors in 2D BlendTree (you need a corresponding 2D blendtree in Animator Controller)
        /// </summary>       
        Vector2 idle1 = new Vector2(0f, 0f);
        Vector2 idle2 = new Vector2(0f, 1f);
        Vector2 idle3 = new Vector2(-1f, 0.2f);
        Vector2 idle4 = new Vector2(1f, 0.2f);
        Vector2 idle5 = new Vector2(-1f, -1f);
        Vector2 idle6 = new Vector2(1f, -1f);
        /// <summary>
        /// array holding Idle Animations' vectors in 2D BlendTree
        /// </summary>
        Vector2[] idleAnimsList; 
        /// <summary>
        /// 2D blend tree, containing all idle variants
        /// </summary>
        static int idleBlendTree = Animator.StringToHash("Base Layer.STAND_IDLE");

        /// <summary>
        //Thomas Clips 
        /// </summary>
        public static Dictionary<int, string> dictMotions = new Dictionary<int, string>();

        static int ClipLongMasunaga = Animator.StringToHash("Base Layer.LongMasunaga");
        static int ClipMaagStarking = Animator.StringToHash("Base Layer.MaagStarking");
        static int ClipHart1 = Animator.StringToHash("Base Layer.Hart1");
        static int ClipBlaas = Animator.StringToHash("Base Layer.Blaas");
        static int ClipZwing = Animator.StringToHash("Base Layer.Zwing");
        static int ClipLever = Animator.StringToHash("Base Layer.Lever");
        static int ClipKantStreking = Animator.StringToHash("Base Layer.KantStreking");
        static int ClipBehandeling1 = Animator.StringToHash("Base Layer.Behandeling1");
        static int ClipMasunaga2 = Animator.StringToHash("Base Layer.Masunaga2");
        static int ClipMasunaga3 = Animator.StringToHash("Base Layer.Masunaga3");
        static int ClipMasunaga4 = Animator.StringToHash("Base Layer.Masunaga4");
        static int ClipWarmingUp = Animator.StringToHash("Base Layer.WarmingUp");
        static int ClipLong = Animator.StringToHash("Base Layer.Long");
        static int ClipMaag = Animator.StringToHash("Base Layer.Maag");
        static int ClipNier = Animator.StringToHash("Base Layer.Nier");
        static int ClipWarmer = Animator.StringToHash("Base Layer.Warmer");
        static int ClipHart2 = Animator.StringToHash("Base Layer.Hart2");
        static int ClipRugSterking = Animator.StringToHash("Base Layer.RugSterking");
        static int ClipLeverSterking = Animator.StringToHash("Base Layer.LeverSterking");
        static int ClipBehandeling2 = Animator.StringToHash("Base Layer.Behandeling2");
        static int ClipBehandeling3 = Animator.StringToHash("Base Layer.Behandeling3");

        /// <summary>
        /// flag to indicate if any valid input was given
        /// or the animation needs to stay in idle
        /// </summary>
        private bool idle;
        /// <summary>
        /// flag to indicate if idle state can change
        /// </summary>
        private bool canChangeState = true;

        /// <summary>
        /// current idle state
        /// </summary>
        private Vector2 currentIdleVariant; 
        /// <summary>
        /// next idle state
        /// </summary>
        private Vector2 nextIdleVariant;     
        /// <summary>
        /// next idle animation number from a list
        /// </summary>
        private int NextIdle;
        /// <summary>
        /// how much we want to smooth transition between idle animations
        /// </summary>
        private float IdleSmooth = 3f;
        /// <summary>
        /// how much we want to smooth keyboard transition - idle animations
        /// </summary>
        private float keybSmooth = 5f;   
 
		/// <summary>
		/// Selected input method
		/// </summary>
		private enum InputMethod {None, Keyboard, Joystick, Mouse, All};
        /// <summary>
        /// current input method
        /// </summary>
        private InputMethod inputMethod = InputMethod.All; //lock as default

        /// <summary>
        /// dead zone for any action on an axis
        /// </summary>
        public float axisDeadZone = 0.2f;

        /// <summary>
        /// input values for Stick One and Stick Two
        /// </summary>
        Vector3 stickInput;

        /// <summary>
        /// Keyboard axis and buttons
        /// </summary>
        string keyMoveAxis = MocapiThomas.InputSettings.keyMoveAxis; 
        string keyTurnAxis = MocapiThomas.InputSettings.keyTurnAxis;
        private KeyCode keyRunButton = MocapiThomas.InputSettings.keyRunButton;
        private KeyCode keyStrafeButton = MocapiThomas.InputSettings.keyStrafeButton;
        private float keyRunMultiplier = 0f;


        /// <summary>
        /// Joystic axis and buttons
        /// </summary>
        string joyMoveAxis = MocapiThomas.InputSettings.joyMoveAxis;  
        string joyTurnAxis = MocapiThomas.InputSettings.joyTurnAxis; 
        string joyAlertButton = MocapiThomas.InputSettings.joyAlertButton;
        string joySitButton = MocapiThomas.InputSettings.joySitButton;
        string joyLookButton = MocapiThomas.InputSettings.joyLookButton;
        string joyStrafeButton = MocapiThomas.InputSettings.joyStrafeButton;

        /// <summary>
        /// Mouse axis (unused)
        /// </summary>
        //string mouseMoveAxis = Mocapianimation.InputSettings.mouseMoveAxis;
        //string mouseTurnAxis = Mocapianimation.InputSettings.mouseTurnAxis;
        //string mouseStrafeAxis = Mocapianimation.InputSettings.mouseStrafeAxis;  //probably not axis but (mouse) key + mouseTurnAxis

        /// <summary>
        /// Animation controller
        /// </summary>
        private Animator anim;

        /// <summary>
        /// Current animation state
        /// </summary>
        private AnimatorStateInfo animState;

        /// <summary>
        /// Name of the current motion to be shown as a label and to be processed as motion
        /// </summary>       
        public static string MotionLabel;
        public static string MotionName;

        /// <summary>
        //Check for motion change
        private string field;
        public bool Change
        {
            get
            {
                if (field != MotionName)
                {
                    field = MotionName;
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Check if animation in any of the IDLE state
        /// </summary>
        public bool Idle
        {
        	get
        	{
        		animState = anim.GetCurrentAnimatorStateInfo(0);

                if (animState.nameHash == idleBlendTree)
                {
                    return true;
                }
				return false;
        	}
        }

        /// <summary>
        /// speed of the character
        /// </summary>
        public float Move
        {
            get { return move; }
            set
            {
                if (value > axisDeadZone || value < -axisDeadZone)
                    idle = false;

                move = value;

            }
        }
        private float move;

		/// <summary>
		/// direction of the character
		/// </summary>
        public float Direction
        {
            get { return direction; }
            set
            {
                if (value > axisDeadZone || value < -axisDeadZone)
                    idle = false;

                direction = value;

            }
        }
		private float direction;

		/// <summary>
		/// Run flag to indicate if character should be in running mode
		/// </summary>
		public bool Run
		{            
			get{ return run;}
			set
			{
				if(value)
					idle = false;

				run = value;
			}
		}
		private bool run;

        /// <summary>
        /// Strafe flag to indicate if character should be in sidestepping mode
        /// </summary>
        public bool Strafe
        {
            get { return strafe; }
            set
            {
                if (value)
                    idle = false;

                strafe = value;
            }
        }
        private bool strafe;

		/// <summary>
		/// Sit flag to indicate sit animation
		/// </summary>
		public bool SitDown
		{
			get{ return sitDown;}
			set
			{

				if(sitDown)
				{
					idle = false;
                    Move = 0;
                    Direction = 0;

				}
				sitDown = value;
            }
		}
		private bool sitDown;

        /// <summary>
        /// Look flag to indicate LookAround Mode
        /// </summary>
        public bool Look
        {
            get { return look; }
            set
            {
                look = value;
                if (value)
                    idle = false;
            }
        }
        private bool look;

        /// <summary>
        /// Alert flag to indicate Alert mode
        /// </summary>
        public bool Alert
        {
            get { return alert; }
            set
            {
                alert = value;
                if (value)
                    idle = false;
            }
        }
        private bool alert;

        // THOMAS motions
        /// <summary>
        /// XXX flag from Thomas motions
        /// </summary>
        public bool LongMasunaga { get { return _LongMasunaga; } set { _LongMasunaga = value; if (value) idle = false; } } private bool _LongMasunaga;
        public bool MaagStarking { get { return _MaagStarking; } set { _MaagStarking = value; if (value) idle = false; } } private bool _MaagStarking;
        public bool Hart1 { get { return _Hart1; } set { _Hart1 = value; if (value) idle = false; } } private bool _Hart1;
        public bool Blaas { get { return _Blaas; } set { _Blaas = value; if (value) idle = false; } } private bool _Blaas;
        public bool Zwing { get { return _Zwing; } set { _Zwing = value; if (value) idle = false; } } private bool _Zwing;
        public bool Lever { get { return _Lever; } set { _Lever = value; if (value) idle = false; } } private bool _Lever;
        public bool KantStreking { get { return _KantStreking; } set { _KantStreking = value; if (value) idle = false; } } private bool _KantStreking;
        public bool Behandeling1 { get { return _Behandeling1; } set { _Behandeling1 = value; if (value) idle = false; } } private bool _Behandeling1;
        public bool Masunaga2 { get { return _Masunaga2; } set { _Masunaga2 = value; if (value) idle = false; } } private bool _Masunaga2;
        public bool Masunaga3 { get { return _Masunaga3; } set { _Masunaga3 = value; if (value) idle = false; } } private bool _Masunaga3;
        public bool Masunaga4 { get { return _Masunaga4; } set { _Masunaga4 = value; if (value) idle = false; } } private bool _Masunaga4;
        public bool WarmingUp { get { return _WarmingUp; } set { _WarmingUp = value; if (value) idle = false; } } private bool _WarmingUp;
        public bool Long { get { return _Long; } set { _Long = value; if (value) idle = false; } } private bool _Long;
        public bool Maag { get { return _Maag; } set { _Maag = value; if (value) idle = false; } } private bool _Maag;
        public bool Nier { get { return _Nier; } set { _Nier = value; if (value) idle = false; } } private bool _Nier;
        public bool Warmer { get { return _Warmer; } set { _Warmer = value; if (value) idle = false; } } private bool _Warmer;
        public bool Hart2 { get { return _Hart2; } set { _Hart2 = value; if (value) idle = false; } } private bool _Hart2;
        public bool RugSterking { get { return _RugSterking; } set { _RugSterking = value; if (value) idle = false; } } private bool _RugSterking;
        public bool LeverSterking { get { return _LeverSterking; } set { _LeverSterking = value; if (value) idle = false; } } private bool _LeverSterking;
        public bool Behandeling2 { get { return _Behandeling2; } set { _Behandeling2 = value; if (value) idle = false; } } private bool _Behandeling2;
        public bool Behandeling3 { get { return _Behandeling3; } set { _Behandeling3 = value; if (value) idle = false; } } private bool _Behandeling3;
        public bool Stop { get { return _Stop; } set { _Stop = value; if (value) idle = false; } } private bool _Stop;


		/// <summary>
		/// Set default values
		/// </summary>
        void Awake()
        {

            //get animator from current gameObject
            anim = GetComponent<Animator>();

            /// <summary>
            /// Populate the Idle Animation's list. 
            /// Would be nice if thes could be read directly from Animator Controller
            /// </summary>
            idleAnimsList = new Vector2[] { idle1, idle2, idle3, idle4, idle5, idle6 };

            //Load all motions into a dictionary
            dictMotions.Add(0, "Long Masunaga");
            dictMotions.Add(1, "Maag Starking");
            dictMotions.Add(2, "Hart 1");
            dictMotions.Add(3, "Blaas");
            dictMotions.Add(4, "Zwing");
            dictMotions.Add(5, "Lever");
            dictMotions.Add(6, "Kant Streking");
            dictMotions.Add(7, "Behandeling 1");
            dictMotions.Add(8, "Masunaga 2");
            dictMotions.Add(9, "Masunaga 3");
            dictMotions.Add(10, "Masunaga 4");
            dictMotions.Add(11, "Warming Up");
            dictMotions.Add(12, "Long");
            dictMotions.Add(13, "Maag");
            dictMotions.Add(14, "Nier");
            dictMotions.Add(15, "Warmer");
            dictMotions.Add(16, "Hart 2");
            dictMotions.Add(17, "Rug Sterking");
            dictMotions.Add(18, "Lever Sterking");
            dictMotions.Add(19, "Behandeling 2");
            dictMotions.Add(20, "Behandeling 3");
            dictMotions.Add(21, "Stop");

        }

        void Start()
        {

        }
	

		/// <summary>
		/// Get input and send it to the animator, which will translate it to animation
		/// </summary>
		void Update () 
		{


			animState = anim.GetCurrentAnimatorStateInfo(0);
            //MocapiThomas.CameraGUI.Progress = animState.normalizedTime % 1;
            MocapiThomas.CameraGUI.Progress = animState.normalizedTime;


			ProcessInput();

            ProcessDeadZone();

			UpdateAnimator();

            SetLabel();

		}



        /// <summary>
        /// Get the input from keyboard or Joystick
        /// </summary>
        void ProcessInput()
        {
            idle = true;

            //Select inputMethod
            //if (Input.GetKey(KeyCode.F1))
            //{
            //    //inputMethod = InputMethod.Keyboard;     //set to keyboard
            //}
            //else if (Input.GetKey(KeyCode.F2))
            //{
            //    //inputMethod = InputMethod.Mouse;        //set to mouse
            //}
            //else if (Input.GetKey(KeyCode.F3))
            //{
            //    //inputMethod = InputMethod.Joystick;     //set to joystick
            //}
            //else if (Input.GetKey(KeyCode.F4))
            //{
            //    inputMethod = InputMethod.All;     //set to joystick
            //}


            //Process movement based on inputMethod - "All Inputs" locked as default method
            switch (inputMethod)
            {

                case InputMethod.Keyboard:
                    ProcessKeyboard();
                    break;

                case InputMethod.Mouse:
                    ProcessMouse();
                    break;

                case InputMethod.Joystick:
                    ProcessJoystick();
                    break;

                case InputMethod.All:   
                    ProcessAll();
                    break;

            }


            //process basic keys
            //Set constantly in "All inputs" mode. Edit script to enable input switching
            if (Input.GetKey(KeyCode.Escape))
            {
                Debug.Log("Exit!");
                Application.Quit();
            }

            
        }

        /// <summary>
        /// Process all inputs
        /// </summary>
        void ProcessAll()
        {

            if (Run == true)
            {
                keyRunMultiplier = Mathf.Lerp(keyRunMultiplier, 1f, Time.deltaTime * keybSmooth);

            }
            else
            {
                keyRunMultiplier = Mathf.Lerp(keyRunMultiplier, .5f, Time.deltaTime * keybSmooth);
            }


            //Process all axis. Mouse controls not used
            Move = Input.GetAxis(joyMoveAxis) + (Input.GetAxis(keyMoveAxis) * keyRunMultiplier);
            Direction = Input.GetAxis(joyTurnAxis) + Input.GetAxis(keyTurnAxis);

            //process Joystick and Keyboard buttons
            Alert = Input.GetButton(joyAlertButton);
            Strafe = Input.GetKey(joyStrafeButton) || Input.GetKey(keyStrafeButton);
            SitDown = Input.GetButton(joySitButton);
            Look = Input.GetButton(joyLookButton);
            Run = Input.GetKey(keyRunButton);

            if (Change == true)
            {
                //Debug.Log("Switch to " + MotionName);

                _LongMasunaga = false;
                _MaagStarking = false;
                _Hart1 = false;
                _Blaas = false;
                _Zwing = false;
                _Lever = false;
                _KantStreking = false;
                _Behandeling1 = false;
                _Masunaga2 = false;
                _Masunaga3 = false;
                _Masunaga4 = false;
                _WarmingUp = false;
                _Long = false;
                _Maag = false;
                _Nier = false;
                _Warmer = false;
                _Hart2 = false;
                _RugSterking = false;
                _LeverSterking = false;
                _Behandeling2 = false;
                _Behandeling3 = false;
                _Stop = false;

                switch (MotionName)
                {
                    case "Long Masunaga":
                        _LongMasunaga	=true	;
                        break;
                    case "Maag Starking":
                        _MaagStarking = true;
                        break;
                    case "Hart 1":
                        _Hart1 = true;
                        break;
                    case "Blaas":
                        _Blaas = true;
                        break;
                    case "Zwing":
                        _Zwing = true;
                        break;
                    case "Lever":
                        _Lever = true;
                        break;
                    case "Kant Streking":
                        _KantStreking = true;
                        break;
                    case "Behandeling 1":
                        _Behandeling1 = true;
                        break;
                    case "Masunaga 2":
                        _Masunaga2 = true;
                        break;
                    case "Masunaga 3":
                        _Masunaga3 = true;
                        break;
                    case "Masunaga 4":
                        _Masunaga4 = true;
                        break;
                    case "Warming Up":
                        _WarmingUp = true;
                        break;
                    case "Long":
                        _Long = true;
                        break;
                    case "Maag":
                        _Maag = true;
                        break;
                    case "Nier":
                        _Nier = true;
                        break;
                    case "Warmer":
                        _Warmer = true;
                        break;
                    case "Hart 2":
                        _Hart2 = true;
                        break;
                    case "Rug Sterking":
                        _RugSterking = true;
                        break;
                    case "Lever Sterking":
                        _LeverSterking = true;
                        break;
                    case "Behandeling 2":
                        _Behandeling2 = true;
                        break;
                    case "Behandeling 3":
                        _Behandeling3 = true;
                        break;
                    case "Stop":
                        _Stop = true;
                        break;
                }
                       MotionName = null;

            }

            ////Thomas motions - keypress buttons
            //LMasunaga = Input.GetKey(MocapiThomas.InputSettings.keyT_long_masunaga);
            //maagStark = Input.GetKey(MocapiThomas.InputSettings.keyT_maagStark);
            //hart = Input.GetKey(MocapiThomas.InputSettings.keyT_hart);
            //blaas = Input.GetKey(MocapiThomas.InputSettings.keyT_blaas);

            //zwing = Input.GetKey(MocapiThomas.InputSettings.keyT_zwing);
            //lever = Input.GetKey(MocapiThomas.InputSettings.keyT_lever);
            //kant = Input.GetKey(MocapiThomas.InputSettings.keyT_kant);
            //behand = Input.GetKey(MocapiThomas.InputSettings.keyT_behand);

            //process UI buttons
            //Alert = Input.GetButton(joyAlertButton);
            //Strafe = Input.GetKey(joyStrafeButton) || Input.GetKey(keyStrafeButton);
            //SitDown = Input.GetButton(joySitButton);
            //Look = Input.GetButton(joyLookButton);
            //Run = Input.GetKey(keyRunButton);

            //Debug.Log(MocapiThomas.CameraGUI.MotionButton);

            //LMasunaga = ;
            //maagStark = Input.GetKey(MocapiThomas.InputSettings.keyT_maagStark);
            //hart = Input.GetKey(MocapiThomas.InputSettings.keyT_hart);
            //blaas = Input.GetKey(MocapiThomas.InputSettings.keyT_blaas);

            //zwing = Input.GetKey(MocapiThomas.InputSettings.keyT_zwing);
            //lever = Input.GetKey(MocapiThomas.InputSettings.keyT_lever);
            //kant = Input.GetKey(MocapiThomas.InputSettings.keyT_kant);
            //behand = Input.GetKey(MocapiThomas.InputSettings.keyT_behand);

        }

        /// <summary>
        /// Tweak input values acording to a deadzone.
        /// Original code by stfx
        /// </summary>
        void ProcessDeadZone()
        {

            //Here we need 3 axis working together. You may need different configuration.
            stickInput = new Vector2(Direction, Move); 
            float inputMagnitude = stickInput.magnitude;

            if (inputMagnitude < axisDeadZone)
            {
                stickInput = Vector2.zero;
            }
            else
            {
                // rescale the clipped input vector into the non-dead zone space
                stickInput *= (inputMagnitude - axisDeadZone) / ((1f - axisDeadZone) * inputMagnitude);
            }

        }


        /// <summary>
        /// Update the character based on local values.
        /// These values can be modified by
        /// </summary>
        void UpdateAnimator()
        {

            //change idle state if possible
            if (Idle)
            {
                IdleVariants();
            }
            else
            {
                NextIdle = 0;
            }

            //update animator		    
            anim.SetFloat("Move", stickInput.y);
            anim.SetFloat("Direction", stickInput.x);

            anim.SetBool("Idle", idle);
            anim.SetBool("SitDown", sitDown);
            anim.SetBool("Alert", alert);
            anim.SetBool("LookAround", look);
            anim.SetBool("Run", run);
            anim.SetBool("Strafe", strafe);

            //Debug.Log(_LongMasunaga);

            //Thomas group
            anim.SetBool("Long Masunaga", _LongMasunaga);
            anim.SetBool("Maag Starking", _MaagStarking);
            anim.SetBool("Hart 1", _Hart1);
            anim.SetBool("Blaas", _Blaas);
            anim.SetBool("Zwing", _Zwing);
            anim.SetBool("Lever", _Lever);
            anim.SetBool("Kant Streking", _KantStreking);
            anim.SetBool("Behandeling 1", _Behandeling1);
            anim.SetBool("Masunaga 2", _Masunaga2);
            anim.SetBool("Masunaga 3", _Masunaga3);
            anim.SetBool("Masunaga 4", _Masunaga4);
            anim.SetBool("Warming Up", _WarmingUp);
            anim.SetBool("Long", _Long);
            anim.SetBool("Maag", _Maag);
            anim.SetBool("Nier", _Nier);
            anim.SetBool("Warmer", _Warmer);
            anim.SetBool("Hart 2", _Hart2);
            anim.SetBool("Rug Sterking", _RugSterking);
            anim.SetBool("Lever Sterking", _LeverSterking);
            anim.SetBool("Behandeling 2", _Behandeling2);
            anim.SetBool("Behandeling 3", _Behandeling3);
            anim.SetBool("Stop", _Stop);

        }

        void SetLabel()
        {
            animState = anim.GetCurrentAnimatorStateInfo(0);

            if (animState.nameHash == ClipLongMasunaga) { MotionLabel = "Long Masunaga"; }
            else if (animState.nameHash == ClipMaagStarking) { MotionLabel = "Maag Starking"; }
            else if (animState.nameHash == ClipHart1) { MotionLabel = "Hart"; }
            else if (animState.nameHash == ClipBlaas) { MotionLabel = "Blaas"; }
            else if (animState.nameHash == ClipZwing) { MotionLabel = "Zwing"; }
            else if (animState.nameHash == ClipLever) { MotionLabel = "Lever"; }
            else if (animState.nameHash == ClipKantStreking) { MotionLabel = "Kant Streking"; }
            else if (animState.nameHash == ClipBehandeling1) { MotionLabel = "Behandeling"; }
            else { MotionLabel = "_ _ _"; }

        }

        /// <summary>
        /// Process keyboard inputs
        /// Template. Not used.
        /// </summary>
        void ProcessKeyboard()
		{
			//process axis
            Move 		= Input.GetAxis(keyMoveAxis);
            Direction 	= Input.GetAxis(keyTurnAxis);

			//process buttons
			//TODO implement
		}

		/// <summary>
		/// Process joystick inputs
        /// Template. Not used.
		/// </summary>
		void ProcessJoystick()
		{
			//process axis
            Move 		= Input.GetAxis(joyMoveAxis);
            Direction 	= Input.GetAxis(joyTurnAxis);

			//process buttons
			//TODO implement
		}

		/// <summary>
		/// Process mouse inputs
        /// Template. Not used.
        /// </summary>
		void ProcessMouse()
		{
			//process axis
            //Move 		= Input.GetAxis(mouseMoveAxis);
            //Direction = Input.GetAxis(mouseTurnAxis);

			//process buttons
			//TODO implement
		}

		/// <summary>
		/// Change idle if possible
		/// </summary>
        void IdleVariants()
        {
            int animLoopNum = (int)animState.normalizedTime;
            float animPercent = Mathf.Round(((animState.normalizedTime - animLoopNum) * 100f)) / 100f;     //round to DP2
            //Debug.Log(animPercent);
            if ((animPercent > .85f) && (canChangeState == true))
            {
                NextIdle = UnityEngine.Random.Range(0, 6);  //random integer number between min [inclusive] and max [exclusive]
                canChangeState = false;
            }
            else if (animPercent > .0f && animPercent < .5f && (canChangeState == false))
            {
                canChangeState = true;
            }

            //get the vector for the nex Idle Animation
            nextIdleVariant = idleAnimsList[NextIdle];
            //smooth transition in 2D blendTree
            currentIdleVariant = Vector2.Lerp(currentIdleVariant, nextIdleVariant, Time.deltaTime * IdleSmooth); 
            //set the Animator Controller variables controlling 2D blend tree
            anim.SetFloat("IdleRandA", currentIdleVariant.x);
            anim.SetFloat("IdleRandB", currentIdleVariant.y);
        }

	}
}