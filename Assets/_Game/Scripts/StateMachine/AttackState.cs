using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private float timer;
    private float randomTime;
    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        randomTime = Random.Range(3f, 5f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if(timer < randomTime && enemy.IsHaveTargetInRange())
        {
            enemy.Attack();
        }
        else
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
