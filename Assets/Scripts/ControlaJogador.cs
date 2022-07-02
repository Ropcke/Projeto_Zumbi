using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour {
    
    public float Velocidade = 10;
    Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    public bool Vivo = true;
    private Rigidbody rigidbodyJogador;
    private Animator animatorJogador;

    private void Start()
    {
        Time.timeScale = 1;
        rigidbodyJogador = GetComponent<Rigidbody>();
        animatorJogador = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

         direcao = new Vector3(eixoX, 0, eixoZ);   

        if(direcao != Vector3.zero)
        {
            animatorJogador.SetBool("Movendo", true);

        }

        else
        {
            animatorJogador.SetBool("Movendo", false);
        }
          
        if (Vivo == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("game_zumbi_test");
            }
        }

    }

    void FixedUpdate()
    {
        rigidbodyJogador.MovePosition
            (rigidbodyJogador.position + 
            (direcao * Velocidade * Time.deltaTime));

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction *100, Color.red);

        RaycastHit Impacto;

        if (Physics.Raycast(raio, out Impacto, 100, MascaraChao))
        {
            Vector3 PosicaoMiraJogador = Impacto.point - transform.position;

            PosicaoMiraJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(PosicaoMiraJogador);

            rigidbodyJogador.MoveRotation(novaRotacao);
        }
    }
}
