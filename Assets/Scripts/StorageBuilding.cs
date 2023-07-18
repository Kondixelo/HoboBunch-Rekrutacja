using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBuilding : MonoBehaviour, IInteractiveBuilding
{
    public GameResourcesList resourcesList;

    [SerializeField]
    FloatingText floatingTextPrefab;
    private void AddResources(GameResourceSO resourceSO)
    {
        resourcesList.Add(resourceSO, 1);

        var floatingText = Instantiate(floatingTextPrefab, transform.position + Vector3.up, Quaternion.identity);
        floatingText.SetText(resourceSO.resourceName + " +1");
    }
    public IEnumerator BuildingInteraction(GameObject initializingObject, IInteractiveBuilding triggerBuilding = null)
    {
        yield return null;
        if (initializingObject.TryGetComponent<PlayerInventory>(out PlayerInventory playerInv))
        {
            if (!playerInv.IsInventoryEmpty())
            {
                AddResources(playerInv.GetInventoryItem());
            }
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
