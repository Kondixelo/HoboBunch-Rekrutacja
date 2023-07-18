using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameResourceSO inventoryItem;

    public GameObject backpack;
    private PlayerInventoryView playerInvView;
    private void Start()
    {
        inventoryItem = null;
        playerInvView = GetComponent<PlayerInventoryView>();
    }

    public bool AddItemInventory(GameResourceSO resource)
    {
        if (inventoryItem == null)
        {
            inventoryItem = resource;
            playerInvView.UpdateView(resource, 1);
            return true;
        }
        else
        {
            return false;
        }
    }

    public GameResourceSO GetInventoryItem()
    {
        GameResourceSO tmp = inventoryItem;
        inventoryItem = null;
        playerInvView.UpdateView();
        return tmp;
    }

    void Update()
    {
        if(inventoryItem != null)
        {
            backpack.SetActive(true);
        }
        else
        {
            backpack.SetActive(false);
        }
    }

    public bool CheckInventoryItem(GameResourceSO excpectedItem)
    {
        if(inventoryItem == excpectedItem)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsInventoryEmpty()
    {
        if(inventoryItem == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
