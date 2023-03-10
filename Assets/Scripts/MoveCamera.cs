using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 targetPos;
    private Vector3 targetRot;
    private const float speed = 0.75f;
    private float step;

    private static readonly Vector3 posDown = new(0.0f, 1.25f, -0.5f);
    private static readonly Vector3 rotDown = new(30.0f, 0.0f, 0.0f);

    private static readonly Vector3 posUp = new(0.0f, 1.4f, -0.3f);
    private static readonly Vector3 rotUp = new(40.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        KeyboardInput();
    }

    void Move()
    {
        if (isMoving)
        {
            step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            transform.localEulerAngles = Vector3.MoveTowards(transform.localEulerAngles, targetRot, step * 50);
            if (transform.position == targetPos && transform.localEulerAngles == targetRot)
            {
                isMoving = false;
            }
        }
    }

    public void MoveUp()
    {
        targetPos = posUp;
        targetRot = rotUp;
        isMoving = true;
    }

    public void MoveDown()
    {
        targetPos = posDown;
        targetRot = rotDown;
        isMoving = true;
    }

    void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
    }
}
