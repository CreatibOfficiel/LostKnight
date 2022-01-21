using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public float invincibilityTimeAfterHit = 3f;
    public float invincibilityFlashDelay = 0.15f;
    public bool isInvincible = false;

    public SpriteRenderer graphics;
    public HealthBar healthBar;
    public AudioClip hitSound;

    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance PlayerHealth dans la scène");
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }

    public void HealPlayer(int amount)
    {
        if((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
        healthBar.setHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible || DeathZone.instance.playerIsPassAway)
        {
            AudioManager.instance.PlayClipAt(hitSound, transform.position);

            int value = damage;
            switch (GameValues.difficulty)
            {
                case GameValues.Difficulties.Easy:
                    value *= 1;
                    break;
                case GameValues.Difficulties.Medium:
                    value *= 2;
                    break;
                case GameValues.Difficulties.Hard:
                    value *= 3;
                    break;
            }

            currentHealth -= value;
            healthBar.setHealth(currentHealth);

            // vérif si le joueur est tjrs vivant

            if(currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public void Die()
    {
        PlayerMovement.instance.enabled = false;
        PlayerMovement.instance.playerAnim.SetTrigger("Die");
        PlayerMovement.instance.playerRB.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.playerRB.velocity = Vector3.zero;
        PlayerMovement.instance.playerCollider.enabled = false;
        GameOverManager.instance.OnPlayerDeath();
        DeathZone.instance.fadeSystem.SetTrigger("FadeIn");
    }

    public void Respawn()
    {
        PlayerMovement.instance.enabled = true;
        DeathZone.instance.fadeSystem.SetTrigger("exit");
        PlayerMovement.instance.playerAnim.SetTrigger("Respawn");
        PlayerMovement.instance.playerRB.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled = true;
        DeathZone.instance.playerIsPassAway = false;
        currentHealth = maxHealth;
        healthBar.setHealth(currentHealth);
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvincible = false;
    }
}
