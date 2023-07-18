using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private LayerMask groundMask;
    private NavMeshAgent playerAgent;

    private void Awake()
    {
        playerAgent = gameObject.GetComponent<NavMeshAgent>();
    }
    void Start()
    {

        groundMask = LayerMask.NameToLayer("Ground");
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out RaycastHit hitPoint, Mathf.Infinity))
            {
                if (hitPoint.collider.gameObject.layer == groundMask)
                {
                    MoveTo(hitPoint.point);
                }
            }
        }
    }

    public void MoveTo(Vector3 destination)
    {
        Debug.Log("RUSZ SIE");
        playerAgent.SetDestination(destination);
    }
}