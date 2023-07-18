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
        Debug.Log("1: Interkacja");
        yield return null; 
        if (initializingObject.TryGetComponent<PlayerInventory>(out PlayerInventory playerInv))
        {
            Debug.Log("2-1: PlayerInventory");
            if (playerInv.IsInventoryEmpty())
            {
                Debug.Log("3-1-1:  PusteInventory");
                if (resourcesList.CheckResource(outputResourceSO, 1))
                {
                    Debug.Log("3-1-2:  Zrobiony zasob");
                    if (playerInv.AddItemInventory(outputResourceSO))
                    {
                        Debug.Log("DODAJE DO INVENTORY: " + outputResourceSO.name);
                    }
                    else
                    {
                        Debug.LogWarning("NIE UDALO SIE DODAC: " + outputResourceSO.name);
                    }
                }
                else
                {
                    Debug.Log("3-2:  Brak zasobow");
                    ExtractionBuilding nearestExtractionBuilding = ObjectBrowser.instance.FindNearest<ExtractionBuilding>(gameObject);

                    if (nearestExtractionBuilding != null)
                    {
                        Debug.Log("4-1:  Istnieje bud wydobywczy");
                        PlayerBuildingInteraction playerInt = initializingObject.GetComponent<PlayerBuildingInteraction>();

                        StartCoroutine(playerInt.InteractBuilding(nearestExtractionBuilding, this));
                    }
                    else
                    {
                        Debug.Log("4-2: Brak bud wydobywczy");
                    }
                }
            }
            else
            {
                Debug.Log("3-2:  Pelne Inventory");
                if (playerInv.CheckInventoryItem(inputResourceSO))
                {
                    Debug.Log("5-1:  item pasuje, dodaje zasob do produkcji");
                    AddResources(playerInv.GetInventoryItem());
                }
                else
                {
                    Debug.Log("5-2:  item NIE pasuje");
                }
            }
        }
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
