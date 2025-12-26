using UnityEngine;

public class GasController : MonoBehaviour
{
    public ParticleSystem gasParticle;
    public Collider gasBlocker;

    private bool gasActive = true;

    void Start()
    {
        SetGasState(gasActive);
    }

    public void SetGasState(bool active)
    {
        gasActive = active;

        if (active)
        {
            gasParticle.Play();
            gasBlocker.enabled = true;
        }
        else
        {
            gasParticle.Stop(); // ← ini kuncinya
            gasBlocker.enabled = false;
        }
    }

    public void ToggleGas()
    {
        SetGasState(!gasActive);
    }
}
