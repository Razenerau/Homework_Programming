using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRotationStatic : MonoBehaviour
{ 

    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity; // Reset rotation each frame
    }
}
