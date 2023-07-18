using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourcesList : MonoBehaviour
{
    [SerializeField]
    public List<GameResource> resources;
    [SerializeField]
    public List<GameResourceSO> resourceSOs;

    [SerializeField]
    List<GameResourceView> resourceViews;
    [SerializeField]
    GameResourceView resourceViewPrefab;

    [SerializeField]
    Transform resourceViewsParent;

    // Start is called before the first frame update
    void Start()
    {
        resources = new List<GameResource>();
        resourceViews = new List<GameResourceView>();

        foreach (var resourceSO in resourceSOs)
        {
            CreateResource(resourceSO);
        }
    }

    public void Add(GameResourceSO resourceSO, int amount)
    {
        var resource = resources.Find((x) => x.resourceSO == resourceSO);

        if (resource == null)
        {
            CreateResource(resourceSO);
        }

        var resourceView = resourceViews.Find((x) => x.resourceSO == resourceSO);

        resource.amount += amount;
        resourceView.UpdateAmount(resource.amount);
    }

    public void Remove(GameResourceSO resourceSO, int amount)
    {
        Debug.LogWarning("SPRAWDZAM REMOVE");
        var resource = resources.Find((x) => x.resourceSO == resourceSO);

        if (resource != null)
        {
            Debug.LogWarning("ISTNIEJE REMOVE");
            resource.amount -= amount;
            var resourceView = resourceViews.Find((x) => x.resourceSO == resourceSO);
            resourceView.UpdateAmount(resource.amount);
        }
    }

    public bool CheckResource(GameResourceSO resourceSO, int amount)
    {
        Debug.LogWarning("SPRWDZAM: " + resourceSO.name);
        var resource = resources.Find((x) => x.resourceSO == resourceSO);

        if(resource != null)
        {
            Debug.Log(resource.amount);
            Debug.LogWarning("ISTNIEJE" );
            if (resource.amount > 0 && amount <= resource.amount)
            {
                Debug.LogWarning("WYSTARCZAJACA ILOSC");
                Remove(resourceSO, amount);
                return true;
            }
        }
        return false;
    }

    public bool TryUse(GameResourceSO resourceSO, int amount)
    {
        var resource = resources.Find((x) => x.resourceSO == resourceSO);

        if (resource == null)
        {
            CreateResource(resourceSO);
        }

        var resourceView = resourceViews.Find((x) => x.resourceSO == resourceSO);

        if (amount > resource.amount)
        {
            return false;
        }

        resource.amount -= amount;
        resourceView.UpdateAmount(resource.amount);

        return true;
    }

    private void CreateResource(GameResourceSO resourceSO)
    {
        var resource = new GameResource(resourceSO);
        resources.Add(resource);

        GameResourceView resourceView = Instantiate<GameResourceView>(resourceViewPrefab, resourceViewsParent);
        resourceView.resourceSO = resourceSO;
        resourceView.UpdateResourceName(resourceSO.resourceName);
        resourceViews.Add(resourceView);
    }
}
