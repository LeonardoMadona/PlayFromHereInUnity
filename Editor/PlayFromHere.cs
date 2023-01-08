using UnityEngine;
using UnityEditor;
using System;

[InitializeOnLoad]
public class PlayFromHere : Editor
{
    static Vector2 oldMousePos;
    static Vector2 mousePos;

    static PlayFromHereData data;

    static PlayFromHere()
    {
        SceneView.duringSceneGui += OnSceneGUI;
        EditorApplication.playModeStateChanged += ResetState;

        FetchScriptableObjectData();

        Debug.Log("PlayFromHere Initialization.");
    }

    static void FetchScriptableObjectData()
    {
        data = Resources.Load("PlayFromHereDataSO") as PlayFromHereData;
    }

    static void OnSceneGUI(SceneView sceneview)
    {
        if (Event.current.button == 1)
        {
            mousePos = Event.current.mousePosition;

            if (Event.current.rawType == EventType.MouseDown)
            {
                oldMousePos = mousePos;
            }
            else if (Event.current.rawType == EventType.MouseUp && mousePos == oldMousePos)
            {
                if (data == null)
                {
                    FetchScriptableObjectData();
                }
                if (data.running)
                {
                    Debug.Log("PlayFromHere is already tagged as running. Change the ScriptableObject if that isn't the case. Aborting Request.");
                    return;
                }

                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("Play from here"), false, ActivatePlayFromHere, 1);
                menu.ShowAsContext();

                Event.current.Use();
            }
        }
    }

    static void ActivatePlayFromHere(object obj)
    {
        if (SceneView.lastActiveSceneView.camera == null)
        {
            Debug.Log("Camera is currently null. Aborting request.");
            return;
        }

        Vector2 mousePosCorrected = new Vector2(mousePos.x, SceneView.lastActiveSceneView.camera.pixelHeight - mousePos.y);

        Ray mouseRay = SceneView.lastActiveSceneView.camera.ScreenPointToRay(mousePosCorrected);

        RaycastHit hit;

        if (Physics.Raycast(mouseRay, out hit, 1000.0f))
        {
            SpawnPlayerObjectAt(hit.point);

            EditorApplication.EnterPlaymode();
            data = Resources.Load("PlayFromHereDataSO") as PlayFromHereData;
            data.running = true;
        }
        else
        {
            Debug.Log("No Collision Found");
        }
    }

    static void SpawnPlayerObjectAt(Vector3 pos)
    {
        Instantiate(data.playerObject, pos, Quaternion.identity).name = data.temporaryPlayerName;
    }

    static void ResetState(PlayModeStateChange state)
    {

        if (state == PlayModeStateChange.EnteredEditMode && data.running)
        {
            GameObject tempPlayer = GameObject.Find(data.temporaryPlayerName);

            if (tempPlayer != null)
                DestroyImmediate(tempPlayer);

            data.running = false;
        }
    }
}
