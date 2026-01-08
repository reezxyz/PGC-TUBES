using UnityEngine;

public class WoodBoardInteract : MonoBehaviour, IInteractable
{
    [Header("Requirements")]
    public bool requiresPipe = true;          // tutorial: butuh pipa besi
    public float holdDuration = 3f;
    public float returnSpeed = 0.8f;

    [Header("Physics")]
    public Rigidbody boardRigidbody;           // aktif saat board lepas
    public Collider boardCollider;

    [Header("UI")]
    public ProgressUI progressUI;
    InteractionTextSpawner textSpawner;

    [Header("Audio (Optional)")]
    public AudioSource audioSource;
    public AudioClip pryLoop;
    public AudioClip boardBreak;

    float holdProgress = 0f;
    bool isRemoved = false;
    bool isHolding = false;
    bool isReturning = false;

    void Awake()
    {
        textSpawner = GetComponent<InteractionTextSpawner>();
    }

    void Start()
    {
        if (progressUI != null)
            progressUI.SetVisible(false);

        if (boardRigidbody != null)
            boardRigidbody.isKinematic = true;
    }

    // DIPANGGIL SETIAP FRAME SAAT DI-HOVER
    public void Interact()
    {
        if (isRemoved) return;

        // Cek syarat item (pipa)
        if (requiresPipe)
        {
            PlayerInventory inv = FindObjectOfType<PlayerInventory>();
            if (inv == null || !inv.hasPipe)
                return;
        }

        if (Input.GetKey(KeyCode.E))
        {
            StartHold();
        }
        else
        {
            StopHold();
        }
    }

    void Update()
    {
        if (isRemoved) return;

        if (isHolding)
        {
            holdProgress += Time.deltaTime / holdDuration;
            holdProgress = Mathf.Clamp01(holdProgress);

            progressUI.SetProgress(holdProgress);

            if (holdProgress >= 1f)
                RemoveBoard();
        }
        else if (isReturning)
        {
            holdProgress -= Time.deltaTime * returnSpeed;
            holdProgress = Mathf.Clamp01(holdProgress);

            progressUI.SetProgress(holdProgress);

            if (holdProgress <= 0f)
                isReturning = false;
        }
        // ⛔ JANGAN panggil SetProgress saat idle / hover saja
    }


    void StartHold()
    {
        isHolding = true;
        isReturning = false;

        if (progressUI != null)
            progressUI.SetVisible(true);

        if (audioSource && !audioSource.isPlaying)
        {
            audioSource.clip = pryLoop;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void StopHold()
    {
        if (!isHolding) return;

        isHolding = false;
        isReturning = true;

        if (audioSource && audioSource.isPlaying)
            audioSource.Stop();
    }

    void RemoveBoard()
    {
        isRemoved = true;
        isHolding = false;
        isReturning = false;

        if (audioSource)
        {
            audioSource.Stop();
            if (boardBreak) audioSource.PlayOneShot(boardBreak);
        }

        if (progressUI != null)
            progressUI.SetVisible(false);

        if (textSpawner != null)
            textSpawner.Hide();

        // Aktifkan physics (board jatuh)
        if (boardRigidbody != null)
        {
            boardRigidbody.isKinematic = false;
            boardRigidbody.AddForce(transform.forward * 1.5f, ForceMode.Impulse);
        }

        if (boardCollider != null)
            boardCollider.enabled = false;

        // (Opsional) bunyi/noise
        // NoiseSystem.Instance.MakeNoise(transform.position, 5f);
    }

    public void OnHoverEnter()
    {
        if (isRemoved) return;

        if (textSpawner != null)
            textSpawner.Show();
    }

    public void OnHoverExit()
    {
        if (isRemoved) return;

        StopHold();

        if (textSpawner != null)
            textSpawner.Hide();

        // ⛔ JANGAN sentuh progressUI di sini
    }
}
