using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private RectTransform _rockTransform;
    [SerializeField] private RectTransform _paperTransform;
    [SerializeField] private RectTransform _scissorsTransform;

    [Header("Variables")]
    [SerializeField] private float _amplitude;
    [SerializeField] private float _period;
    [SerializeField] private float _xOffset;

    private float _yInt;
    private float _x = 0;

    private void Start()
    {
        _yInt = _rockTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        _x += Time.deltaTime;

        _rockTransform.position = SineGraph(_x, 0);
        _paperTransform.position = SineGraph(_x, _xOffset);
        _scissorsTransform.position = SineGraph(_x, _xOffset * 2f);
    }

    private Vector2 SineGraph(float x, float xOffset)
    {
        Vector2 pos = _rockTransform.position;
        pos.y = _amplitude * Mathf.Sin(_period * _x * 2 * Mathf.PI - xOffset) + _yInt;
        return pos;
    }
}
