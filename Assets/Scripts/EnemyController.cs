using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float velocidade = 3f;
    public int vidaMaxima = 50;
    private int vidaAtual;
    public int danoDeAtaque = 10;
    private Transform jogador;
    private Rigidbody2D rb;
    public float distanciaDeAtaque = 5f;

    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("player").transform;
        rb = GetComponent<Rigidbody2D>();
        vidaAtual = vidaMaxima;
    }

    void Update()
    {
        // Move apenas no eixo X em direção ao jogador
        Vector2 direcao = new Vector2(jogador.position.x - transform.position.x, 0);
        rb.velocity = direcao.normalized * velocidade;

        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        rb.rotation = Quaternion.Euler(new Vector3(0, 0, angulo - 90f)).z;

        

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("munição"))
        {
            // Supondo que o prefab "munição" tenha um script que lida com dano
            int dano = other.GetComponent<Bullet>().dano;
            SofrerDano(dano);

            // Destruir o prefab de munição
            Destroy(other.gameObject);
        }
    }

    public void SofrerDano(int dano)
    {
        vidaAtual -= dano;

        // Verifica se o inimigo foi derrotado
        if (vidaAtual <= 0)
        {
            Destroy(gameObject);
        }
    }

 
}


