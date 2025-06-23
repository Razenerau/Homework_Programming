using UnityEngine;

public class BulletView : MonoBehaviour
{
    [Header("Sprite Renderers")]
    [SerializeField] private SpriteRenderer _lineArt;
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private SpriteRenderer _shadows;

    [Header("Bullet Data")]
    [SerializeField] private BulletModel _bulletModel;
 
    private void Awake()
    {
        InitializeSprites();
    }

    private void InitializeSprites() 
    {
        _lineArt.sprite = _bulletModel.LineArt;
        _body.sprite = _bulletModel.Body;
        _shadows.sprite = _bulletModel.Shadows;
    }
}
