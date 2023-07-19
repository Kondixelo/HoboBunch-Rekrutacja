using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private GameResourceSO inventoryItem;
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
            GameEvents.instance.PlayerCarrying();
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
        GameEvents.instance.PlayerStopCarrying();
        return tmp;
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
