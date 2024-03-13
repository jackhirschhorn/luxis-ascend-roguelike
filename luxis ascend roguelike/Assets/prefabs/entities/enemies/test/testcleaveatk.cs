using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="testcleaveatk")]
public class testcleaveatk : attack
{
    public override bool atkcheck(entity targ, entity me){
		if(Vector3.Distance(me.transform.position,player.pc.transform.position) < 1.5f){ //are we close enough?
			if(!Physics.Raycast(me.transform.position+new Vector3(0,0.1f,0), player.pc.transform.position-me.transform.position, 1.5f, master.MR.wallonlymask)){//is there not a wall between us?
				return true;
			}
		}
		return false;
	}
	
	public Transform vis;
    public List<Transform> visuse = new List<Transform>();// Is list instead of just one thingy
    public override void demoatk(entity targ, entity me){ 
        Transform tempvisuse = Instantiate(vis); //the demo attack's position
		tempvisuse.position = targ.transform.position+new Vector3(0,0.1f,0);// put the demo attack on the target
        visuse.Add(tempvisuse); //temporarily adds the thingies
        if(targ.transform.position.x == me.transform.position.x){
			tempvisuse = Instantiate(vis);
            tempvisuse.position = targ.transform.position+new Vector3(1,0.1f,0);
			visuse.Add(tempvisuse);
			tempvisuse = Instantiate(vis);
			tempvisuse.position = targ.transform.position+new Vector3(-1,0.1f,0);
			visuse.Add(tempvisuse);
        }
		else if(targ.transform.position.z == me.transform.position.z){
			tempvisuse = Instantiate(vis);
			tempvisuse.position = targ.transform.position+new Vector3(0,0.1f,1);
			visuse.Add(tempvisuse);
			tempvisuse = Instantiate(vis);
			tempvisuse.position = targ.transform.position+new Vector3(0,0.1f,-1);
			visuse.Add(tempvisuse);
		}
		else{
			tempvisuse = Instantiate(vis);
			tempvisuse.position = new Vector3(me.transform.position.x,0.1f,targ.transform.position.z);
			visuse.Add(tempvisuse);
			tempvisuse = Instantiate(vis);
			tempvisuse.position = new Vector3(targ.transform.position.x,0.1f,me.transform.position.z);
			visuse.Add(tempvisuse);
		}
    }

		
	
	public override IEnumerator doatk2(Vector3 targ, entity me, int indx){
		for(int i = 0; i < visuse.Count; i++){
			foreach(Transform t2 in master.MR.entrans){
				if(i < visuse.Count && Vector3.Distance(visuse[i].position, t2.position) <= 0.1f){
					t2.GetComponent<entity>().takedamage(1,0);
				}
			}
			if(i < visuse.Count && Vector3.Distance(visuse[i].position, player.pc.transform.position) <= 0.1f){
				player.pc.takedamage(1,0);
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
