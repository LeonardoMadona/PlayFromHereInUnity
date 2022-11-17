using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayFromHereData", menuName = "ScriptableObjects/PlayFromHereData", order = 1)]
public class PlayFromHereData : ScriptableObject
{
    //the prefab we wish to use as the Player (most likely will have to include a camera)
    public GameObject playerObject;
    //the name we wil give the prefab. This is used by the script to delete the prefab when we exit play mode
    public string temporaryPlayerName;
    //check to see if the script is running
    public bool running;
}
