using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlasticGui.LaunchDiffParameters;

public class EnemyTrashModel : MonoBehaviour
{
    public void FreezeAllEnemies()
    {
        Debug.Log("frozen " + transform.childCount + " " + gameObject.name);


        foreach(Transform child in transform)
        {
            Debug.Log(child.name);
            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll; // Stops movement and rotation
            }
        }
    }
}
