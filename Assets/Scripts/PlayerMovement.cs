using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidade = 5f;
    public float forcaPulo = 15f;
    public bool estaPulando = false;
    public float movimento;
    private Rigidbody2D rb;
    private float escalaInicial;
    public int playerLife = 3; // Número inicial de vidas do jogador
    public float knockbackForce; // Força do impulso ao encostar no monstro
    public GameObject gameOver;
    public bool isGameOver;
    private float dashVelocity = 15f;
    private float dashDuration = .7f;
    private bool podeDash = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        escalaInicial = transform.localScale.x;
    }

    private void Update()
    {
        Move();

        Pular();

        verificaGameOver();

        Dash();
    }

    void Move()
    {
        movimento = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(movimento * velocidade, rb.velocity.y);
        if (movimento > 0)
        {
            transform.localScale = new Vector2(escalaInicial, transform.localScale.y);
        }
        else if (movimento < 0)
        {
           transform.localScale = new Vector2(-escalaInicial, transform.localScale.y);
        }

        // Verificar se o jogador quer pular
    }

    private void Pular()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (estaPulando == false)
            {
                estaPulando = true;
                rb.velocity = Vector2.up * forcaPulo; 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "chao")
        {
            estaPulando = false;
        }

        if (collision.gameObject.CompareTag("inimigo"))
        {
            Vector2 knockbackDirection = transform.position + (transform.position - collision.transform.position).normalized * 2f;
            print(knockbackDirection);

            rb.MovePosition(Vector2.MoveTowards(transform.position, knockbackDirection, 5f));
            // Remova uma vida do jogador
            playerLife--;

            // Verifique se o jogador ficou sem vidas
            
        }
    }

    private void verificaGameOver()
    {
        if (playerLife <= 0)
        {
            Time.timeScale = 0;
            gameOver.SetActive(true);

        }
    }

        void CreateLife()
    {
        // Adiciona uma vida ao jogador
        playerLife++;

        // Lógica adicional para atualizar a interface do usuário, se necessário
    }

    public void TirarVida(int dano)
    {
        // Lógica para tirar vida do jogador
        playerLife -= dano;

        // Verifica se o jogador ainda tem vidas restantes
        if (playerLife <= 0)
        {
            verificaGameOver();
        }
    }

   public void Dash()
   {
        if (Input.GetButtonDown("Fire3") && podeDash)
        {
            podeDash = false;
            rb.velocity = new Vector2(movimento, rb.velocity.y);
            if (movimento > 0)
            {
                transform.localScale = new Vector2(movimento * dashVelocity, transform.localScale.y);
            }
            if(movimento < 0)
            {
                transform.localScale = new Vector2(-movimento * dashVelocity, transform.localScale.y);
            }
            StartCoroutine(dashTempo());
            Debug.Log("DASH");
        }
   }

   public IEnumerator dashTempo()
   {
        yield return new WaitForSeconds(dashDuration);
        podeDash = true;
   }
}





















