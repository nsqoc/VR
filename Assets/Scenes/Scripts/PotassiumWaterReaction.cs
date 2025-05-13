using UnityEngine;

public class PotassiumWaterReaction : MonoBehaviour
{
    public GameObject potassium;
    public GameObject water;
    public GameObject explosionEffect;
    public float reactionDistance = 1.0f;
    public float explosionForce = 10.0f;
    public float explosionRadius = 5.0f;

    void Update()
    {
        if (potassium != null && water != null && Vector3.Distance(potassium.transform.position, water.transform.position) < reactionDistance)
        {
            React();
        }
    }

    void React()
    {
        // تفعيل تأثير الانفجار
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, potassium.transform.position, Quaternion.identity);
        }

        // تطبيق قوة الانفجار
        Collider[] colliders = Physics.OverlapSphere(potassium.transform.position, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, potassium.transform.position, explosionRadius);
            }
        }

        // تدمير البوتاسيوم والماء بعد التفاعل (إذا كان ذلك مقصودًا)
        if (potassium != null)
        {
            Destroy(potassium);
        }
        if (water != null)
        {
            Destroy(water);
        }
    }
}