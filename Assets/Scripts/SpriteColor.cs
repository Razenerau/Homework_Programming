using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteColor : MonoBehaviour
{
    [SerializeField]
    private bool _isSolidColor;

    private Image image;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    void Start()
    {
        image = gameObject.GetComponent<Image>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");

        if(_isSolidColor)
        {
            whiteSprite();
        }
        else
        {
            normalSprite();
        }
    }

    void Update()
    {
        
    }
    private void whiteSprite()
    {
        image.material.shader = shaderGUItext;
        image.color = Color.white;
    }

    private void normalSprite()
    {
        image.material.shader = shaderSpritesDefault;
        image.color = Color.white;
    }
}
