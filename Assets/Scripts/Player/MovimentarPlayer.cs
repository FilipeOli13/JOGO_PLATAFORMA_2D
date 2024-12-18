using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarPlayer : MonoBehaviour
{
    //Variável de velocidade de movimento
    public float velocidade;

    //Variáveis para dar força ao jogador para pular
    public float forcaDoPuloY = 1.5f;
    public float forcaDoPuloX = 0;

    //Variável para verificar se o jogador está pulando
    public bool estaPulando;

    //Variável para habilitar o pulo duplo
    public bool puloDuplo;

    //Variável de Coroutine
    private Coroutine coroutinePulo;    

    // Update is called once per frame
    void Update()
    {
        if(PlayerMng.Instance.movimentacaoHabilitada == false) return;
        
        Movimentar();
        Pular();
        PularDaParede();
    }

    //Método para pular o jogador
    private void Pular(){
        //Verificar se o jogador clicou na tecla para pular
        if(Input.GetButtonDown("Jump")){
            //Verificar se o jogador está no chão
            if(PlayerMng.pePlayer.EstaNoChao == true){
                //Ativa o pulo
                PlayerMng.animacaoPlayer.PlayJump();
                estaPulando = true;
                puloDuplo = true;
                AtivarCoroutinePulo();
            }else{
                //Posso fazer um pulo duplo
                if(puloDuplo == true){
                    PlayerMng.animacaoPlayer.PlayDoubleJump();
                    estaPulando = true;
                    puloDuplo = false;
                    AtivarCoroutinePulo();
                }
            }
        }

        //Verifica se está habilitado a pular
        if(estaPulando == true){
            //Verifica se a cabeça do jogador está livre
            if(PlayerMng.cabecaPlayer.LimiteDaCabeca == false){
                //Fazer o jogar subir para simular o pulo
                PlayerMng.rigidbody2D.velocity = Vector3.zero;
                PlayerMng.rigidbody2D.gravityScale = 0;
                Vector3 direcaoPulo = new Vector3(forcaDoPuloX, forcaDoPuloY, 0);
                transform.position += direcaoPulo * Time.deltaTime * velocidade;
            }
        }else{
            PlayerMng.rigidbody2D.gravityScale = 4;
        }
    }

    //Ativa a coroutina para finalizar o pulo
    private void AtivarCoroutinePulo(){
        if(coroutinePulo != null){
            StopCoroutine(coroutinePulo);
        }
        coroutinePulo = StartCoroutine(TempoPulo());
    }

    //Método para inverter o valor do está pulando através de um tempo
    private IEnumerator TempoPulo(){
        yield return new WaitForSeconds(0.3f);
        forcaDoPuloX = 0;
        estaPulando = false;
    }

    //Método para movimentar o jogador
    private void Movimentar(){
        //Obter uma entrada do usuário, um teclado de início.
        float eixoX = Input.GetAxis("Horizontal");

        if(eixoX > 0 && PlayerMng.direitaPlayer.LimiteDireita == true) {eixoX = 0;}
        else if(eixoX < 0 && PlayerMng.esquerdaPlayer.LimiteEsquerda == true) {eixoX = 0;}

        //Verificar qual direção o player vai andar
        if(eixoX > 0){
            PlayerMng.flipCorpoPlayer.OlharDireita();
        }else if(eixoX < 0){
            PlayerMng.flipCorpoPlayer.OlharEsquerda();
        }

        //Verificar se o player está no chão
        if(PlayerMng.pePlayer.EstaNoChao == true){
            //Verificar se o player está se movendo
            if(eixoX != 0){
                PlayerMng.animacaoPlayer.PlayRun();
            }else{
                PlayerMng.animacaoPlayer.PlayIdle();
            }
        }else{
            //Ativa animação de queda
            PlayerMng.animacaoPlayer.PlayFall();
        }

        //Declarar uma variável para armazenar a direção do movimento do jogador
        Vector3 direcaoMovimento = new Vector3(eixoX, 0, 0);

        //Mover o objeto
        transform.position += direcaoMovimento * Time.deltaTime * velocidade;
    }

    private void PularDaParede(){
        //Verificar se o jogador não está no chão e se ele está em uma das paredes
        if(PlayerMng.pePlayer.EstaNoChao == false && (PlayerMng.direitaPlayer.LimiteDireita == true || PlayerMng.esquerdaPlayer.LimiteEsquerda == true)){
            //Animação do pulo da parede
            PlayerMng.animacaoPlayer.PlayWallSlider();

            //Verificar se o jogador clicou em pular
            if(Input.GetButtonDown("Jump")){
                forcaDoPuloX = PlayerMng.flipCorpoPlayer.VisaoEsquerdaOuDireita == true ? forcaDoPuloY : forcaDoPuloY * -1;
                PlayerMng.animacaoPlayer.PlayJump();
                puloDuplo = true;
                estaPulando = true;
                AtivarCoroutinePulo();
            }
        }
    }
}
