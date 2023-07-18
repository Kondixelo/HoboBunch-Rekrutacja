using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float floatingSpeed = 5f;
    [SerializeField] private float timeToDestroy = 1.2f;
    float startTime;

    [SerializeField]
    TMPro.TMP_Text tmpText;

    void Start()
    {
        startTime = Time.time;
    }

    public void SetText(string text)
    {
        tmpText.text = text;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + floatingSpeed * Time.deltaTime, transform.position.z);

        if (startTime + timeToDestroy < Time.time)
        {
            Destroy(gameObject);
        }
    }
}
