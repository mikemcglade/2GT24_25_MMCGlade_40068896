using UnityEngine;

public class InvincibilityPowerup : MonoBehaviour
{
    public float invincibilityDuration = 7f;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.ActivateInvincibility(invincibilityDuration);
            Destroy(gameObject);
        }
    }
}