using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockBulletsModel : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    private void Start()
    {
        //_gameObject.SetActive(false);
       // _gameObject.transform.position = new Vector3(4, -40, 0);

    }

    public void SpawnUnlockingHand()
    {
        _gameObject.SetActive(true);
        _gameObject.transform.position = new Vector3(4, 0, 0);
    }
}
