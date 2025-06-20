using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHeartModel : MonoBehaviour
{
    [SerializeField] private GameObject heart;

    public void CreateHeart()
    {
        float random = Random.value;
      
        if (random < 0.15f)
        {
            Vector3 pos = gameObject.transform.position;
            Instantiate(heart, pos, Quaternion.identity);
        }
        
    }
}
