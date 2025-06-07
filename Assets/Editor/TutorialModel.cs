using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialModel : MonoBehaviour
{
    [SerializeField] private TutorialVIew _tutorialView;
    [SerializeField] private AWSDView _AWSDView;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TutorialStart());
        _tutorialView.SetVisible(false);
        _AWSDView.SetVisible(false);
    }

    private IEnumerator TutorialStart()
    {
        yield return new WaitForSecondsRealtime(2f);
        AWSD();
    }

    private void AWSD()
    {
        _tutorialView.SetSize(70f);
        _tutorialView.SetText("Use AWSD to move");
        _tutorialView.SetVisible(true);
        _AWSDView.SetVisible(true);
    }
}
