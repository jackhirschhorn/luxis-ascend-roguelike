using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class shrine : entity
{
	public shrine(){
		hp = 5;
		chp = 5;
		armor = 5;
	}
	
	public bool shrineopen = false;
	public GameObject UI;
	public static int totalkarmabought = 1;
	public static int totalvowsbought = 1;
	public int goldcontained = 0;
	public bool locked = false;
	public TextMeshProUGUI gold1,gold2,gold3;
	
	public void Awake(){
		goldcontained = Random.Range(25,100);
	}
	
	public void openshrine(){
		if(Vector3.Distance(transform.position,player.pc.transform.position) < 1.5f && !locked){
			shrineopen = !shrineopen;
			refreshUI();
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
	
	public void refreshUI(){
		gold1.text = (shrine.totalkarmabought*shrine.totalkarmabought*25)+"";
		gold2.text = (shrine.totalvowsbought*shrine.totalvowsbought*100)+"";
		gold3.text = goldcontained+"";
	}
	
	public void buykarma(){
		if(!locked){
			if(player.pc.gold >= shrine.totalkarmabought*shrine.totalkarmabought*25){
				player.pc.gold -= shrine.totalkarmabought*shrine.totalkarmabought*25;
				Debug.Log(player.pc.gold);
				goldcontained += shrine.totalkarmabought*shrine.totalkarmabought*25;
				shrine.totalkarmabought++;
				master.MR.basekarma++;
				master.MR.showkarma();
				refreshUI();
			} else {
				Debug.Log("can't afford");
			}
		}
	}
	
	public void buyvow(){
		if(!locked){
			if(player.pc.gold >= shrine.totalvowsbought*shrine.totalvowsbought*100){
				player.pc.gold -= shrine.totalvowsbought*shrine.totalvowsbought*100;
				Debug.Log(player.pc.gold);
				goldcontained += shrine.totalvowsbought*shrine.totalvowsbought*100;
				shrine.totalvowsbought++;
				karmachoose.kc.makenewchoice();
				refreshUI();
				onmove_player();
			} else {
				Debug.Log("can't afford");
			}
		}
	}
	
	public void stealfrom(){
		player.pc.gold += goldcontained;
		goldcontained = 0;
		locked = true;
		master.MR.basekarma -= 3;
		master.MR.showkarma();
		refreshUI();
		onmove_player();
	}
}