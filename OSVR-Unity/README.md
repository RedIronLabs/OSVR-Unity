OSVR-Unity README
=================

>   *This is an Alpha Release, and probably not entirely idiomatic for Unity.
>   Check back for updates often.*

Documentation
-------------

For links to details, documentation, and support, visit the repository on
github: https://github.com/OSVR/OSVR-Unity\#readme

Known Issues
------------

This list only includes Unity-specific issues that have a substantial impact on
the development experience. For a full list of issues, see the [GitHub issue
tracker](https://github.com/sensics/OSVR-Unity/issues)

-   Unity 4 Free version will not use distortion as this is a Pro-only feature.
    You will get a warning that you can ignore and continue to use OSVR without
    distortion. OSVR plugins will still work with Unity 4 Free.

Basic Principles and Files
--------------------------

On a machine where you're running an OSVR-Unity application, you need to run an
OSVR server, part of the OSVR-Core builds. For convenience, a 32-bit OSVR Server
install is bundled in the OSVR-Unity snapshot archives. [Contact
us](mailto:support@osvr.org) if you need help.

The `OSVR-Unity.unitypackage` package should contain the x86 and x86\_64 binary
plugins, the compiled Managed-OSVR wrapper, the OSVRUnity scripts (in the
`Assets` directory), and a directory of prefabs. Import this package into your
project.

There is also a few sample/demo applications included, ranging from fairly
sparse environments also used for development of the code/prefabs, to a
high-detail demo.

### ClientKit object

You need exactly one instance of `OSVR.Unity.ClientKit` in your project: get one
using the `ClientKit` prefab. You need to set the app ID: use a reversed DNS
name as seen elsewhere (Java, Android, etc). This just uniquely identifies your
application to the OSVR software. If you fail to do this, you'll see an error in
the Unity console.

### Tracking

For trackers (Pose, Position, Orientation), there are prefabs of nodes that
update their transform accordingly. You'll need to set the path you want to use.
Please see the C/C++ documentation for client apps to find valid interface
paths. (Note that the OSVR-Unity package handles normalization of the coordinate
system to the Unity standard: ignore the one seen in the C++ documentation.)

### Manually handling callbacks

This involves two pieces:

-   Adding an `OSVR.Unity.InterfaceGameObject` script component, in which you
    can specify the path. There is a prefab for this.

-   Adding your own script component (which should inherit from
    `OSVR.Unity.InterfaceBase` instead of `MonoBehaviour` for simplest usage)
    that uses the `InterfaceGameObject` to register a callback.

Examples for buttons and analog triggers are included in the `minigame` scene.

Paths for these callbacks that provide useful information can be found in the
main OSVR-Core documentation on the "Writing a client application" page.

### Other interaction

Any other interaction with the OSVR framework should go directly through the
Managed-OSVR (.NET) wrapper without any Unity-specific adaptations. See that
source for examples of button and analog callbacks, as well as display parameter
access (ideally used to set up the display properly). In terms of API, the
Managed-OSVR API is effectively a direct translation of the C++ wrappers of OSVR
`ClientKit`, so please see the main OSVR-Core client documentation for more
information.

### Execution

A standalone player built for Windows may end up needing the `-adapter N`
argument, where `N` is a Direct3D display adapter, to put the rendered output on
the HMD display.

### DLL’s and Plugins

By default, the DLL’s and Plugins required to make this work are omitted from
the repository.  For easy access, you can use the latest officially tested DLL’s
from the following location.  Or build them from the following repositories.

### Assets/OSVRUnity

Contains the actual VR tools needed to interact with an OSVR supported HMD.

### Assets/OSVRUnityServer

Contains sample code showing how to launch the OSVR server from the Editor.  And
contains sample code showing how to launch the OSVR server from inside a game.

OSVRUnity
---------

### Core Component Prefabs

-   ClientKit

    -   Required for all applications, needs a unique reverse domain for the
        application name

-   Recenter

    -   Used to recenter the view based on the transformation of the parent
        object (used to stop you from looking in the wrong direction when the
        application is launched)

-   SampleEyeTracker2DCursor

    -   ??? (Currently broken in repo)

-   VRDisplayTracked

    -   Sample prefab showing how the HMD can be used to look around.  Does not
        contain controls for movement.

-   VRFirstPersonController

    -   Sample prefab showing how the HMD can be used to look around.  Has some
        controls for movement based on the Unity settings for “Horizontal” and
        “Verticle” input

### Interface Prefabs

You can attach these interfaces to game objects, with the intention of accessing
data points from scripts attached to those game objects.  These can be accessed
by using `GetComponent<PositionInterface>()`

-   AnalogInterface

    -   Exposes any analog device information from the HMD

-   ButtonInterface

    -   Exposes any physical buttons on the HMD

-   DirectionInterface

    -   Exposes direction of view

-   EyeTracker2DInterface

    -   Exposes eye tracker 2D view information from supported devices

-   EyeTracker3DInterface

    -   Exposes eye tracker 3D view information from supported devices

-   EyeTrackerBlinkInterface

    -   Exposes eye tracker blinking information from supported devices

-   Location2DInterface

    -   Exposes position in 2D (for UI elements?)

-   NaviPositionInterface

    -   Exposes real position

-   NaviVelocityInterface

    -   Exposes velocity

-   OrientationInterface

    -   Exposes orientation

-   PoseInterface

    -   Exposes position and orientation

-   PositionInterface

    -   Exposes local position

### Deprecated Prefabs

-   InterfaceGameObject

    -   No longer used, use the interfaces above instead

### Important Scripts

-   DisplayController.cs

    -   Creates the stereo rendering of the scene, and the lifecycle of the
        system.  Creates the VRViewer and VReyes children as required.  Handles
        direct mode.  Also attempts to create the OsvrRenderManager.

-   DLLSearchPathFixer

    -   Tries to ensure that the appropriate DLL’s are being referenced

-   GetParent

    -   Simple helper utility for returning parents gameobject

-   OsvrCharacterMotor

    -   Handles movement of the character

    -   Note:  Unlike the Unity Character Motor, jumping is disabled by default

-   OsvrInputController

    -   Handles input for first person, using “horizontal” and “vertical”
        references, to push events to the OsvrCharacterMotor

-   OsvrMouseLook

    -   Used for debugging, lets you use a mouse as if it was HMD movement

-   OsvrRenderManager

    -   Used by DisplayController.cs, optional, but improves performance.

-   VREye

    -   Actual transformation point of where your eye is offset from the
        VRViewer layer.  In the prefabs, only one instance exists,

-   VRSurface

    -   One instance per eye, actual camera view.  In the prefabs, only one
        instance exists, and any components on the VRSurface will be replicated
        to any auto-generated sibling instances.

-   VRViewer

    -   One instance, manages both eyes, and acts as the general view point of
        where the eyes will look.

OSVRUnityServer
---------------

### Core Component Prefabs

-   OSVR-Server-RealWorldCanvas

    -   Shows a VR-friendly UI for configuring and launching the OSVR Server

    -   The VR friendly UI is likely going to be unsable however, as it is
        assumed the OSVR Server is not running

    -   Has examples of how to auto-run the server if the path is configured and
        set

-   OSVR-Server-Auto-RealWorldCanvas

    -   Framework for auto-running the server (coming in an updated DLL)

### Scripts

-   OSVRUnityServer\\Editor\\OSVRWindowPaths.cs

    -   Allows you to set and launch the OSVR server using Window \> OSVR Server

-   OSVRUnityServer\\Server\\OSVRServerPaths.cs

    -   Static helper utility for managing the OSVR Server location, can be used
        to force a server restart on application launch, and to run the server
        with optional arguments

-   OSVRUnityServer\\Server\\OSVRServerGUI.cs

    -   GUI handling framework for manually running the server at game launch

-   OSVRUnityServer\\Server\\OSVRServerAutolaunchGUI.cs

    -   GUI handling framework for auto-running the server (coming in an updated
        DLL)

###  
