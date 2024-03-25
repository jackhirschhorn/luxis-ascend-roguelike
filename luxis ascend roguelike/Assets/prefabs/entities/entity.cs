using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entity : MonoBehaviour
{
    public int hp, chp, mana, cmana, armor;
	public bool dead = false;
	
	
	public Animator anim;
	
	public bool moving = false;
	public virtual void move(Transform t, int indx){ //player move
		if(Vector3.Distance(transform.position,t.position) < 1.67f){
			Collider[] cols = Physics.OverlapSphere(t.position, 0.25f, master.MR.entitymask);
			if(cols.Length == 0 || cols[0].transform.parent.parent == this.transform){
				if(Vector3.Distance(t.position,transform.position) > 0.1f)master.MR.hideallitems();
				moving = true;
				anim.SetBool("move", true);
				StartCoroutine(move2(t,indx)); //move over time
			}
		}
	}
	
	public virtual void move(Vector3 v, int indx){ //npc move
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
		if(player.pc == this){master.MR.doenemyturn(0); //do enemy turns
		} else {			
			master.MR.doenemyturn(indx+1); //do next enemy turn in sequence
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
		if(player.pc == this){master.MR.doenemyturn(0); //do enemy turns
		} else {			
			master.MR.doenemyturn(indx+1); //do next enemy turn in sequence
		}
	}
	
	public void heal(int whatstat, int howmuch){
		switch(whatstat){
			case 0:
				chp = Mathf.Min(howmuch + chp,hp);//heals
				break;
			case 1:
				cmana = Mathf.Min(howmuch + cmana,mana);//mana
				break;
		}
	}
	
	public virtual void takedamage(int dam, int peirce){
		chp -= Mathf.Max(dam-(Mathf.Max(armor-peirce,0)),0);
		if(chp <= 0)die();
	}
	
	public virtual void die(){//need to do this all at once during the cleanup step
		Destroy(this.gameObject);
	}
}
