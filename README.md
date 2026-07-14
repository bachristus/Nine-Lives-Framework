This is a dedicated repository for Unity games framework package.
It contains plain C# and Unity-dependant code shared between actual games.
The Unity Package folder is Unity package containing all the C# code (*.cs files)
The PlainDotNet folder contains the solution and projects files that take .cs files from Unity Package folder to compile them independently of Unity and run test without running Unity instance in CI pipeline
It is done so in order to be able to connect the Unity Package folder as package to Unity playground (sandbox) project and change .cs files directly in Unity Package folder to simplify the git workflow.