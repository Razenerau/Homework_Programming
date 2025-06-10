using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockBullets : MonoBehaviour
{
    [SerializeField] private SwitchBulletType _switchBulletType;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player collided!");

        if (collision.gameObject.tag == "Player")
        {
            _switchBulletType.enabled = true;
            Destroy(gameObject);
        }
    }
}
