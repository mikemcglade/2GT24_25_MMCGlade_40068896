using UnityEngine;
using System.Collections;

public class PlayerShrink : MonoBehaviour
{
    [SerializeField] private Vector3 shrunkScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private float shrinkDuration = 3.0f;
    [SerializeField] private float cooldownDuration = 5.0f;

    private Vector3 originalScale;
    private bool canShrink = true;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && canShrink)
        {
            StartCoroutine(ShrinkCoroutine());
        }
    }

    private IEnumerator ShrinkCoroutine()
    {
        canShrink = false;

        // Shrink
        transform.localScale = shrunkScale;
        yield return new WaitForSeconds(shrinkDuration);

        // Return to original size
        transform.localScale = originalScale;

        // Cooldown
        yield return new WaitForSeconds(cooldownDuration);
        canShrink = true;
    }
}