# PlayFromHereInUnity
A tool that mimics the "Play From Here" functionality of Unreal Engine in Unity. It requires a modular Player prefab.

The script will enable a Right-click menu inside the Scene View in unity and will test a raycast against any collider in your scene. If it finds anything, the player prefab will be instantiated and the editor will enter Play Mode.

The player prefab should be set in the PlayFromHereData_SO Scriptable Object inside the Editor/Resources folder.
