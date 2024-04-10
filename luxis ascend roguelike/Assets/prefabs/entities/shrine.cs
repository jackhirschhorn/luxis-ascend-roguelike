using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrine : entity
{
	public shrine(){
		hp = 5;
		chp = 5;
		armor = 5;
	}
	
	public bool shrineopen = false;
	public GameObject UI;
	
	public void openshrine(){
		if(Vector3.Distance(transform.position,player.pc.transform.position) < 1.5f){
			shrineopen = !shrineopen;
			UI.SetActive(shrineopen);
			StartCoroutine(schmove());
			
		}
	}
	
	public IEnumerator schmove(){
		Vector3 startpos = Camera.main.transform.parent.position;
		Vector3 endpos = new Vector3((shrineopen?-2:0),0,0);
		bool b = shrineopen;
		float timer = 0;
		while(timer < 1){			
			Camera.main.transform.parent.position = Vector3.Lerp(startpos,endpos,timer);
			timer += Time.deltaTime*6;
			yield return new WaitForEndOfFrame();
			if(shrineopen != b)yield break;
		}
		Camera.main.transform.parent.position = endpos;
	}
	
	public void onmove_player(){
		shrineopen = false;
		UI.SetActive(false);
		StartCoroutine(schmove());
	}
	
	public void buykarma(){
		Debug.Log("karma +1");
	}
	
	public void buyvow(){
		Debug.Log("vow +1");
	}
	
	public void stealfrom(){
		Debug.Log("evil +1");
	}
}