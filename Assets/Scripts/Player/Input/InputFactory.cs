using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlatformInput 
{
    void GetPlayerMove(Rigidbody m_rigidbody, Transform transform, VJHandle m_movementVJ);

    void GetPlayerShoot();
}

public abstract class PlatformInputFilter
{
    public abstract IPlatformInput GetPlatformInputFilter(string inputType);
}

public class CreatePlatformInputFilter : PlatformInputFilter
{
    public override IPlatformInput GetPlatformInputFilter(string inputType)
    {
        switch(inputType)
        {
            case "WINDOWS":
            {
                return new PCInputFilter();
            }
            case "ANDROID":
            {
                return new AndroidInputFilter();
            }
            default:
                return null;
        } 
    }
}
