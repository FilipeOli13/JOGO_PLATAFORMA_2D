using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMng : MonoBehaviour
{
    public static PlayerMng Instance;
    public static FlipCorpoPlayer flipCorpoPlayer;
    public static AnimacaoPlayer animacaoPlayer;
    public static PePlayer pePlayer;
    public static CabecaPlayer cabecaPlayer;
    public static DireitaPlayer direitaPlayer;
    public static EsquerdaPlayer esquerdaPlayer;
    public static MovimentarPlayer movimentarPlayer;
    public static PlayerDano playerDano;
    public static Rigidbody2D rigidbody2D;

    void Awake(){
        if(Instance == null){
            flipCorpoPlayer = GetComponentInChildren<FlipCorpoPlayer>();
            animacaoPlayer = GetComponentInChildren<AnimacaoPlayer>();
            pePlayer = GetComponentInChildren<PePlayer>();
            cabecaPlayer = GetComponentInChildren<CabecaPlayer>();
            direitaPlayer = GetComponentInChildren<DireitaPlayer>();
            esquerdaPlayer = GetComponentInChildren<EsquerdaPlayer>();
            movimentarPlayer = GetComponent<MovimentarPlayer>();
            playerDano = GetComponent<PlayerDano>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public bool movimentacaoHabilitada;
    public int qtdChaves;

    public int QtdChaves{
        get{return qtdChaves;}
    }

    void Start(){
        movimentacaoHabilitada = false;
        qtdChaves = 0;
    }

    public void ResetarVelocidadeDaFisica(){
        rigidbody2D.velocity = Vector3.zero;
    }

    public void ArremesarPlayer(int x, int y){
        rigidbody2D.AddForce(new Vector2(x,y));
    }

    public void RemoverSimulacaoDaFisica(){
        ResetarVelocidadeDaFisica();
        rigidbody2D.simulated = false;
    }

    public void HabilitarMovimentacao(){
        movimentacaoHabilitada = true;
    }

    public void DesabilitaMovimentacao(){
        movimentacaoHabilitada = false;
    }

    public void CongelarPlayer(){
        DesabilitaMovimentacao();
        animacaoPlayer.PlayIdle();
    }

    public void ExpelirPlayer(){
        var numeroSorteado = new System.Random().Next(0,2);
        var x = numeroSorteado == 0 ? -1000 : 1000;
        ResetarVelocidadeDaFisica();
        ArremesarPlayer(x, 1000);
    }

    public void IncrementarChave(){
        qtdChaves++;
    }

    public void DecrementarChave(){
        qtdChaves--;
    }
}
