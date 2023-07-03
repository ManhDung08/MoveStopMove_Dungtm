using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask obstacleLayer;
    

    private float horizontal;
    private float vertical;
    private Vector3 direction;

    private void Update()
    {
        if (isDead)
        {
            return;
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Move();
            }
            if (Input.GetMouseButtonUp(0))
            {
                Stop();
            }
        }
    }

    private void Move()
    {
        ChangeAnim(AnimationName.run);
        horizontal = variableJoystick.Horizontal;  
        vertical = variableJoystick.Vertical;
        direction = new Vector3(horizontal, 0f, vertical);
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720f * Time.fixedDeltaTime);

        //check obstacle
        if (Physics.Raycast(transform.position + Vector3.up, direction, out RaycastHit hit, 0.75f, obstacleLayer))
        {
            return;
        }
        transform.position += speed * Time.deltaTime * direction;
    }

    private void Stop()
    {
        ChangeAnim(AnimationName.idle);

        //Tìm xem có enemy inRange hay không có thì attack
        GameObject enemy = attackRange.GetComponent<AttackRange>().FindEnemy();
        if (enemy != null)
        {
            Attack(enemy);
        }
    }

    protected override void OnInit()
    {
        weaponPrefab = Instantiate(weaponData.GetWeappon(WeaponType.hammer), weaponTransform);
        base.OnInit();
    }
    
}
