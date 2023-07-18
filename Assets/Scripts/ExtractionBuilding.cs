using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionBuilding : MonoBehaviour, IInteractiveBuilding
{
    public float timeToExtract = 5f;

    float timeProgress = 0f;
    public GameResourceSO resourceSO;
    public GameResourcesList resourcesList;
    
    [SerializeField]
    FloatingText floatingTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        timeProgress = 0f;
    }

    // Update is called once per frame
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
        Debug.Log("1: Interkacja");
        if(initializingObject.TryGetComponent<PlayerInventory>(out PlayerInventory playerInv))
        {
            Debug.Log("2-1: PlayerInventory");
            if (playerInv.IsInventoryEmpty())
            {
                Debug.Log("3-1-1:  PusteInventory");
                yield return new WaitUntil(() => resourcesList.CheckResource(resourceSO, 1));
                Debug.Log("3-1-2:  Zrobiony zasob");
                if (playerInv.AddItemInventory(resourceSO))
                {
                    Debug.Log("DODAJE DO INVENTORY: " + resourceSO.name);
                }
                else
                {
                    Debug.LogWarning("NIE UDALO SIE DODAC: " + resourceSO.name);
                }

                if (triggerBuilding!= null)
                {
                    PlayerBuildingInteraction playerInt = initializingObject.GetComponent<PlayerBuildingInteraction>();
                    Debug.Log("4-1:  trigger building");

                    StartCoroutine(playerInt.InteractBuilding(triggerBuilding, this));
                }
            }
            else
            {
                Debug.Log("3-2:  Pelne Inventory");
            }
        }
        else
        {
            Debug.Log("2-2: Brak PlayerInventory");
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
