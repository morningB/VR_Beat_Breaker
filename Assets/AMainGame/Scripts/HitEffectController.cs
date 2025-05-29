using UnityEngine;
using UnityEngine.UI;

public class HitEffectController : MonoBehaviour
{
    private static HitEffectController _instance;

    public static HitEffectController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<HitEffectController>();
                if (_instance == null)
                    Debug.LogError("There is no active HitEffectController in the scene!");
            }
            return _instance;
        }
    }

    public Canvas canvas;
    public GameObject floatingTextPrefab;

    public void CreateHitEffect(Vector3 hitPoint, string message)
    {
        GameObject textObj = Instantiate(floatingTextPrefab, canvas.transform);
        
        // Canvas가 World Space일 경우: localPosition 사용
        textObj.GetComponent<RectTransform>().localPosition =
            canvas.transform.InverseTransformPoint(hitPoint + new Vector3(0, 0.2f, 0));

        // 카메라 정면을 바라보게 회전
        textObj.transform.LookAt(Camera.main.transform);
        textObj.transform.Rotate(0, 180, 0);

        // 텍스트 설정
        Text text = textObj.GetComponent<Text>(); // 또는 TMP_Text
        text.text = message;

        Destroy(textObj, 1.5f);
    }
}
