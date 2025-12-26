using UnityEngine;

public class NoiseSystem : MonoBehaviour
{
    public static NoiseSystem Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void MakeNoise(Vector3 position, float radius)
    {
        // Untuk debug sekarang
        Debug.Log($"Noise made at {position} with radius {radius}");

        // Visual debug
        StartCoroutine(DebugNoise(position, radius));
    }

    System.Collections.IEnumerator DebugNoise(Vector3 pos, float radius)
    {
        float t = 0f;
        while (t < 0.3f)
        {
            t += Time.deltaTime;
            Debug.DrawLine(pos, pos + Vector3.up * radius, Color.red);
            yield return null;
        }
    }
}
