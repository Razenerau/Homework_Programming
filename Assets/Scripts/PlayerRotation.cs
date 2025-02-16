using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Camera _camera;
    private const string _cameraName = "Game_Camera";

    private Vector3 _mousePosition;

    private void Start()
    {
        _camera = GameObject.Find(_cameraName).GetComponent<Camera>();
    }

    private void Update()
    {
        // Gets mouse position relative to where the camera sees it
        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

                   //Where the mouse is \/          Where the player is \/
        Vector3 positionDifference = _mousePosition - transform.position;
        float rotationZ = Mathf.Atan2(positionDifference.x, positionDifference.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ - 90;
    }
}
