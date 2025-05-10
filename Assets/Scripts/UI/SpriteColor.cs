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
    private Color _cooldownColor;
   
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

    public static void pulse()
    {
        Instance.StartCoroutine(Instance.PulseCoroutine());
    }

    private IEnumerator PulseCoroutine()
    {
        if (image != null)
        {
            // Set to white
            image.color = Color.white;

            // Wait for 5 seconds
            yield return new WaitForSeconds(0.1f);

            // Set to original
            image.color = _color;
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

    public void SetSprite(bool isCooldown)
    {
        if (isCooldown)
        {
            if (_cooldownColor == null) Debug.Log("no color");
            colorSprite(_cooldownColor);
        }
        else
        {
            colorSprite(_color);
        }
    }
}


