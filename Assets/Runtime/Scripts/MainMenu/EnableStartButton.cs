using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnableStartButton : StateMachineBehaviour
{
    
     //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject gameObject = GameObject.FindWithTag("Screen");
        Image img = gameObject.GetComponent<Image>();
        img.enabled = true;

        TextMeshProUGUI text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.enabled = true;
    }
}
