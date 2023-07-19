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

    //Player
    public event Action OnPlayerWalking;
    public event Action OnPlayerStopWalking;

    public event Action OnPlayerCarrying;
    public event Action OnPlayerStopCarrying;


    public void GameStart()
    {
        OnGameStart?.Invoke();
    }

    public void PlaceBuilding()
    {
        OnPlaceBuilding?.Invoke();
    }

    public void PlayerCarrying()
    {
        OnPlayerCarrying?.Invoke();
    }

    public void PlayerWalking()
    {
        OnPlayerWalking?.Invoke();
    }

    public void PlayerStopCarrying()
    {
        OnPlayerStopCarrying?.Invoke();
    }

    public void PlayerStopWalking()
    {
        OnPlayerStopWalking?.Invoke();
    }
}