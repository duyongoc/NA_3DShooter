using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Platform))]
public class PlatformEditor : Editor
{


    // OnInspector GUI
    public override void OnInspectorGUI() //2
    {
        serializedObject.Update();

        GUILayout.Space(20f);
        GUILayout.Label("PLATFORM: ");
        GUILayout.Label("0-Windows  /  1-Android");

        Platform myTarget = (Platform)target; //1
        EditorGUILayout.PropertyField(serializedObject.FindProperty("platform"));

        string currentPlatform = "";
        if(myTarget.platform == 0)
        {
            currentPlatform = "Windows";
            Platform.WINDOWS = true;
            Platform.ANDROID = false;
        }
        else if(myTarget.platform == 1)
        {
            currentPlatform = "Android";
            Platform.WINDOWS = false;
            Platform.ANDROID = true;
        }

        GUILayout.Space(15f); //2
        GUILayout.Label("Current Platform: " + currentPlatform, EditorStyles.boldLabel); //3

        serializedObject.ApplyModifiedProperties();
    }
}
