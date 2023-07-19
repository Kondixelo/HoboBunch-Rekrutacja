using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject model;
    [SerializeField] private string isWalking;
    [SerializeField] private string isCarrying;
    [SerializeField] private float moveSpeed;

    private bool moveModel;
    void Start()
    {
        GameEvents.instance.OnPlayerCarrying += PlayerIsCarrying;
        GameEvents.instance.OnPlayerStopCarrying += PlayerStopCarrying;
        GameEvents.instance.OnPlayerWalking += PlayerIsWalking;
        GameEvents.instance.OnPlayerStopWalking += PlayerStopWalking;
    }

    private void OnDisable()
    {
        GameEvents.instance.OnPlayerCarrying -= PlayerIsCarrying;
        GameEvents.instance.OnPlayerStopCarrying -= PlayerStopCarrying;
        GameEvents.instance.OnPlayerWalking -= PlayerIsWalking;
        GameEvents.instance.OnPlayerStopWalking -= PlayerStopWalking;
    }

    private void Update()
    {
        if (moveModel)
        {
            model.transform.localPosition = Vector3.Lerp(model.transform.localPosition, Vector3.zero, Time.deltaTime * moveSpeed);
            model.transform.localRotation = Quaternion.Lerp(model.transform.localRotation, Quaternion.identity, Time.deltaTime * moveSpeed);
        }
    }


    private void PlayerIsWalking()
    {
        animator.SetBool(isWalking, true);
        moveModel = true;
    }

    private void PlayerStopWalking()
    {
        animator.SetBool(isWalking, false);
        moveModel = false;
    }

    private void PlayerIsCarrying()
    {
        animator.SetBool(isCarrying, true);
    }

    private void PlayerStopCarrying()
    {
        animator.SetBool(isCarrying, false);
    }
}
