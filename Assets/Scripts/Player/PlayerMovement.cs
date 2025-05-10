using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private float _xSpeed;
    private float _ySpeed;
    public float speed;

    private const string xInput = "Horizontal";
    private const string yInput = "Vertical";

    private void Start()
    {
        gameObject.SetActive(true);
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _xSpeed = Input.GetAxis(xInput);
        _ySpeed = Input.GetAxis(yInput);

        _rigidbody.velocity = new Vector2(_xSpeed, _ySpeed) * speed;
    }
}
