using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    public Animator fadeSystem;
    public static DeathZone instance;
    public bool playerIsPassAway = false;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("prob d'instance");
            return;
        }

        instance = this;

        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !playerIsPassAway)
        {
            playerIsPassAway = true;
            PlayerHealth.instance.TakeDamage(100);
        }
        else
        {
            Destroy(collider.gameObject);
        }
    }
}
