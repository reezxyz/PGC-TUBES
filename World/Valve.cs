using UnityEngine;

public class ValveHold : MonoBehaviour, IInteractable
{
    public GasController targetGas;
    public Transform valveHandle;

    public float maxRotateAngle = -120f;
    public float holdDuration = 2f;
    public float returnSpeed = 0.5f;
    public ValveProgressUI progressUI;

    float holdProgress = 0f;
    bool isActivated = false;
    bool isHolding = false;
    bool isReturning = false;
    bool uiLocked = false;

    Quaternion startRotation;
    Quaternion endRotation;

    void Start()
    {
        startRotation = valveHandle.localRotation;
        endRotation = Quaternion.Euler(0, 0, maxRotateAngle) * startRotation;
    }

    void Update()
    {
        if (isActivated) return;

        if (isHolding)
        {
            holdProgress += Time.deltaTime / holdDuration;
            holdProgress = Mathf.Clamp01(holdProgress);

            if (holdProgress >= 1f)
            {
                CompleteValve();
            }
        }
        else if (isReturning)
        {
            holdProgress -= Time.deltaTime * returnSpeed;
            holdProgress = Mathf.Clamp01(holdProgress);

            if (holdProgress <= 0f)
            {
                isReturning = false;
            }
        }

        if (progressUI != null && !uiLocked)
        progressUI.SetProgress(holdProgress);

        UpdateValveRotation();
    }

    public void Interact()
    {
        if (isActivated) return;

        isHolding = true;
        isReturning = false;

        if (progressUI != null)
            progressUI.SetVisible(true);
    }


    void LateUpdate()
    {
        if (isHolding && Input.GetKeyUp(KeyCode.E))
        {
            CancelHold();
        }
    }

    void UpdateValveRotation()
    {
        valveHandle.localRotation =
            Quaternion.Lerp(startRotation, endRotation, holdProgress);
    }

    void CompleteValve()
    {
        isActivated = true;
        isHolding = false;
        isReturning = false;

        holdProgress = 1f;
        valveHandle.localRotation = endRotation;

        uiLocked = true;

        if (progressUI != null)
            progressUI.SetVisible(false);

        if (targetGas != null)
            targetGas.ToggleGas();
    }

    void CancelHold()
    {
        isHolding = false;
        isReturning = true;
    }

    public string GetInteractText()
    {
        return isActivated ? "Valve engaged" : "Hold E to turn valve";
    }
}
