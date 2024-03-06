using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="testrangedatk")]
public class testrangedatk : attack
{
    public override bool atkcheck(entity targ, entity me){
		if(Vector3.Distance(me.transform.position,player.pc.transform.position) < 2.9f && Vector3.Distance(me.transform.position,player.pc.transform.position) >1.5f) //are we enough?
		{ // are we far enough?
			if(!Physics.Raycast(me.transform.position+new Vector3(0,0.1f,0), targ.transform.position-me.transform.position, 2.9f, master.MR.wallonlymask)){//is there not a wall between us?
				return true;
			}
		}
		return false;
	}
	
	public Transform vis;
	public List<Transform> visuse = new List<Transform>();// Is list instead of just one thingy
	public override void demoatk(entity targ, entity me){ 
		RaycastHit hit; //the info from raycats(:3) can be called with "hit"
		Physics.Raycast(me.transform.position+new Vector3(0,0.1f,0), targ.transform.position-me.transform.position, out hit, 10, master.MR.wallonlymask);//where is the wall past the player
		// step through each tile between "me" and "hit.point" and create a warning there
		for(int i = 1; i < hit.distance; i++){
			Transform tempvisuse = Instantiate(vis); //the demo attack's position
			Vector3 tempv = (me.transform.position+((hit.point-me.transform.position).normalized*i));
			tempvisuse.position = new Vector3(Mathf.FloorToInt(tempv.x+0.5f),0.1f,Mathf.FloorToInt(tempv.z+0.5f));//Space it sends to
			visuse.Add(tempvisuse); //temporarily adds the thingies
		}
		
	}
	
	public override IEnumerator doatk2(Vector3 targ, entity me, int indx){
		for(int i = 0; i < visuse.Count; i++){
			foreach(Transform t2 in master.MR.entrans){
				if(Vector3.Distance(visuse[i].position, t2.position) <= 0.1f){
					t2.GetComponent<entity>().takedamage(1,0);
					i = visuse.Count;
				}
			}
			if(i < visuse.Count && Vector3.Distance(visuse[i].position, player.pc.transform.position) <= 0.1f){
				i = visuse.Count;
				player.pc.takedamage(1,0);
			}
		}
		for (int i = visuse.Count - 1; i >= 0; i--){
			Destroy(visuse[i].gameObject);
		}
		visuse.Clear();
		//Destroy(visuse.gameObject);
		me.anim.SetBool("attack",true);
		yield return new WaitUntil(() => !me.anim.GetBool("attack"));
		master.MR.doenemyturnatk(indx+1);
	}
	
	public override void clearattack(){
		for (int i = visuse.Count - 1; i >= 0; i--){
			if(visuse[i]!= null)Destroy(visuse[i].gameObject);
		}
	}
}
