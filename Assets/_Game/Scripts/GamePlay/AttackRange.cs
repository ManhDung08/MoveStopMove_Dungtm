using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private LayerMask characterLayer;
    [SerializeField] private float attackRadius;
    [SerializeField] GameObject circleAroundEnemy;


    public float GetattackRadius()
    {
        return attackRadius;
    }

    //Attack of Enemy
    public GameObject FindNearestEnemy()   
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius, characterLayer);
        if(colliders != null)
        {
            GameObject nearestEnemy = null;
            float closestDistance = 100f;
            foreach (Collider collider in colliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance && distance != 0)
                {
                    closestDistance = distance;
                    nearestEnemy = collider.gameObject;
                }
            }
            return nearestEnemy;
        }
        else
        {
            return null;
        }
    }


    //Attack of Player

    public GameObject currentCircle;
    public List<GameObject> enemy;


    private void Update()
    {
        if(enemy == null || enemy.Count == 0)
        {
            return;
        }
        if (currentCircle == null)
        {
            currentCircle = Instantiate(circleAroundEnemy, enemy[0].transform.position, Quaternion.identity);
            currentCircle.transform.parent = enemy[0].transform;
        }
        for (int i = 0; i < enemy.Count; i++)
        {
            if (enemy[i].GetComponent<Character>().IsDead)
            {
                if (i == 0)
                {
                    Destroy(currentCircle);
                    currentCircle = null;
                }
                enemy.RemoveAt(i);
            }
        }
    }

    public GameObject FindEnemy()
    {
        if (enemy == null || enemy.Count == 0)
        {
            return null;
        }
        return enemy[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("characterLayer"))
        {
            enemy.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("characterLayer"))
        {
            if (enemy.Count > 0 && enemy[0] == other.gameObject)
            {
                Destroy(currentCircle);
                currentCircle = null;
            }
            enemy.Remove(other.gameObject);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRadius);
    //}
}
