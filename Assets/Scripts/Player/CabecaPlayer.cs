using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabecaPlayer : MonoBehaviour
{
    private bool limiteDaCabeca;

    public bool LimiteDaCabeca{
        get{return limiteDaCabeca;}
    }

    //Detecta se o objeto ainda está colidindo
    private void OnTriggerEnter2D(Collider2D colisor){
        if(colisor.gameObject.layer == 6){
            limiteDaCabeca = true;
        }
    }

    private void OnTriggerExit2D(Collider2D colisor){
        if(colisor.gameObject.layer == 6){
            limiteDaCabeca = false;
        }
    }
}
