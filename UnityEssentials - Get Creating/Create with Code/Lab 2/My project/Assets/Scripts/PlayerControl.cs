using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5.0f;
    public float smoothness = 0.1f;
    private float zBound = 10;
    public float fireRate = 1f;
    public float canFire = -1f;
    private Rigidbody playerRb;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public bool hasPowerup;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetVelocity;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.freezeRotation = true; // Prevent the player from rotating

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
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        targetVelocity = new Vector3(horizontalInput, 0, verticalInput).normalized * speed;
        playerRb.velocity = Vector3.SmoothDamp(playerRb.velocity, targetVelocity, ref velocity, smoothness);
        // playerRb.AddForce(Vector3.forward * speed * verticalInput);
        // playerRb.AddForce(Vector3.right * speed * horizonalInput);
         
    }

    void ConstrainPlayerPosition()
    {
        if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }

    // code to constrain the player oth sides of z, which isn't necessary
    // Vector3 pos = transform.position;
    // pos.z = Mathf.Clamp(pos.z, -zBound, zBound);
    // transform.position = pos;
  
    // old code to constrain the play on z axis
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
}
