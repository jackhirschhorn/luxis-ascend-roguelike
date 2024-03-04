using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class master : MonoBehaviour
{
    public static master MR;
	public int state = 0;//0 = move, 1 = item
	public UEvent clickevent;
	public LayerMask wallonlymask;
	public RectTransform hpscaler, manascaler;
	public TextMeshProUGUI hptxt, manatxt, goldcounter;
	
	public void FixedUpdate(){ 
		//updates the UI to display players current HP, mana, and gold
		hptxt.text =""+ player.pc.chp;
		hpscaler.anchorMin = new Vector2(hpscaler.anchorMin.x, (player.pc.hp+0f)/(player.pc.chp+0f));
		manatxt.text =""+ player.pc.cmana;
		manascaler.anchorMin = new Vector2(manascaler.anchorMin.x, (player.pc.mana+0f)/(player.pc.cmana+0f));
		goldcounter.text = ""+ player.pc.gold;
	}
	
	public void Awake(){
		MR = this;
	}
	
	public void Start(){
		for(int i = 0; i < player.pc.inventorysize; i++){
			addinvitem();
		}
	}
	
	public void tileclick(Transform t){
		//changes based on states
		if(!player.pc.moving){
			if(state == 0){
				player.pc.move(t,0);
			} else if(state == 1){
				clickevent.Invoke(t);
			}
		}
	}
	
	public void button(){
		Debug.Log("Ye");
	}
	
	public void dropgold(int i, Vector3 pos){
		//todo
	}
	
	public void droploot(Transform[] loot, Vector3 pos){
		//todo
	}
	
	public bool additem(Transform itm){
		//todo
		return false;
	}
	
	public Transform entrans;
	
	public void doenemyturn(int i){	
		StartCoroutine(doenemyturn2(i));
	}
	
	public IEnumerator doenemyturn2(int i){
		yield return new WaitForEndOfFrame();
		if(i < entrans.childCount){entrans.GetChild(i).GetComponent<enemy>().brn.doturn(i);
		} else {
			doenemyturnatk(0);
		}
	}
	
	public void doenemyturnatk(int i){
		StartCoroutine(doenemyturnatk2(i));
	}
	
	public IEnumerator doenemyturnatk2(int i){
		yield return new WaitForEndOfFrame();
		if(i < entrans.childCount){
			entrans.GetChild(i).GetComponent<enemy>().brn.doatkturn(i);
		}
	}
	
	public Transform invitempre;
	public Transform inv;
	
	public void addinvitem(){
		RectTransform clone = Instantiate(invitempre as RectTransform);
		clone.parent = inv;
		int temp = inv.childCount+1;
		for(int i = 0; i < inv.childCount; i++){
			(inv.GetChild(i) as RectTransform).anchoredPosition = new Vector2(Screen.width*((i+1f)/temp),58);
		}
		
	}
	
	public void removeinvitem(){
		
	}
}

[System.Serializable]
public class UEvent : UnityEvent<Transform>
{
}

