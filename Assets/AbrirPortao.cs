using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPortao : MonoBehaviour
{
    private bool abriuPortao = false;
    private Quaternion rotacaoAlvo;
    public float velocidade;
    public GameObject fechadura;

    void Start(){
        rotacaoAlvo = Quaternion.Euler(new Vector3(0, 90, 0));
    }

    void Update(){
        if(abriuPortao == true){
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacaoAlvo, velocidade * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D colisao){
        if(colisao.gameObject.tag.Equals("Player") && abriuPortao == false && PlayerMng.Instance.QtdChaves > 0){
            abriuPortao = true;
            fechadura.SetActive(false);
            PlayerMng.Instance.DecrementarChave();
            var boxCollider = GetComponent<BoxCollider2D>();
            Destroy(boxCollider);
        }
    }
}
