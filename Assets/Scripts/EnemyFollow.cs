using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform player;
    public float speed = 3f;
    private Rigidbody rb;

    // Reference to the enemy hit sound
    private AudioSource audioSource;

    // Determines if the enemy is currently knocked back
    private bool isKnockedBack = false;

    void Start()
    {
        // Find the player using the Player tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the Rigidbody attached to the enemy
        rb = GetComponent<Rigidbody>();

        // Get the AudioSource attached to the enemy
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Only follow the player if not being knocked back
        if (player != null && !isKnockedBack)
        {
            // Calculate the direction towards the player
            Vector3 direction = player.position - transform.position;

            // Stop vertical movement
            direction.y = 0;

            // Move towards the player
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
    }

    // This function knocks the enemy away
    public void KnockBack(Vector3 direction, float force)
    {
        // Play the hit sound when the enemy is hit
        if (audioSource != null)
        {
            audioSource.Play();
        }

        StartCoroutine(KnockBackRoutine(direction, force));
    }

    System.Collections.IEnumerator KnockBackRoutine(Vector3 direction, float force)
    {
        // Stop the enemy from following temporarily
        isKnockedBack = true;

        // Apply knockback force
        rb.AddForce(direction.normalized * force, ForceMode.Impulse);

        // Wait for one second
        yield return new WaitForSeconds(1f);

        // Allow the enemy to follow again
        isKnockedBack = false;
    }
}