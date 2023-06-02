using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private float speed;


    private float horizontal;
    private float vertical;
    private Vector3 direction;


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Move();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Stop();
        }
    }

    private void Move()
    {
        horizontal = variableJoystick.Horizontal;
        vertical = variableJoystick.Vertical;
        direction = new Vector3(horizontal, 0f, vertical);
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720f * Time.deltaTime);
        transform.position += direction * speed * Time.deltaTime;
    }

    private void Stop()
    {
        
    }
}
