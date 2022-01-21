using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Transform playerSpawn;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            CurrentSceneManager.instance.respawnPoint = transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
