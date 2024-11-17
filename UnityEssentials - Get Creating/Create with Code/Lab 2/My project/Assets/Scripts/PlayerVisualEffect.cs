using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualEffect : MonoBehaviour
{
    public Color invincibilityColor = Color.yellow;
    public float blinkRate = 0.2f;
    
    private Renderer playerRenderer;
    private Color originalColor;
    private bool isBlinking = false;

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.color;
    }

    public void StartInvincibilityEffect(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(InvincibilityEffect(duration));
    }

    private System.Collections.IEnumerator InvincibilityEffect(float duration)
    {
        isBlinking = true;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Color currentColor = Color.Lerp(invincibilityColor, originalColor, t);

            // Blink effect
            if (Mathf.PingPong(Time.time, blinkRate) > blinkRate / 2f)
            {
                playerRenderer.material.color = currentColor;
            }
            else
            {
                playerRenderer.material.color = originalColor;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerRenderer.material.color = originalColor;
        isBlinking = false;
    }
}
