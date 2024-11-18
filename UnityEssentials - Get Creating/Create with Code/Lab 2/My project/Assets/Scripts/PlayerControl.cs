using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5.0f;
    private float zBound = 10;
    public float fireRate = 1f;
    public float canFire = -1f;
    private Rigidbody playerRb;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public bool hasPowerup;



    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MovePlayer();

    }
    void Update()
    {
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
        float horizonalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizonalInput, 0, verticalInput).normalized * speed;
        playerRb.velocity = new Vector3(movement.x, playerRb.velocity.y, movement.z);
        // playerRb.AddForce(Vector3.forward * speed * verticalInput);
        // playerRb.AddForce(Vector3.right * speed * horizonalInput);
         
    }

    void ConstrainPlayerPosition()
    {
        // code to constrain the player oth sides of z, which isn't necessary
        // Vector3 pos = transform.position;
        // pos.z = Mathf.Clamp(pos.z, -zBound, zBound);
        // transform.position = pos;
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
            StartCoroutine(PowerupCountdownRoutine());
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

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
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
