using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Vector3 rotate = new Vector3(5, 5, 0);

    void  Update()
    {
        transform.Rotate(rotate, Space.World);
    }

}
