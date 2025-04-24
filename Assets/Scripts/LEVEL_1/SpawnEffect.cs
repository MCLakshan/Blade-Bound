using System.Collections;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    [SerializeField] float Delay = 0;

    void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    // Coroutine to wait for 3 seconds and then destroy the GameObject
    IEnumerator DestroyAfterDelay()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(Delay);

        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
    }
}
