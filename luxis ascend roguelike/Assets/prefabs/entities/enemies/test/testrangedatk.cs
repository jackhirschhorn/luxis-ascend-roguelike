using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
		List<Transform> temps = new List<Transform>();
		List<Transform> temps2 = new List<Transform>();
		foreach(Transform t in visuse){
			if(!temps.Any(f => (f != t && Vector3.Distance(t.position,f.position) <0.1f))){
				temps.Add(t);
			} else {
				temps2.Add(t);
			}
		}
		for(int i2 = temps2.Count-1; i2 >=0; i2--){
			Destroy(temps2[i2].gameObject);
		}
		visuse = temps;
	}
	
	public override IEnumerator doatk2(Vector3 targ, entity me, int indx){
		for(int i = 0; i < visuse.Count; i++){
			Collider[] cols = Physics.OverlapSphere(visuse[i].position, 0.25f, master.MR.entitymask);
			foreach(Collider c in cols){
				if(c.transform.parent.parent.GetComponent<entity>()){
					c.transform.parent.parent.GetComponent<entity>().takedamage(1,0);
				}
			}
		}
		for (int i = visuse.Count - 1; i >= 0; i--){
			Destroy(visuse[i].gameObject);
		}
		master.MR.StartCoroutine(clearit());
		//Destroy(visuse.gameObject);
		me.anim.SetBool("attack",true);
		yield return new WaitUntil(() => !me.anim.GetBool("attack"));
		master.MR.doenemyturnatk(indx+1);
	}
	
	public override void clearattack(){
		for (int i = visuse.Count - 1; i >= 0; i--){
			if(visuse[i]!= null)Destroy(visuse[i].gameObject);
			master.MR.StartCoroutine(clearit());
		}
	}
	
	public IEnumerator clearit(){
		yield return new WaitForEndOfFrame();
		visuse.Clear();
	}
}
