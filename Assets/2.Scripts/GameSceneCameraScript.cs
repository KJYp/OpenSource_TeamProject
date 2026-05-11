using UnityEngine;
using UnityEngine.InputSystem;

public class GameSceneCameraScript : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float mouseEdgeSize = 20f;

    public Transform background;

    private float minX;
    private float maxX;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        float backWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        float camWidth = cam.orthographicSize * 2f * cam.aspect;

        minX = -(backWidth / 2f) + (camWidth / 2f);
        maxX = (backWidth / 2f) - (camWidth / 2f);
    }

    void Update()
    {
        float moveX = 0f;

        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;

        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
        {
            moveX = -1f;
        }

        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
        {
            moveX = 1f;
        }

        if (mouse.position.ReadValue().x <= mouseEdgeSize)
        {
            moveX = -1f;
        }

        if (mouse.position.ReadValue().x >= Screen.width - mouseEdgeSize)
        {
            moveX = 1f;
        }

        Vector3 pos = transform.position;
        pos.x += moveX * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        transform.position = pos;
    }
}
