using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entity : MonoBehaviour
{
    public int hp, chp, mana, cmana, armor;
	
	
	public Animator anim;
	
	public bool moving = false;
	public virtual void move(Transform t, int indx){
		if(Vector3.Distance(transform.position,t.position) < 1.67f){
			moving = true;
			anim.SetBool("move", true);
			StartCoroutine(move2(t,indx));
		}
	}
	
	public virtual void move(Vector3 v, int indx){
		moving = true;
		anim.SetBool("move", true);
		StartCoroutine(move2(v,indx));
	}
	
	float movetimer = 0f;
	Vector3 startpos = Vector3.zero;
	
	public virtual IEnumerator move2(Transform t, int indx){
		startpos = transform.position;
		while(movetimer < 1){
			movetimer += Time.deltaTime*3f;
			transform.position = Vector3.Lerp(startpos,t.position, movetimer);
			yield return new WaitForEndOfFrame();
		}
		movetimer = 0f;
		transform.position = t.position;
		moving = false;		
		if(player.pc == this){master.MR.doenemyturn(0);
		} else {			
			master.MR.doenemyturn(indx+1);
		}
	}
	
	public virtual IEnumerator move2(Vector3 v, int indx){
		startpos = transform.position;
		while(movetimer < 1){
			movetimer += Time.deltaTime*3f;
			transform.position = Vector3.Lerp(startpos,v, movetimer);
			yield return new WaitForEndOfFrame();
		}
		movetimer = 0f;
		transform.position = v;
		moving = false;
		if(player.pc == this){master.MR.doenemyturn(0);
		} else {			
			master.MR.doenemyturn(indx+1);
		}
	}
	
	
	public virtual void takedamage(int dam, int peirce){
		hp -= Mathf.Max(dam-(Mathf.Max(armor-peirce,0)),0);
		if(hp <= 0)die();
	}
	
	public virtual void die(){
		Destroy(this.gameObject);
	}
}
