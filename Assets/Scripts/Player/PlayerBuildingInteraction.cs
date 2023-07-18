using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBuildingInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask interactiveObjectMask;
    [SerializeField] private int minimumDistanceFromObject;
    private PlayerMovement playerMovement;
    public GameObject clickedBuilding;
    private float distanceFromBuilding;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out RaycastHit hitPoint, Mathf.Infinity))
            {
                clickedBuilding = null;
                if (hitPoint.transform.gameObject.TryGetComponent<IInteractiveBuilding>(out IInteractiveBuilding interactiveBuilding))
                {
                    clickedBuilding = hitPoint.transform.gameObject;
                    Debug.Log("BUDYNEK INTERAKTYWNY");
                    StopAllCoroutines();
                    StartCoroutine(InteractBuilding(interactiveBuilding));
                }
            }
        }

        if (clickedBuilding != null)
        {
            distanceFromBuilding = Vector3.Distance(gameObject.transform.position, clickedBuilding.transform.position);
        }

    }

    public IEnumerator InteractBuilding(IInteractiveBuilding objectToInspect, IInteractiveBuilding triggerBuilding = null)
    {
        if (triggerBuilding != null)
        {
            clickedBuilding = objectToInspect.GetGameObject();
        }
        distanceFromBuilding = Vector3.Distance(gameObject.transform.position, clickedBuilding.transform.position);
        if (distanceFromBuilding > minimumDistanceFromObject && playerMovement != null)
        {
            playerMovement.MoveTo(clickedBuilding.transform.position);
        }
        yield return new WaitUntil(() => distanceFromBuilding <= minimumDistanceFromObject);
        Debug.LogWarning("Interact Building");

        clickedBuilding = null;
        if(triggerBuilding != null)
        {
            StartCoroutine(objectToInspect.BuildingInteraction(gameObject,triggerBuilding));
        }
        else
        {
            StartCoroutine(objectToInspect.BuildingInteraction(gameObject));
        }
    }

}
