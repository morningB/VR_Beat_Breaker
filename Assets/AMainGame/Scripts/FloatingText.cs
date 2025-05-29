using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public float floatSpeed = 2f;
    public float fadeSpeed = 1f;
    private Text text;
    private Color startColor;

    void Start()
    {
        text = GetComponent<Text>();
        startColor = text.color;
    }

    void Update()
    {
        //transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
        startColor.a -= fadeSpeed * Time.deltaTime;
        text.color = startColor;

        if (startColor.a <= 0) Destroy(gameObject);
    }
}
