using UnityEngine;

public class Building : MonoBehaviour
{
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
}
