using System.Collections;
using UnityEngine;

public class ExtractionBuilding : MonoBehaviour, IInteractiveBuilding
{
    [SerializeField] private float timeToExtract = 5f;

    private float timeProgress = 0f;
    [SerializeField] private GameResourceSO resourceSO;
    [SerializeField] private GameResourcesList resourcesList;
    [SerializeField] private FloatingText floatingTextPrefab;

    void Start()
    {
        timeProgress = 0f;
    }

    void Update()
    {
        timeProgress += Time.deltaTime;

        if (timeProgress > timeToExtract)
        {
            Extract();
            timeProgress = 0f;
        }
    }

    private void Extract()
    {
        resourcesList.Add(resourceSO, 1);

        var floatingText = Instantiate(floatingTextPrefab, transform.position + Vector3.up, Quaternion.identity);
        floatingText.SetText(resourceSO.resourceName + " +1");
    }

    public IEnumerator BuildingInteraction(GameObject initializingObject, IInteractiveBuilding triggerBuilding = null)
    {
        if (initializingObject.TryGetComponent<PlayerInventory>(out PlayerInventory playerInv) && playerInv.IsInventoryEmpty())
        {
            yield return new WaitUntil(() => resourcesList.CheckResource(resourceSO, 1));
            playerInv.AddItemInventory(resourceSO);

            if (triggerBuilding != null)
            {
                PlayerBuildingInteraction playerInt = initializingObject.GetComponent<PlayerBuildingInteraction>();
                StartCoroutine(playerInt.InteractBuilding(triggerBuilding, this));
            }
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
