using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    private void Awake()
    {
        instance = this;
    }
    
    public event Action OnGameStart;
    public event Action OnPlaceBuilding;

    public void GameStart()
    {
        OnGameStart?.Invoke();
    }

    public void PlaceBuilding()
    {
        OnPlaceBuilding?.Invoke();
    }
}