using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PePlayer : MonoBehaviour
{
    private bool estaNoChao;

    public bool EstaNoChao{
        get{return estaNoChao;}
    }

    //Detecta se o objeto ainda está colidindo
    private void OnTriggerStay2D(Collider2D colisor){
        if(colisor.gameObject.layer == 6){
            estaNoChao = true;
        }
    }

    private void OnTriggerExit2D(Collider2D colisor){
        if(colisor.gameObject.layer == 6){
            estaNoChao = false;
        }
    }
}
