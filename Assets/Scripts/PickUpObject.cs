using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public AudioClip sound;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(sound, transform.position);
            Inventory.instance.addCoins(1);
            CurrentSceneManager.instance.coinsPickedUpInThisSceneCount++;
            Destroy(gameObject);
        }
    }
}
