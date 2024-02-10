using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : entity
{
    public static player pc;
	public Animator anim;
	
	public bool moving = false;
	
	public void Awake(){
		pc = this;
	}
	
	public void move(Transform t){
		if(Vector3.Distance(transform.position,t.position) < 1.67f){
			moving = true;
			anim.SetBool("move", true);
			StartCoroutine(move2(t));
		}
	}
	
	float movetimer = 0f;
	Vector3 startpos = Vector3.zero;
	
	public IEnumerator move2(Transform t){
		startpos = transform.position;
		while(movetimer < 1){
			movetimer += Time.deltaTime;
			transform.position = Vector3.Lerp(startpos,t.position, movetimer);
			yield return new WaitForEndOfFrame();
		}
		movetimer = 0f;
		transform.position = t.position;
		moving = false;
	}
	
	
}
