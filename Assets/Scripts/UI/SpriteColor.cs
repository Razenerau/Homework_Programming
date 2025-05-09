using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.U2D;



#if UNITY_EDITOR
using UnityEditor;
#endif


public class SpriteColor : MonoBehaviour
{
    [SerializeField]
    private bool _isSolidColor;
    [SerializeField]
    private Color _color;
   
    [SerializeField]
    public bool CanPulse = false;
    private float _pulseTime = 0.1f;
    private float _currentPulseTime = 0;

    [SerializeField]
    private Color _pulseColor;
    private bool _isPulsating = false;

    private Image image;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    [Header("Sprites")]
    [SerializeField]
    private Sprite _rockSprite;
    [SerializeField]
    private Sprite _paperSprite;
    [SerializeField]
    private Sprite _scissorsSprite;

    float time = 0f;

    public static SpriteColor Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        Instance.image = gameObject.GetComponent<Image>();
        Instance.shaderGUItext = Shader.Find("GUI/Text Shader");
        Instance.shaderSpritesDefault = Shader.Find("Sprites/Default");

        if(_isSolidColor)
        {
            whiteSprite();
        }
        else
        {
            normalSprite();
        }

        colorSprite(_color);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0)) _isPulsating=true;
        if(_isPulsating)
        {
            pulse();
        } 
    }
    private static void whiteSprite()
    {
        Instance.image.material.shader = Instance.shaderGUItext;
        Instance.image.color = Color.white;
    }

    private static void colorSprite(Color color)
    {
        Instance.image.color = color;
    }

    private static void normalSprite()
    {
        Instance.image.material.shader = Instance.shaderSpritesDefault;
        Instance.image.color = Color.white;
    }

    private static void pulse()
    {
        Instance.time += Time.deltaTime;
        Instance._currentPulseTime = incrementOnce(Instance.time, Instance._pulseTime);

        Instance._pulseColor.a = Mathf.Clamp(Instance._pulseColor.a, 1f, 1f);
        Instance._color.a = Mathf.Clamp(Instance._color.a, 1f, 1f);

        Color _lerpedColor = Color.Lerp(Instance._pulseColor, Instance._color, Instance._currentPulseTime);
        colorSprite(_lerpedColor);

        if(Instance._currentPulseTime >= Instance._pulseTime)
        {
            Instance.time = 0;
            colorSprite(Instance._color);
            Instance._isPulsating = false;
        }
    }

    private static float incrementOnce(float t, float length)
    {
        return Mathf.Clamp(t, 0, length);
    }

    public void SetSprite(CooldownModel.States state)
    {
        switch (state)
        {
            case CooldownModel.States.ROCK:
                Instance.image.sprite = _rockSprite;
                break;
            case CooldownModel.States.PAPER:
                Instance.image.sprite = _paperSprite;
                break;
            case CooldownModel.States.SCISSORS:
                Instance.image.sprite = _scissorsSprite;
                break;
        }
    }
}


