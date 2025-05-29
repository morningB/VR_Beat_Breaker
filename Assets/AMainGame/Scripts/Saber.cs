using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Saber : MonoBehaviour
{
    public LayerMask layer;
    Vector3 prevPos;
    public Text precision;
    public Canvas canvas;
    public GameObject floatingTextPrefab;
    void Start()
    {

    }
    
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1, layer))
        {
            float angle = Vector3.Angle(transform.position - prevPos, hit.transform.up);

            if (angle > 150)
            {
                Debug.Log("Perfect");
                // precision.text = "Perfect!!";
                CreateHitEffect(hit.point, "Perfect!!");
                GameManager.Instance.AddScore(3);
                //����Ʈ �߰��ϱ�(hit.transform)����
                Destroy(hit.transform.gameObject);
            }
            else if (angle > 130)
            {
                Debug.Log("Good");
                // precision.text = "Good!";
                GameManager.Instance.AddScore(1);
                CreateHitEffect(hit.point, "Good");
                //����Ʈ �߰��ϱ�(hit.transform)����
                Destroy(hit.transform.gameObject);
            }
        }
        prevPos = transform.position;

    }
    public void CreateHitEffect(Vector3 hitPoint, string message)
    {
        GameObject textObj = Instantiate(floatingTextPrefab, canvas.transform);
        
        // Canvas�� World Space�� ���: localPosition ���
        textObj.GetComponent<RectTransform>().localPosition =
            canvas.transform.InverseTransformPoint(hitPoint + new Vector3(0, 0.2f, 0));

        // ī�޶� ������ �ٶ󺸰� ȸ��
        textObj.transform.LookAt(Camera.main.transform);
        textObj.transform.Rotate(0, 180, 0);

        // �ؽ�Ʈ ����
        Text text = textObj.GetComponent<Text>(); // �Ǵ� TMP_Text
        text.text = message;
        if (message == "Good")
        {
            text.color = Color.blue;
        }
        else
        {
            text.color = Color.yellow;
        }

        Destroy(textObj, 1.5f);
    }


}
