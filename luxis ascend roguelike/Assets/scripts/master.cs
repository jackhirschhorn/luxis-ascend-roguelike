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
	
	public void Update(){
		if(Input.GetKeyDown(KeyCode.Space))addinvitem();
	}
	
	public void Awake(){
		MR = this;
	}
	
	public void Start(){
		for(int i = 0; i < player.pc.inventorysize; i++){
			addinvitem(true);
		}
	}
	
	public void tileclick(Transform t){
		//changes based on states
		if(!player.pc.moving){
			if(state == 0){
				player.pc.move(t,0);
				itmindx = 0;
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
		clone.anchoredPosition = new Vector2(Screen.width*((temp+1f)/temp),58);
		StartCoroutine(animateinv(clone,new Vector2(Screen.width*((temp-1f)/temp),58),0));
		for(int i = inv.childCount-2; i > -1; i--){
			StartCoroutine(animateinv((inv.GetChild(i) as RectTransform),new Vector2(Screen.width*((i+1f)/temp),58),inv.childCount-i+5));
			//(inv.GetChild(i) as RectTransform).anchoredPosition = new Vector2(Screen.width*((i+1f)/temp),58);
		}
		
	}
	
	public void addinvitem(bool b){
		RectTransform clone = Instantiate(invitempre as RectTransform);
		clone.parent = inv;
		int temp = inv.childCount+1;
		for(int i = inv.childCount-1; i > -1; i--){
			//StartCoroutine(animateinv((inv.GetChild(i) as RectTransform),new Vector2(Screen.width*((i+1f)/temp),58),inv.childCount-i+5));
			(inv.GetChild(i) as RectTransform).anchoredPosition = new Vector2(Screen.width*((i+1f)/temp),58);
		}
		
	}
	
	public IEnumerator animateinv(RectTransform rt, Vector2 v, int i){
		yield return new WaitForSeconds(i*0.02f);
		Vector2 temp = rt.anchoredPosition;
		Vector2 rbr = Vector2.LerpUnclamped(temp,v,1.1f); //rubber band the animation slightly
		float timer = 0;
		while(timer <= 1){
			rt.anchoredPosition = Vector2.Lerp(temp,rbr,timer);
			yield return new WaitForEndOfFrame();
			timer += Time.deltaTime*3;			
		}
		timer = 0;
		while (timer <= 1){
			rt.anchoredPosition = Vector2.Lerp(rbr,v,timer);
			yield return new WaitForEndOfFrame();
			timer += Time.deltaTime*5;		
		}
		rt.anchoredPosition = v;
	}
	
	public void removeinvitem(){
		
	}
	
	public Transform worlditemholder;
	public int itmindx = 0;
	
	public void showitem(Transform itm, Transform itment){
		itm.parent = worlditemholder;
		if(itmindx == 0){
			itm.gameObject.SetActive(true);
			itm.GetComponent<item>().visindx = itmindx;
			Debug.Log(Camera.main.ScreenToWorldPoint(itment.position));
			(itm as RectTransform).anchoredPosition = (Vector2)Camera.main.WorldToScreenPoint(itment.position);
		} else {
			
		}
		itmindx++;		
	}
	
	public void hideitem(Transform itm, Transform itment){
		itm.parent = itment.parent;
		itm.gameObject.SetActive(false);
	}
}

[System.Serializable]
public class UEvent : UnityEvent<Transform>
{
}

