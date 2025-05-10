using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject _preFab;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(LoadPrefabAfterDelay(3.5f));
    }

    private System.Collections.IEnumerator LoadPrefabAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("5 seconds passed");
        Instantiate(_preFab, Vector3.zero, Quaternion.identity);
    }

}
