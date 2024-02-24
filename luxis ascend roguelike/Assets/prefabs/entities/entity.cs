using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entity : MonoBehaviour
{
    public int hp, chp, mana, cmana, armor;
	
	public Animator anim;
	
	public bool moving = false;
	public virtual void move(Transform t){
		if(Vector3.Distance(transform.position,t.position) < 1.67f){
			moving = true;
			anim.SetBool("move", true);
			StartCoroutine(move2(t));
		}
	}
	
	public virtual void move(Vector3 v){
		moving = true;
		anim.SetBool("move", true);
		StartCoroutine(move2(v));
	}
	
	float movetimer = 0f;
	Vector3 startpos = Vector3.zero;
	
	public virtual IEnumerator move2(Transform t){
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
	
	public virtual IEnumerator move2(Vector3 v){
		startpos = transform.position;
		while(movetimer < 1){
			movetimer += Time.deltaTime;
			transform.position = Vector3.Lerp(startpos,v, movetimer);
			yield return new WaitForEndOfFrame();
		}
		movetimer = 0f;
		transform.position = v;
		moving = false;
	}
}
