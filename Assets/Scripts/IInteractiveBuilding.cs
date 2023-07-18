using UnityEngine;
using System.Collections;
public interface IInteractiveBuilding
{
    public IEnumerator BuildingInteraction(GameObject triggerObject, IInteractiveBuilding triggerBuilding = null);
    public GameObject GetGameObject();
}