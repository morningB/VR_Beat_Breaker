using UnityEngine;

public class CubeMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * Random.Range(0, 8);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Deadzone"))
        {
            Debug.Log("Miss!");
            GameManager.Instance.MissCube();  // 점수 차감 등
            Destroy(gameObject);             // 큐브 제거
        }
    }
}
