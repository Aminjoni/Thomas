using UnityEngine;
using System.Collections;


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
        /// Name of the current motion to be shown as a label
        /// </summary>       
        public static string MotionLabel;

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
        static int ClipLMasunaga = Animator.StringToHash("Base Layer.long_masunaga");
        static int ClipLmaagStark = Animator.StringToHash("Base Layer.maag_starking");
        static int ClipHart = Animator.StringToHash("Base Layer.hart");
        static int ClipBlaas = Animator.StringToHash("Base Layer.blaas");

        static int ClipZwing = Animator.StringToHash("Base Layer.zwing");
        static int ClipLever = Animator.StringToHash("Base Layer.lever");
        static int ClipKant_streking = Animator.StringToHash("Base Layer.kant_streking");
        static int ClipBehandeling = Animator.StringToHash("Base Layer.behandeling");


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
        /// Long Masunaga flag from Thomas motions
        /// </summary>
        public bool LMasunaga
        {
            get { return _LMasunaga; }
            set
            {
                _LMasunaga = value;
                if (value)
                    idle = false;
            }
        }
        private bool _LMasunaga;

        /// <summary>
        /// maag Starking flag from Thomas motions
        /// </summary>
        public bool maagStark 
        {
            get { return _maagStark; }
            set
            {
                _maagStark = value;
                if (value)
                    idle = false;
            }
        }
        private bool _maagStark;

        /// <summary>
        /// hart flag from Thomas motions
        /// </summary>
        public bool hart
        {
            get { return _hart; }
            set
            {
                _hart = value;
                if (value)
                    idle = false;
            }
        }
        private bool _hart;

        /// <summary>
        /// blaas flag from Thomas motions
        /// </summary>
        public bool blaas
        {
            get { return _blaas; }
            set
            {
                _blaas = value;
                if (value)
                    idle = false;
            }
        }
        private bool _blaas;

        /// <summary>
        /// zwing flag from Thomas motions
        /// </summary>
        public bool zwing
        {
            get { return _zwing; }
            set
            {
                _zwing = value;
                if (value)
                    idle = false;
            }
        }
        private bool _zwing;

        /// <summary>
        /// kant flag from Thomas motions
        /// </summary>
        public bool kant
        {
            get { return _kant; }
            set
            {
                _kant = value;
                if (value)
                    idle = false;
            }
        }
        private bool _kant;

        /// <summary>
        /// lever flag from Thomas motions
        /// </summary>
        public bool lever
        {
            get { return _lever; }
            set
            {
                _lever = value;
                if (value)
                    idle = false;
            }
        }
        private bool _lever;

        /// <summary>
        /// behand flag from Thomas motions
        /// </summary>
        public bool behand
        {
            get { return _behand; }
            set
            {
                _behand = value;
                if (value)
                    idle = false;
            }
        }
        private bool _behand;

		/// <summary>
		/// Set default values
		/// </summary>
		void Awake () 
		{
			//get animator from current gameObject
			anim = GetComponent<Animator>();

		}

        void Start()
        {

            /// <summary>
            /// Populate the Idle Animation's list. 
            /// Would be nice if thes could be read directly from Animator Controller
            /// </summary>
            idleAnimsList = new Vector2[] { idle1, idle2, idle3, idle4, idle5, idle6 }; 

        }
	

		/// <summary>
		/// Get input and send it to the animator, which will translate it to animation
		/// </summary>
		void Update () 
		{

			//animState = anim.GetCurrentAnimatorStateInfo(0);

			ProcessInput();

            ProcessDeadZone();

			UpdateAnimator();

            SetLabel();

		}


        
        /// <summary>
		/// Update the character based on local values.
		/// These values can be modified by
		/// </summary>
		void UpdateAnimator()
		{

			//change idle state if possible
			if(Idle)
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

                //Thomas group
            anim.SetBool("LMasunaga", _LMasunaga);
            anim.SetBool("maagStark", _maagStark);
            anim.SetBool("hart", _hart);
            anim.SetBool("blaas", _blaas);

            anim.SetBool("zwing", _zwing);
            anim.SetBool("lever", _lever);
            anim.SetBool("kant", _kant);
            anim.SetBool("behand", _behand);

			//Debug.Log("Move:"+move);
			//Debug.Log("Dir:"+direction);
			//Debug.Log("Strafe:"+strafe);

		}

		/// <summary>
		/// Get the input from keyboard or Joystick
		/// </summary>
		void ProcessInput()
		{
			idle = true;

            //Process movement based on inputMethod
            switch(inputMethod)
            {
            	case InputMethod.None:
            		//we don't handle input in this case
            		//leave this for control from outside.
            		return;            		

            	case InputMethod.Keyboard:
            		ProcessKeyboard();
            		break;

            	case InputMethod.Mouse:
            		ProcessMouse();
            		break;

            	case InputMethod.Joystick:
            		ProcessJoystick();
            		break;

                case InputMethod.All:   //Locked as default method
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

            if( Input.GetKey(KeyCode.F1))
            {
                //inputMethod = InputMethod.Keyboard;     //set to keyboard
            }
            else if( Input.GetKey(KeyCode.F2))
            {
                //inputMethod = InputMethod.Mouse;        //set to mouse
            }
            else if( Input.GetKey(KeyCode.F3))
            {
                //inputMethod = InputMethod.Joystick;     //set to joystick
            }
            else if (Input.GetKey(KeyCode.F4))
            {
                inputMethod = InputMethod.All;     //set to joystick
            }

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
		/// Process all inputs
		/// </summary>
        void ProcessAll()
        {

            if (Run == true)
            {
                keyRunMultiplier = Mathf.Lerp(keyRunMultiplier, 1f, Time.deltaTime * keybSmooth );

            }
            else
            {
                keyRunMultiplier = Mathf.Lerp(keyRunMultiplier, .5f, Time.deltaTime * keybSmooth );
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

            LMasunaga = Input.GetKey(MocapiThomas.InputSettings.keyT_long_masunaga);
            maagStark = Input.GetKey(MocapiThomas.InputSettings.keyT_maagStark);
            hart = Input.GetKey(MocapiThomas.InputSettings.keyT_hart);
            blaas = Input.GetKey(MocapiThomas.InputSettings.keyT_blaas);

            zwing = Input.GetKey(MocapiThomas.InputSettings.keyT_zwing);
            lever = Input.GetKey(MocapiThomas.InputSettings.keyT_lever);
            kant = Input.GetKey(MocapiThomas.InputSettings.keyT_kant);
            behand = Input.GetKey(MocapiThomas.InputSettings.keyT_behand);

        }

        void SetLabel()
        {
            animState = anim.GetCurrentAnimatorStateInfo(0);

            if (animState.nameHash == ClipLMasunaga) { MotionLabel = "Long Masunaga"; }
            else if (animState.nameHash == ClipLmaagStark) { MotionLabel = "Maag Starking"; }
            else if (animState.nameHash == ClipHart) { MotionLabel = "Hart"; }
            else if (animState.nameHash == ClipBlaas) { MotionLabel = "Blaas"; }
            else if (animState.nameHash == ClipZwing) { MotionLabel = "Zwing"; }
            else if (animState.nameHash == ClipLever) { MotionLabel = "Lever"; }
            else if (animState.nameHash == ClipKant_streking) { MotionLabel = "Kant Streking"; }
            else if (animState.nameHash == ClipBehandeling) { MotionLabel = "Behandeling"; }
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