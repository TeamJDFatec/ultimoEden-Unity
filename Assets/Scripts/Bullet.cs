using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        CausarDanoAoInimigo();
    }

    public int dano = 10;

    // Método chamado quando a munição colide com o inimigo
    public void CausarDanoAoInimigo()
    {
        // Verifica se o objeto atingido tem um script de inimigo
        EnemyController inimigo = GetComponent<EnemyController>();

        // Se encontrou o script do inimigo, causa dano
        if (inimigo != null)
        {
            inimigo.SofrerDano(dano);
        }
    }
}
