using UnityEngine;

public class PlayerInventoryView : MonoBehaviour
{
    [HideInInspector]
    public GameResource resource;

    GameResourceView resourceView;
    [SerializeField]
    GameResourceView resourceViewPrefab;

    [SerializeField]
    Transform resourceViewsParent;

    [SerializeField] private Canvas buildingCanvas;
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject == gameObject)
            {
                buildingCanvas.gameObject.SetActive(true);
            }
            else
            {
                buildingCanvas.gameObject.SetActive(false);
            }
        }
        else
        {
            buildingCanvas.gameObject.SetActive(false);
        }
    }

    public void UpdateView(GameResourceSO resourceSO, int amount)
    {
        if(resourceView != null)
        {
            Destroy(resourceView.gameObject);
        }
        resourceView = CreateResource(resourceSO);

        resource.resourceSO = resourceSO;
        resource.amount = amount;
        resourceView.UpdateAmount(resource.amount);
    }

    public void UpdateView()
    {
        Destroy(resourceView.gameObject);
    }


    private GameResourceView CreateResource(GameResourceSO resourceSO)
    {
        resource = new GameResource(resourceSO);

        GameResourceView resourceView = Instantiate<GameResourceView>(resourceViewPrefab, resourceViewsParent);
        resourceView.resourceSO = resourceSO;
        resourceView.UpdateResourceName(resourceSO.resourceName);
        return resourceView;
    }
}
