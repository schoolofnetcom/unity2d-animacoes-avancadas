using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavaleiro : MonoBehaviour {
    
    private Rigidbody2D rb2d;
    private bool viradoParaDireita;
	public float velocidade = 10;
    private bool attack;
	private Animator anim;

	private bool noChao;

	public float alturaPulo = 250f;

	// Use this for initialization
	void Start () {
		viradoParaDireita = true;
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	     float horizontalAxis = Input.GetAxis("Horizontal");
         movimentacao(horizontalAxis);
         virar(horizontalAxis);
		 atacando();
		 Resetando();
		 pular();
	}

	void Update(){
		teclado();
	}

	void movimentacao(float horizontal){
		if(!this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Atacando")){
               
            	rb2d.velocity = new Vector2(horizontal*velocidade,rb2d.velocity.y);
                Debug.Log(horizontal);
		        anim.SetFloat("velocidade", Mathf.Abs(horizontal));

		}
	}

	void virar(float horizontal){
	   
	   if(horizontal > 0 && !viradoParaDireita || horizontal < 0 && viradoParaDireita){
          
		   viradoParaDireita = !viradoParaDireita;
		   Vector3 tamanhoPerson = transform.localScale;
		   tamanhoPerson.x *= -1;
		   transform.localScale = tamanhoPerson;
          
	   }
       

	}

	void atacando(){

       if(attack && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Atacando")){
		   anim.SetTrigger("atacando");
		   rb2d.velocity = Vector2.zero;
	   }

	}

	void teclado(){
		if(Input.GetKeyDown(KeyCode.B))
		{
			attack = true;
		}
	}

	void Resetando(){
		attack = false;
	}

    void OnCollisionEnter2D(Collision2D obj){
       
        noChao = true;

	}

	void OnCollisionStay2D(Collision2D obj){
      
         noChao = true;

	}

	void OnCollisionExit2D(Collision2D obj){
       
	    noChao = false;

	}

	void pular(){

         if(Input.GetKeyDown(KeyCode.Space) && noChao){
			 rb2d.AddForce(Vector2.up*alturaPulo);
			 anim.SetTrigger("saltando");
		 }

	}

}
