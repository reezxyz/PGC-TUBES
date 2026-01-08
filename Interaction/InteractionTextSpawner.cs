using TMPro;
using UnityEngine;

public class InteractionTextSpawner : MonoBehaviour
{
    [Header("UI")]
    public GameObject textPrefab;
    public string interactText = "Interact";

    [Header("Positioning")]
    public float heightOffset = 0.2f;
    public float cameraForwardOffset = 0.4f; // INI KUNCINYA

    GameObject textInstance;
    TMP_Text tmp;

    void Start()
    {
        textInstance = Instantiate(textPrefab);
        tmp = textInstance.GetComponentInChildren<TMP_Text>();
        tmp.text = interactText;

        textInstance.SetActive(false);
    }

    void LateUpdate()
    {
        if (!textInstance || !textInstance.activeSelf || Camera.main == null)
            return;

        Transform cam = Camera.main.transform;

        // arah dari object ke kamera (INI YANG BENAR)
        Vector3 toCamera = (cam.position - transform.position).normalized;

        Vector3 targetPos =
            transform.position +
            Vector3.up * heightOffset +
            toCamera * cameraForwardOffset;

        textInstance.transform.position = targetPos;

        // hadap kamera
        textInstance.transform.rotation =
            Quaternion.LookRotation(textInstance.transform.position - cam.position);
    }


    public void Show()
    {
        if (textInstance)
            textInstance.SetActive(true);
    }

    public void Hide()
    {
        if (textInstance)
            textInstance.SetActive(false);
    }

    void OnDestroy()
    {
        if (textInstance)
            Destroy(textInstance);
    }
}
