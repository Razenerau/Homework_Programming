using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CooldownModel : MonoBehaviour
{
    //public bool isCooldown;
    public enum States
    {
        ROCK,
        PAPER,
        SCISSORS
    }

    public static States CurrentState { get; private set; }

    public static CooldownModel Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        CurrentState = States.ROCK;
    }

    public static void SetState(States state)
    {
        CurrentState = state;
        SpriteColor spriteColor = Instance.GetComponent<SpriteColor>();
        spriteColor.SetSprite(CurrentState);
    }

    public static void SetStateColor(bool isCooldown)
    {
        SpriteColor spriteColor = Instance.GetComponent<SpriteColor>();
        spriteColor.SetSprite(isCooldown);
    }

    public static void Cooldown()
    {
        switch (CurrentState)
        {
            case States.ROCK:
                SpriteColor.pulse();
                break;
            case States.PAPER:
                if (PlayerShoot.Instance.CanShootPaperBullets) SpriteColor.pulse();
                break;
            case States.SCISSORS:
                if (PlayerShoot.Instance.CanShootScissorsBullets) SpriteColor.pulse();
                break;
        }
    }

}
