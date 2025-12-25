using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(AudioSource))]
public class LightFlickerBurst : MonoBehaviour
{
    private Light lightSource;
    private AudioSource audioSource;

    [Header("Stable State")]
    public float stableIntensity = 0.8f;

    [Header("Burst Flicker")]
    public float flickerIntensityMin = 0.2f;
    public float flickerIntensityMax = 0.7f;
    public float flickerDuration = 0.4f;
    public int flickerCount = 6;

    [Header("Burst Timing")]
    public float minTimeBetweenBursts = 4f;
    public float maxTimeBetweenBursts = 10f;

    [Header("Audio")]
    public AudioClip flickerSound;

    void Start()
    {
        lightSource = GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();

        lightSource.intensity = stableIntensity;

        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            // Lampu stabil lama
            yield return new WaitForSeconds(Random.Range(minTimeBetweenBursts, maxTimeBetweenBursts));

            // Play sound ONCE per burst
            if (flickerSound != null)
            {
                audioSource.PlayOneShot(flickerSound);
            }

            // Flicker cepat
            for (int i = 0; i < flickerCount; i++)
            {
                lightSource.intensity = Random.Range(flickerIntensityMin, flickerIntensityMax);
                yield return new WaitForSeconds(flickerDuration / flickerCount);
            }

            // Kembali stabil
            lightSource.intensity = stableIntensity;
        }
    }
}
