using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private bool _canPulse = false;
    private float _pulseTime = 0.1f;
    private float _currentPulseTime = 0;

    [SerializeField]
    private Color _pulseColor;
    private bool _isPulsating = false;

    private Image image;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    float time = 0f;

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
    private void whiteSprite()
    {
        image.material.shader = shaderGUItext;
        image.color = Color.white;
    }

    private void colorSprite(Color color)
    {
        image.color = color;
    }

    private void normalSprite()
    {
        image.material.shader = shaderSpritesDefault;
        image.color = Color.white;
    }

    private void pulse()
    {
        time += Time.deltaTime;
        _currentPulseTime = incrementOnce(time, _pulseTime);

        _pulseColor.a = Mathf.Clamp(_pulseColor.a, 1f, 1f);
        _color.a = Mathf.Clamp(_color.a, 1f, 1f);

        Color _lerpedColor = Color.Lerp(_pulseColor, _color, _currentPulseTime);
        colorSprite(_lerpedColor);

        if(_currentPulseTime >= _pulseTime)
        {
            time = 0;
            colorSprite(_color);
            _isPulsating = false;
        }
    }

    private float incrementOnce(float t, float length)
    {
        return Mathf.Clamp(t, 0, length);
    }

    
    /*
    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(SpriteColor))]
    public class SpriteColorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SpriteColor spriteColor = (SpriteColor)target;

            spriteColor._canPulse = EditorGUILayout.Foldout(spriteColor._canPulse, "Pulse", true);

            if(spriteColor._canPulse )
            {
                EditorGUI.indentLevel++;
                DrawPulseDetails(spriteColor);
                EditorGUI.indentLevel--;
            }
        }

        static void DrawPulseDetails(SpriteColor spriteColor)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Delails");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Pulse Color", GUILayout.MaxWidth(100));
            spriteColor._pulseColor = EditorGUILayout.ColorField(spriteColor._pulseColor);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Fading Time", GUILayout.MaxWidth(100));
            spriteColor._pulseTime = EditorGUILayout.FloatField(spriteColor._pulseTime);
            EditorGUILayout.EndHorizontal();
        }
    }
#endif
    #endregion
    */

}


