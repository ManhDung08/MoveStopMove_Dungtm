using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow> 
{
    [SerializeField] private Transform player;
    [SerializeField] private Player playerCtrl;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Material blurMaterial;

    private Material originalMaterial;
    private Transform currentObstacle;
    private bool isBlocked;
    private float speed;

    private void Start()
    {
        isBlocked = false;
        speed = 5f;
    }


    private void Update()
    {
        //if(GameManager.Instance.currentState != GameState.InGame)
        //{

        //    return;
        //}
        CheckObtacle();
        transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0, 12.5f, -12.5f), Time.deltaTime * speed);
        transform.transform.rotation = Quaternion.Euler(45, 0, 0);
    }


    private void CheckObtacle()
    {

        Vector3 playerPos = Camera.main.WorldToScreenPoint(player.position);
        Ray ray = Camera.main.ScreenPointToRay(playerPos);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, obstacleLayer))
        {
            if (!isBlocked)
            {
                isBlocked = true;
                currentObstacle = hit.transform;
                originalMaterial = currentObstacle.GetComponent<MeshRenderer>().material;
                currentObstacle.GetComponent<MeshRenderer>().material = blurMaterial;
            }
        }
        else
        {
            if (originalMaterial != null)
            {
                currentObstacle.GetComponent<MeshRenderer>().material = originalMaterial;
                currentObstacle = null;
                originalMaterial = null;
            }
            isBlocked = false;
        }


    }
}
