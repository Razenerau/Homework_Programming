using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [Header("Line Art")]
    [SerializeField] private SpriteRenderer _lineArt;
    [SerializeField] private Sprite _rockLineArt;
    [SerializeField] private Sprite _paperLineArt;
    [SerializeField] private Sprite _scissorsLineArt;

    [Header("Body")]
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private Sprite _rockBody;
    [SerializeField] private Sprite _paperBody;
    [SerializeField] private Sprite _scissorsBody;

    [Header("Shadows")]
    [SerializeField] private SpriteRenderer _shadows;
    [SerializeField] private Sprite _rockShadows;
    [SerializeField] private Sprite _paperShadows;
    [SerializeField] private Sprite _scissorsShadows;

    public static PlayerView Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of PlayerView detected. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    public void SetRock()
    {
        _lineArt.sprite = _rockLineArt;
        _body.sprite = _rockBody;
        _shadows.sprite = _rockShadows;
    }

    public void SetPaper()
    {
        _lineArt.sprite = _paperLineArt;
        _body.sprite = _paperBody;
        _shadows.sprite = _paperShadows;
    }

    public void SetScissors()
    {
        _lineArt.sprite = _scissorsLineArt;
        _body.sprite = _scissorsBody;
        _shadows.sprite = _scissorsShadows;
    }
}


