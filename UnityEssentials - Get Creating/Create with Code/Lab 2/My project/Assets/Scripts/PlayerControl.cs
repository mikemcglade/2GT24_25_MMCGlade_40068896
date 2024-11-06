using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float speed = 5.0f;
    private float zBound = 10;
    public float fireRate = 1f;
    public float canFire = -1f;
    private Rigidbody playerRb;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public bool hasPowerup;
    //private bool isInvincible = false;

   //private GameManager playerControlScript;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //playerControlScript = GameObject.Find("Player").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerPosition();

        // if the player presses space bar, fire a bullet

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            FireBullet();
        }
        
    }
    private void FireBullet()
    {
        // add bullet cool down period
        canFire = Time.time + fireRate;

        Instantiate(projectilePrefab, projectileSpawnPoint.position, projectilePrefab.transform.rotation);

    }

    void MovePlayer()
    {
       // if (playerControlScript.isGameActive == true)
        {
            float horizonalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            playerRb.AddForce(Vector3.forward * speed * verticalInput);
            playerRb.AddForce(Vector3.right * speed * horizonalInput);
         }
    }

    void ConstrainPlayerPosition()
    {
    if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
    //if (transform.position.z > zBound)
    //    {
    //        transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
    //    }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Destroy(gameObject);
            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
        }
    }

    //public void SetInvincible(bool invincible)
//    {
  //      isInvincible = invincible;
    //}

 //   public bool IsInvincible()
   // {
     //   return isInvincible;
    //}

//     public void TakeDamage(int damage)
  //  {
    //    if (!isInvincible)
      //  {
            // Apply damage logic here
        //}
    //}
}
