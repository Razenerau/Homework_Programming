using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject _preFab;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(LoadPrefabAfterDelay(1f));
    }

    private System.Collections.IEnumerator LoadPrefabAfterDelay(float delay)
    {
        StartCounterView.SetVisible(false);

        yield return new WaitForSeconds(delay);
        StartCounterView.SetVisible(true);
        StartCounterView.SetText("3");

        yield return new WaitForSeconds(delay);
        StartCounterView.SetText("2");

        yield return new WaitForSeconds(delay);
        StartCounterView.SetText("1");

        yield return new WaitForSeconds(delay);

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(delay / 5);
            StartCounterView.SetText("GO GO GO");
            StartCounterView.SetVisible(true);

            yield return new WaitForSeconds(delay / 5);
            StartCounterView.SetVisible(false);
        }

        Instantiate(_preFab, Vector3.zero, Quaternion.identity);
    }

}
