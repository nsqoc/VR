using UnityEngine;

public class ExplosionTrigger : MonoBehaviour
{
    public ParticleSystem smokeEffect; // ضع الـ Particle System هنا

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // إذا اصطدم بالأرض
        {
            smokeEffect.Play(); // تشغيل الدخان
        }
    }
}
