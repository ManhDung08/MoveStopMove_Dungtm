using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private LayerMask characterLayer;
    [SerializeField] private float radius;

    public GameObject FindNearestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, characterLayer);
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

    
}
