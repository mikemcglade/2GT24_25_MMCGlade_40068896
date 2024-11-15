using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private GameManager gameManager;
    public int pointValue;
    public ParticleSystem explosionParticle;
    private bool hasCollided = false;
    //[SerializeField] ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider other)
    {
        Debug.Log("Collision detected with " + other.tag);

        if (other.CompareTag("Player") && !hasCollided)
        {
            Debug.Log("Player collision processed");
            hasCollided = true;
            gameManager.AddLives(-1);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Bullet"))
        {
      
            //scoreManager.AddPoint();
            gameManager.AddScore(pointValue);
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

            Destroy(other.gameObject);

        }
    }
}
