using UnityEngine;

public class HealPowerUp : MonoBehaviour
{

    public int healthPoints;
    public AudioClip pickupSound;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(PlayerHealth.instance.currentHealth != PlayerHealth.instance.maxHealth)
        {
            AudioManager.instance.PlayClipAt(pickupSound, transform.position);
            PlayerHealth.instance.HealPlayer(healthPoints);
            Destroy(gameObject);
        }
    }
}
