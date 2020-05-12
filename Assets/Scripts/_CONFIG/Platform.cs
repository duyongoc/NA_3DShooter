using UnityEditor;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("0-Windows / 1-Anroid")]
    [Range(0,1)]
    public int platform;


    public string platformName = string.Empty;
    public static bool WINDOWS;
    public static bool ANDROID;

    public static Platform s_instane;

    public PlayerInputHandler playerInputHandler;

    private void Awake()
    {
        if(s_instane != null)
            return;
        s_instane = this;
        
    }

    void OnValidate()
    {
        switch(this.platform)
        {
            case 0:
            {
                Platform.WINDOWS = true;
                Platform.ANDROID = false;
                platformName = "WINDOWS";

                PlatformInputFilter input = new CreatePlatformInputFilter();
                playerInputHandler.platformInput = input.GetPlatformInputFilter(platformName);
                break;
            }
            case 1:
            {
                Platform.WINDOWS = false;
                Platform.ANDROID = true;
                platformName = "ANDROID";

                PlatformInputFilter input = new CreatePlatformInputFilter();
                playerInputHandler.platformInput = input.GetPlatformInputFilter(platformName);
                break;
            }
        }
    }

}
