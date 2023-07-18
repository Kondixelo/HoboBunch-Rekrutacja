using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBrowser : MonoBehaviour
{
    public static ObjectBrowser instance;
    private void Awake()
    {
        instance = this;
    }
    public T FindNearest<T>(GameObject callingObject) where T : Component
    {
        T[] foundObjects = GameObject.FindObjectsOfType<T>();
    
        T nearestObject = null;
        float closestDistance = Mathf.Infinity;
    
        if (foundObjects == null || foundObjects.Length == 0)
        {
            return null;
        }
    
        foreach (T obj in foundObjects)
        {
            float distance = Vector3.Distance(callingObject.transform.position, obj.transform.position);
    
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestObject = obj;
            }
        }
   
        return nearestObject;
    }
}
