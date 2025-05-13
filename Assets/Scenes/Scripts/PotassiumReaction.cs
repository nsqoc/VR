using System.Collections;
using UnityEngine;

public class PotassiumReaction : MonoBehaviour
{
    public ParticleSystem flameEffect; // تأثير النار البنفسجية
    public ParticleSystem bubblesEffect; // تأثير الفقاعات
    public AudioSource reactionSound; // صوت التفاعل
    private bool hasReacted = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water") && !hasReacted) 
        {
            hasReacted = true;
            StartReaction();
        }
    }

    void StartReaction()
    {
        
        flameEffect.Play();
        bubblesEffect.Play();
        reactionSound.Play();

        
        Destroy(gameObject, 3f);
    }
}
