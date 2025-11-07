using UnityEngine;

public class HitSound : MonoBehaviour
{
    public AudioClip hitClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Õ“Ë‚µ‚½‚çŒø‰Ê‰¹‚ğ–Â‚ç‚·
        if (hitClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitClip);
        }
    }
}