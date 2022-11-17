# PlayFromHereInUnity
A tool that mimics the "Play From Here" functionality of Unreal Engine in Unity. It requires a modular Player prefab.

The script will enable a Right-click menu inside the Scene View in unity. 
It will check for a raycast collision with any collider in your scene, and will spawn the Player Prefab if it finds anything.

Player Prefab should be set in the PlayFromHereData_SO Scriptable Object inside the Editor/Resources folder.
