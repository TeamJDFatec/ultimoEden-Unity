using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YuriScript : PlayerController
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 8f;
    public float destroyTime = 2f;
    public float timeBetweenShots = 1f; // Tempo entre os tiros

    private Vector2 lastDirection = Vector2.right; // Armazena a última direção
    private bool canShoot = true; // Flag para verificar se é possível atirar

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        bc = GetComponent<BoxCollider2D>();

        tr = GetComponent<TrailRenderer>();

        rb.gravityScale = 4f;

        speed = new Vector3(8f, 0, 0);

        jumpForce = 18f;

        isGrounded = false;
    }

    void Update()
    {

        Jump();
        Run();
        Dash();

        // Verifica se o botão de tiro (Fire1) foi pressionado e se pode atirar
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            // Atira na última direção registrada
            Shoot(lastDirection);

            // Define a flag para impedir tiros consecutivos imediatos
            canShoot = false;

            // Inicia a contagem regressiva para permitir o próximo tiro
            Invoke("EnableShooting", timeBetweenShots);
        }

        // Detecta a direção do movimento do jogador
        float horizontalInput = Input.GetAxis("Horizontal");

        // Atualiza a última direção quando o jogador se move
        if (horizontalInput > 0)
        {
            lastDirection = Vector2.right;
        }
        else if (horizontalInput < 0)
        {
            lastDirection = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.UpArrow)) // Adiciona verificação para a seta para cima
        {
            lastDirection = Vector2.up;
        }
    }

    void Shoot(Vector2 direction)
    {
        // Instancia a bala na posição do firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Ajusta a rotação da bala para a direção correta
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Obtém o componente Rigidbody2D da bala
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Define a velocidade da bala
        rb.velocity = direction * bulletSpeed;

        // Destroi a bala após o tempo especificado
        Destroy(bullet, destroyTime);
    }

    void EnableShooting()
    {
        // Permite atirar novamente
        canShoot = true;
    }

}
