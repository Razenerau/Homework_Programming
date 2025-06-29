using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

public class SwitchBulletType : MonoBehaviour
{
    public static string CurrentBulletType { get; private set; } = Tags.ROCK;
    private static int bulletIndex = 0;

    void Update()
    {
        SwitchBullet();
    }

    public static void SwitchBullet()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            List<string> bulletNames = BulletTypesData.Instance.GetBulletNames();
            int bulletNamesNum = bulletNames.Count;
            /*if (bulletIndex >= bulletNamesNum - 1)
            {
                bulletIndex = 0;  
            }
            else
            {
                bulletIndex++;
            }*/
            bulletIndex = bulletIndex >= bulletNamesNum - 1 ? 0 : bulletIndex++;
            CurrentBulletType = bulletNames[bulletIndex];
            Debug.Log(bulletNames[bulletIndex]);
        }
    }
}

