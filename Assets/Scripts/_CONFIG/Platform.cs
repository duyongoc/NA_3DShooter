using UnityEditor;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Range(0,1)]
    public int platform;

    public static bool WINDOWS;
    public static bool ANDROID;

    private void Awake()
    {
        if(this.platform == 0)
        {
            Platform.WINDOWS = true;
            Platform.ANDROID = false;
        }
        else if(platform == 1)
        {
            Platform.WINDOWS = false;
            Platform.ANDROID = true;
        }
    }

    // void OnValidate()
    // {
    //     tempWindows = M_WINDOWS;
    //     tempAndroid = M_ANDROID;

    //     WINDOWS = M_WINDOWS;
    //     ANDROID = M_ANDROID;        
    // }

}
