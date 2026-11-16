using UnityEngine;

public class WeaponKnockback : MonoBehaviour
{
    public float range = 3f;
    public float knockbackForce = 10f;

    // Reference to the player's camera
    public Camera playerCamera;

    void Update()
    {
        // Left mouse button uses the weapon
        if (Input.GetMouseButtonDown(0))
        {
            UseWeapon();
        }
    }

    void UseWeapon()
    {
        // Create a ray from the centre of the camera
        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        RaycastHit hit;

        // Check if the ray hits something
        if (Physics.Raycast(ray, out hit, range))
        {
            // Check if the object has the EnemyFollow script
            EnemyFollow enemy = hit.collider.GetComponent<EnemyFollow>();

            if (enemy != null)
            {
                // Calculate the direction away from the player
                Vector3 knockbackDirection =
                    hit.transform.position - playerCamera.transform.position;

                // Knock the enemy away
                enemy.KnockBack(knockbackDirection, knockbackForce);
            }
        }
    }
}