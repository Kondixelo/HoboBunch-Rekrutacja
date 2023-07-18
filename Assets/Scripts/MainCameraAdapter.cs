using UnityEngine;

public class MainCameraAdapter : MonoBehaviour
{
    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;   
    }
}
