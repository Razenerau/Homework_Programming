using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsView : MonoBehaviour
{
    [SerializeField] private List<GameObject> boundsList;

    public void SetBoundsActive(bool active)
    {
        foreach (var bounds in boundsList)
        {
            bounds.SetActive(active);
        }
    }
}
