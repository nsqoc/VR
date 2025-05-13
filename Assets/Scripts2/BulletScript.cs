using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private bool hasHit = false; // Prevents multiple score counts for the same hit

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return; 
        hasHit = true;

        Debug.Log("🔴 Bullet hit: " + collision.collider.name); // Logs the object hit

        string hitTag = collision.collider.tag;
        Debug.Log("✅ Hit Tag: " + hitTag); // Logs the tag of the hit object

        int points = 0;

        switch (hitTag)
        {
            case "X_Zone":
                points = 50;
                break;
            case "Nine_Zone":
                points = 30;
                break;
            case "Eight_Zone":
                points = 20;
                break;
            case "Target":
                points = 10;
                break;
            default:
                Debug.Log("⚠️ No matching tag found, assigning 0 points.");
                points = 0;
                break;
        }

        Debug.Log("⭐ Points to be added: " + points); // Logs the points being added

        if (points > 0)
        {
            ScoreManager.Instance.AddScore(points);
        }

        Destroy(gameObject); // Destroy the bullet after the hit
    }
}
