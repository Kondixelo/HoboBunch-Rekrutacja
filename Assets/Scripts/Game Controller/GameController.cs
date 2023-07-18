using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class GameController : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navSurface;
    void Start()
    {
        GameEvents.instance.OnPlaceBuilding += UpdateNavigationSurface;
        UpdateNavigationSurface();
    }

    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        GameEvents.instance.OnPlaceBuilding -= UpdateNavigationSurface;
    }

    void Update()
    {
        
    }



    public void UpdateNavigationSurface()
    {
        navSurface.BuildNavMesh();
    }
}
