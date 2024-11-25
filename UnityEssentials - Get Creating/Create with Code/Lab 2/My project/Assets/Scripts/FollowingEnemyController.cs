using UnityEngine;

public class FollowingEnemyController : MonoBehaviour
{
    public float speed = 3f;
    public float followDistance = 10f;  // Distance within which the enemy will follow the player
    private Transform player;
    private Rigidbody enemyRb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRb = GetComponent<Rigidbody>();

        // Freeze Y position to prevent sinking
        enemyRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= followDistance)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                enemyRb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
            }
        }
    }
}