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

        if(playerAgent.velocity.magnitude > 0)
        {
            GameEvents.instance.PlayerWalking();
        }
        else
        {
            GameEvents.instance.PlayerStopWalking();
        }
    }

    public void MoveTo(Vector3 destination)
    {
        playerAgent.SetDestination(destination);
    }
}
