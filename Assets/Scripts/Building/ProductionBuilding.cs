using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding : MonoBehaviour, IInteractiveBuilding
{
    public int inputAmountRequired = 2;
    public GameResourceSO inputResourceSO;
    public GameResourceSO outputResourceSO;

    public float timeToExtract = 5f;

    float timeProgress = 0f;
    public GameResourcesList resourcesList;

    [SerializeField]
    FloatingText floatingTextPrefab;

    void Start()
    {
        timeProgress = 0f;
    }

    void Update()
    {
        timeProgress += Time.deltaTime;

        if (timeProgress > timeToExtract)
        {
            Product();
            timeProgress = 0f;
        }
    }

    private void Product()
    {
        if (resourcesList.TryUse(inputResourceSO, inputAmountRequired))
        {
            resourcesList.Add(outputResourceSO, 1);

            var floatingText = Instantiate(floatingTextPrefab, transform.position + Vector3.up, Quaternion.identity);
            floatingText.SetText($"{inputResourceSO.resourceName} -{inputAmountRequired}\n{outputResourceSO.resourceName}+1");
        }
    }

    private void AddResources(GameResourceSO resourceSO)
    {
        resourcesList.Add(resourceSO, 1);

        var floatingText = Instantiate(floatingTextPrefab, transform.position + Vector3.up, Quaternion.identity);
        floatingText.SetText(resourceSO.resourceName + " +1");
    }


    public IEnumerator BuildingInteraction(GameObject initializingObject, IInteractiveBuilding triggerBuilding = null)
    {
        if (!initializingObject.TryGetComponent<PlayerInventory>(out PlayerInventory playerInv))
            yield break;

        if (playerInv.IsInventoryEmpty())
        {
            if (resourcesList.CheckResource(outputResourceSO, 1))
            {
                playerInv.AddItemInventory(outputResourceSO);
            }
            else
            {
                ExtractionBuilding nearestExtractionBuilding = ObjectBrowser.instance.FindNearest<ExtractionBuilding>(gameObject);
                if (nearestExtractionBuilding != null)
                {
                    PlayerBuildingInteraction playerInt = initializingObject.GetComponent<PlayerBuildingInteraction>();
                    StartCoroutine(playerInt.InteractBuilding(nearestExtractionBuilding, this));
                }
            }
        }
        else if (playerInv.CheckInventoryItem(inputResourceSO))
        {
            AddResources(playerInv.GetInventoryItem());
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
