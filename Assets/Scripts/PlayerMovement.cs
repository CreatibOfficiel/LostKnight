using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D playerRB; // Propriété qui tiendra en réféence le rigid body de notre player
    SpriteRenderer playerRenderer; // Propriété qui tiendra la réféence du sprite rendered de notre player
    public Animator playerAnim; // Propriete qui tiendra la reference a notre composant animator
    public CapsuleCollider2D playerCollider;

    public float moveSpeed;
    public float jumpForce;
    public float groundCheckRadius;

    bool canMove = true; // Valeur qui traque si l'utilisateur peut bouger
    bool facingRight = true; // Par défaut, notre player regarde à droite
    public bool isGrounded;

    public LayerMask collisionLayers;
    public Transform groundCheck;

    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance PlayerMovement dans la scène");
            return;
        }
        instance = this;
    }

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>(); // On utilise GetComponent car notre Rb se situe au sein du même objet
        playerRenderer = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
        collisionLayers = 1 << 9;
        // Récupérer le component sprite renderer en dessous de cette ligne
    }
    // Update is called once per frame
    void Update()
    {
        if (canMove && isGrounded && Input.GetAxis("Jump") > 0)
        { // On verifie si l'utilisateur est au sol, et si l'input jump est en appui
            playerAnim.SetBool("IsGrounded", false); // On defini le parametre danimation IsGrounded a faux car nous nous aprettons a sauter
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0f); // On defini la velocite y a 0 pour etre sur d'avoir la mêe hauteur quelque soit le contexte
            playerRB.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // On ajoute de la force sur notre rigidbody afin de le faire s'envoler, on precise bien un forcement a impulse pour avoir toute la force d'un seul coup
            isGrounded = false; // On defini notre grounded a false pour garder en memoire l'etat du personnage
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
        playerAnim.SetFloat("VerticalVelocity", playerRB.velocity.y); // on arrete aussi l'animation en cours si on etait en train de courir
        // on utilise ici notre moteur physique pour crér un petit cercle àn endroit bien precis(position du ground check personnage)
        // On defini aussi son radius pour dessiner le cercle
        // Et on defini le layer sur lequel on veut checker l'overlap
        // Si le cercle overlap avec le ground layer, ç va nous renvoyer vrai, car le player est au sol
        // Sinon cela veut dire que le personnage est en l'air
        // Cela va nous permetre aussi de declencer la condition permettant d'appliquer la force sur notre personnage
        playerAnim.SetBool("IsGrounded", isGrounded); // On utilise donc cette information pour mettre a jour note animator
        float move = Input.GetAxis("Horizontal");
        // Rédiger ci dessous le code permettant de déerminer quand le player doit se retourner
        if (canMove)
        {
            if (move > 0 && !facingRight)
                Flip();
            else if (move < 0 && facingRight)
                Flip();
            playerRB.velocity = new Vector2(move * moveSpeed, playerRB.velocity.y); // On utilise vector 2 car nous sommes dans un contexte 2D
            playerAnim.SetFloat("Speed", Mathf.Abs(move)); // Defini une valeur pour le float MoveSpeed dans notre animator
        }
        else
        {
            playerRB.velocity = new Vector2(0, playerRB.velocity.y); // Si movement non autorise, on arrete la velocite
            playerAnim.SetFloat("Speed", 0); // on arrete aussi l'animation en cours si on etait en train de courir
        }
        /*playerRB.velocity = new Vector2(move * maxSpeed, playerRB.velocity.y); // On utilise vector 2 car nous sommes dans un contexte 2D
        playerAnim.SetFloat("MoveSpeed", Mathf.Abs(move)); // Defini une valeur pour le float MoveSpeed dans notre animator*/
    }
    void Flip()
    {
        facingRight = !facingRight; // On change la valeur du boolen facing right par son contraire, représentant la direction du personnage
        playerRenderer.flipX = !playerRenderer.flipX; // Même chose ici pour que notre flipx et facingRight soient en phase
    }

    public void toggleCanMove()
    {
        canMove = !canMove;
    }
}
