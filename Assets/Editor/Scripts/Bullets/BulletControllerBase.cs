using Codice.ThemeImages;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

public abstract class BulletControllerBase : MonoBehaviour
{
    //==================================================================================================================
    // Variables 
    //================================================================================================================== 

    [SerializeField] protected Transform _parentTransform;

    [Header("Variables")]
    [SerializeField][ReadOnly] protected float _speed;                         //Speed at which the bullet moves  
    [SerializeField][ReadOnly] protected float _deathTime;                     //How long before the bullet dies 
    [SerializeField][ReadOnly] protected Queue<GameObject> _pool;
    [SerializeField] protected int _poolBulletNumber;                          //How many bullets containde inside a bullet pool                            

    [Header("Tags")]
    [SerializeField][ReadOnly] protected string _targetEnemyTag;               //The enemy the bullet would kill
    [SerializeField][ReadOnly] protected string _lethalEnemyTag;               //The enemy that will kill the bullet
    [SerializeField][ReadOnly] protected string _neutralEnemyTag;              //The enemy with the same type as the bullet

    [Header("Data")]
    [SerializeField] protected BulletModel _bulletModel;

    protected bool isActive = false;

    //==================================================================================================================
    // Bullet Set Up  
    //==================================================================================================================

    /*private void Update() 
    {
        if (!isActive) return;
        int layer = (int)gameObject.transform.position.y;
        Debug.Log(layer);
    }*/

    private void Awake()
    {
        InitializeBullet();
    }

    private void InitializeBullet()
    {
        //Unity object setup
        gameObject.name = _bulletModel.Name;
        SetTransform(BulletPool.Instance.GetPoolTranform(gameObject));

        //Enemies setup
        SetTargetEnemy(_bulletModel.TargetEnemyTag);
        SetLethalEnemy(_bulletModel.LethalEnemyTag);
        SetNeutralEnemy(_bulletModel.NeutralEnemyTag);

        //Variables setup
        SetDeathTime(_bulletModel.DeathTime);
        SetSpeed(_bulletModel.Speed);
        SetPoolBulletNumber(_bulletModel.PoolBulletNumber);
        SetPoolBulletNumber(_bulletModel.PoolBulletNumber);
        Debug.Log($"{name} was constructed");
    }

    //Waits till timer is out then destroys the bullet 
    private IEnumerator Expiration()
    {
        float timeElapsed = 0;
        while (timeElapsed < _deathTime) 
        {
            // Set sprites' layer according to the y pos
            //At the bottom of the screen the layer is the highest
            //At the top it's 10, 11, 12 respictively
            //Find the height of the screen in unity coordinates

            float yPos = gameObject.transform.position.y;
            int baseLayer = 10;

            Camera cam = Camera.main;
            float screenHeight = cam.orthographicSize;

            // f(x) returns a layer #, x is object's height
            // at x = sceenHeigh, y = 10
            // at x = 0, y = screenHeight + 10
            // at x = screen/2; y = screen/2 + 10
            // y = mx + b; b = 10;
            // 0 = m(screen) 
            // (screen) = m(0) 

            int layer = (int)(screenHeight + yPos) + 10;
            Debug.Log("Layer: " + layer);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        //yield return new WaitForSeconds(_deathTime);
        Debug.Log("BUlletExpired");
        
        ReturnBulletToPool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if(tag == _lethalEnemyTag)                  //Collision with object that kills the bullet
        {
            Debug.Log("collitsion with lethal enemy");
            ReturnBulletToPool();
        }
        else if (tag == _targetEnemyTag)            //Collision with object that the bullet defeats
        {
            Debug.Log("collitsion with target enemy"); // find a way to make bullet presist after hitting enemy
            ReturnBulletToPool();
        }
        else if (tag == _neutralEnemyTag)           //Collision with the same object as the bullet
        {
            RepellSelf(collision.gameObject);
            Debug.Log("collitsion with neutral enemy");
        }
        else if (tag.Contains("Player"))
        {
            return;
        }
        else                                                    //Collision with bounds or other objects
        {
            ReturnBulletToPool();
        }
    }

    public void Shoot(Vector2 pos, Vector2 velocity, Quaternion rotation)
    {
        SetPosition(pos);
        SetVelocity(velocity);
        SetRotation(rotation);
        StartCoroutine(Expiration());
    }

    protected void RepellSelf(GameObject collisionObj)
    {
        Rigidbody2D _rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        Vector2 normal = collisionObj.GetComponent<Rigidbody2D>().linearVelocity.normalized;
        Vector2 incidentDirection = _rigidbody2D.linearVelocity.normalized;
        Vector2 reflectedDirection = Vector2.Reflect(incidentDirection, normal);
        _rigidbody2D.linearVelocity = reflectedDirection * _rigidbody2D.linearVelocity.magnitude;
    }

    private void ReturnBulletToPool()
    {
        BulletPool.Instance.AddBulletToPool(gameObject);
    }

    //--------------------------------------------------------------------------------
    //                  GETTER AND SETTER
    //--------------------------------------------------------------------------------

    public BulletModel GetBulletModel() { return _bulletModel; }
    public int GetPoolBulletNumber() { return  _poolBulletNumber; }
    protected void SetPoolBulletNumber(int num) {  _poolBulletNumber = num; }
    public Transform GetParentTransform() { return _parentTransform; }
    protected void SetParentTransform(Transform gameObject) { _parentTransform = gameObject; }
    protected void SetPosition(Vector2 pos) { gameObject.transform.position = pos; }
    protected void SetRotation(Quaternion rotation) { gameObject.transform.rotation = rotation; }

    protected void SetVelocity(Vector2 velocity)
    {
        Rigidbody2D _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _rigidbody2D.linearVelocity = velocity;
    }
    protected void SetSpeed(float num) { _speed = num; }
    public float GetSpeed() { return _speed; }
    protected void SetTransform(Transform transform) { this.gameObject.transform.SetParent(transform); }
    protected void SetDeathTime(float num) { _deathTime = num; }
    protected void SetLethalEnemy(string tag) { _lethalEnemyTag = tag; }
    protected void SetTargetEnemy(string tag) { _targetEnemyTag = tag; }
    protected void SetNeutralEnemy(string tag) { _neutralEnemyTag = tag; }
}
