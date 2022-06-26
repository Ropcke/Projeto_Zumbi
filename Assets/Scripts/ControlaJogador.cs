using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour {
    
    public float Velocidade = 10;
    Vector3 direcao;
    public LayerMask MascaraChao;

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

         direcao = new Vector3(eixoX, 0, eixoZ);   

        if(direcao != Vector3.zero)
        {
            GetComponent<Animator>().SetBool("Movendo", true);

        }

        else
        {
            GetComponent<Animator>().SetBool("Movendo", false);
        }
          

    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition
            (GetComponent<Rigidbody>().position + 
            (direcao * Velocidade * Time.deltaTime));

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction *100, Color.red);

        RaycastHit Impacto;

        if (Physics.Raycast(raio, out Impacto, 100, MascaraChao))
        {
            Vector3 PosicaoMiraJogador = Impacto.point - transform.position;

            PosicaoMiraJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(PosicaoMiraJogador);

            GetComponent<Rigidbody>().MoveRotation(novaRotacao);
        }
    }
}
