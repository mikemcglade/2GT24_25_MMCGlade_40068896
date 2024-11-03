using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup : MonoBehaviour
{
    public float duration = 7f;
    public GameObject visualCue;
    public float visualCueTime = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControl playerControlScript = other.GetComponent<PlayerControl>();
            if (playerControlScript != null)
            {
                StartCoroutine(ApplyInvincibility(playerControlScript));
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator ApplyInvincibility(PlayerControl player)
    {
        player.SetInvincible(true);
        
        yield return new WaitForSeconds(duration - visualCueTime);
        
        visualCue.SetActive(true);
        
        yield return new WaitForSeconds(visualCueTime);
        
        player.SetInvincible(false);
        visualCue.SetActive(false);
        
        Destroy(gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
