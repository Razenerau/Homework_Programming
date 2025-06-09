using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPositionStatic : MonoBehaviour
{
    private void LateUpdate()
    {
        gameObject.transform.position = Vector3.zero;
    }
}
