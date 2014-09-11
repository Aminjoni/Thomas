Mocapi Motion Pack


Demo scene is available in folder Demo\Scenes.
To enable proper Input Control, please copy contents of 
InputManager.zip 
to folder ProjectSettings, overwriting the default InputManager.

Caution:
this will overwrite any custom Input Settings. 
Please, backup the original file first!



For a detailed turorial and more info
please visit 
http://www.mocapianimation.com/MotionPack.html



List of Motion Modules:

Stand Idle Variants
Turn Left / Right
Look Around
Walk Ahead / Turn Left / Turn Right
Walk Back / Turn Left / Turn Right
Run Ahead / Turn Left / Turn Right
Run Back / Turn Left / Turn Right
Sidestep Left / Right
Alert
Sit Down



Quick usage notes:

1. Mecanim integration - Integrate the entire Mocapi Animation Controller into your project. 
Simply export MocapiMotionPack as Unity Package and import it into your project.
Assign Mocapi_Locomotion Controller to your game avatar's Animation Controller slot.

2. Modular Mecanim integration - Merge only certain animation modules.
Copy selected animation modules from Mocapi_Locomotion.controller to your Animation Controller.
Connect them with transitons as needed.

3. Non-Mecanim integration
You can easily integrate any Animation Clip from our FBX files into your code without using Mecanim.