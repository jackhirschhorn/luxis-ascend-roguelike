using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class karmachoose : MonoBehaviour
{
	public Transform banes, boons;
	public vow[] baneSOs = new vow[3];
	public vow[] boonSOs = new vow[3];
	public bool chose1 = false;
	public static karmachoose kc;
	
	public void Awake(){
		kc = this;
		gameObject.SetActive(false);
	}
	
    public void makenewchoice(){
		int rand = Random.Range(0,2);
		if(player.pc.karma >= 6){//best karma
			generatechoices(3,3,3,-1,-1,-1);
		} else if(player.pc.karma == 5){
			if(rand == 0){
				generatechoices(3,3,3,-1,-1,-2);
			} else {
				generatechoices(2,3,3,-1,-1,-1);
			}
		} else if (player.pc.karma == 4){
			generatechoices(2,3,3,-1,-1,-2);
		} else if(player.pc.karma == 3){
			if(rand == 0){
				generatechoices(2,2,3,-1,-1,-2);
			} else {
				generatechoices(2,3,3,-1,-2,-2);
			}
		} else if (player.pc.karma == 2){
			generatechoices(2,2,3,-1,-2,-2);
		} else if(player.pc.karma == 1){
			if(rand == 0){
				generatechoices(1,2,3,-1,-2,-2);
			} else {
				generatechoices(2,2,3,-1,-2,-3);
			}
		} else if (player.pc.karma == 0){
			generatechoices(1,2,3,-1,-2,-3);
		} else if(player.pc.karma == -1){
			if(rand == 0){
				generatechoices(1,2,2,-1,-2,-3);
			} else {
				generatechoices(1,2,3,-2,-2,-3);
			}
		} else if (player.pc.karma == -2){
			generatechoices(1,2,2,-2,-2,-3);
		} else if(player.pc.karma == -3){
			if(rand == 0){
				generatechoices(1,1,2,-2,-2,-3);
			} else {
				generatechoices(1,2,2,-2,-3,-3);
			}
		} else if (player.pc.karma == -4){
			generatechoices(1,1,2,-2,-3,-3);
		} else if(player.pc.karma == -5){
			if(rand == 0){
				generatechoices(1,1,2,-3,-3,-3);
			} else {
				generatechoices(1,1,1,-2,-3,-3);
			}
		} else if (player.pc.karma <= -6){
			generatechoices(1,1,1,-3,-3,-3);
		}
		gameObject.SetActive(true);
		foreach(Transform t in banes){
			t.gameObject.SetActive(true);
		}
		foreach(Transform t in boons){
			t.gameObject.SetActive(true);
		}
	}
	
	public void generatechoices(int i,int i2,int i3,int i4,int i5,int i6){
		vowholder vh = transform.GetComponent<vowholder>();
		baneSOs[0] = (i == 1?vh.baneSO1[Random.Range(0,vh.baneSO1.Count)]:(i == 2?vh.baneSO2[Random.Range(0,vh.baneSO2.Count)]:vh.baneSO3[Random.Range(0,vh.baneSO3.Count)]));
		banes.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = baneSOs[0].description;
		banes.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + baneSOs[0].karmavalue;
		baneSOs[1] = (i2 == 1?vh.baneSO1[Random.Range(0,vh.baneSO1.Count)]:(i2 == 2?vh.baneSO2[Random.Range(0,vh.baneSO2.Count)]:vh.baneSO3[Random.Range(0,vh.baneSO3.Count)]));
		banes.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = baneSOs[1].description;
		banes.GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + baneSOs[1].karmavalue;
		baneSOs[2] = (i3 == 1?vh.baneSO1[Random.Range(0,vh.baneSO1.Count)]:(i3 == 2?vh.baneSO2[Random.Range(0,vh.baneSO2.Count)]:vh.baneSO3[Random.Range(0,vh.baneSO3.Count)]));
		banes.GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = baneSOs[2].description;
		banes.GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + baneSOs[2].karmavalue;
		
		boonSOs[0] = (i4 == -1?vh.boonSO1[Random.Range(0,vh.boonSO1.Count)]:(i4 == -2?vh.boonSO2[Random.Range(0,vh.boonSO2.Count)]:vh.boonSO3[Random.Range(0,vh.boonSO3.Count)]));
		boons.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = boonSOs[0].description;
		boons.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + boonSOs[0].karmavalue;
		boonSOs[1] = (i5 == -1?vh.boonSO1[Random.Range(0,vh.boonSO1.Count)]:(i5 == -2?vh.boonSO2[Random.Range(0,vh.boonSO2.Count)]:vh.boonSO3[Random.Range(0,vh.boonSO3.Count)]));
		boons.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = boonSOs[1].description;
		boons.GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + boonSOs[1].karmavalue;
		boonSOs[2] = (i6 == -1?vh.boonSO1[Random.Range(0,vh.boonSO1.Count)]:(i6 == -2?vh.boonSO2[Random.Range(0,vh.boonSO2.Count)]:vh.boonSO3[Random.Range(0,vh.boonSO3.Count)]));
		boons.GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = boonSOs[2].description;
		boons.GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + boonSOs[2].karmavalue;
		
	}
	
	public void choosebane(int i){
		for(int i2 = 0; i2 < banes.childCount; i2++){
			if(i != i2)banes.GetChild(i2).gameObject.SetActive(false);
		}
		master.MR.addvow(baneSOs[i]);
		chose1 = !chose1;
		if(!chose1)gameObject.SetActive(false);
	}
	
	public void chooseboon(int i){
		for(int i2 = 0; i2 < boons.childCount; i2++){
			if(i != i2)boons.GetChild(i2).gameObject.SetActive(false);
		}
		master.MR.addvow(boonSOs[i]);
		chose1 = !chose1;
		if(!chose1)gameObject.SetActive(false);
	}
}
