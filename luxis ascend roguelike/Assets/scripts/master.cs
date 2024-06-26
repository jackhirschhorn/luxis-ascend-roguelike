using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class master : MonoBehaviour
{
    public static master MR;
	public int state = 0;//0 = move, 1 = item
	public UEvent clickevent;
	public item itseld;
	public LayerMask wallonlymask;
	public LayerMask entitymask;
	public RectTransform hpscaler, manascaler;
	public TextMeshProUGUI hptxt, manatxt, goldcounter;
	public item itemup;
	public List<vow> vows = new List<vow>();
	public bool canmove = true;
	public delegate void dielegate();
	public dielegate diecall;
	public Transform canvasscaler;
	public Transform tooltip;
	public TextMeshProUGUI tttitle,tttext;
	public int basekarma = 0;
	public Transform npctextui;
	public npcdialoguetree activenpcdialogue;
	
	public void npc_button(int i){
		Debug.Log(i);
		if(activenpcdialogue != null){
			activenpcdialogue.button(i);
		}
	}
	
	public void FixedUpdate(){ 
		//updates the UI to display players current HP, mana, and gold
		hptxt.text =""+ player.pc.chp;
		hpscaler.anchorMin = new Vector2(hpscaler.anchorMin.x, (player.pc.chp+0f)/(player.pc.hp+0f));
		manatxt.text =""+ player.pc.cmana;
		manascaler.anchorMin = new Vector2(manascaler.anchorMin.x, (player.pc.cmana+0f)/(player.pc.mana+0f));
		goldcounter.text = ""+ player.pc.gold;
	}
	
	public void Update(){
		if(Input.GetKeyDown(KeyCode.Space))transform.GetComponent<mapgen>().generatefloorcall();
	}
	
	public void Awake(){
		MR = this;
		Time.timeScale = 0.01f;
	}
	
	public void Start(){
		for(int i = 0; i < player.pc.inventorysize; i++){
			addinvitem(true);
		}
		float temp = Screen.width/1280f;
		canvasscaler.localScale = new Vector3(temp,temp,temp);
	}
	
	public void tileclick(Transform t){
		//changes based on states
		if(!player.pc.moving && canmove){
			if(state == 0){
				player.pc.move(t,0);
			} else if(state == 1){
				if(itseld.transform.GetComponent<itemrangefinder>())itseld.transform.GetComponent<itemrangefinder>().showrange4();
				clickevent.Invoke(t, itseld);
			}
		}
	}
	
	public void button(){
		Debug.Log("Ye");
	}
	
	public Transform itementholder;
	
	public Transform gold1, gold10, gold100;
	
	public void dropgold(int i, Vector3 pos){
		Transform clone;
		while(i >= 100){
			clone = Instantiate(gold100);
			clone.parent = itementholder;
			clone.position = pos + new Vector3(Random.Range(-10,10)*0.03f,Random.Range(3,20)*0.03f,Random.Range(-10,10)*0.03f);
			clone.rotation = Random.rotation;
			clone.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10,10)*0.03f,Random.Range(-10,10)*0.03f,Random.Range(-10,10)*0.03f));
			i -= 100;
		}
		while(i >= 10){
			clone = Instantiate(gold10);
			clone.parent = itementholder;
			clone.position = pos + new Vector3(Random.Range(-10,10)*0.03f,Random.Range(3,20)*0.03f,Random.Range(-10,10)*0.03f);
			clone.rotation = Random.rotation;
			clone.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10,10)*0.03f,Random.Range(-10,10)*0.03f,Random.Range(-10,10)*0.03f));
			i -= 10;
		}
		while(i >= 1){
			clone = Instantiate(gold1);
			clone.parent = itementholder;
			clone.position = pos + new Vector3(Random.Range(-10,10)*0.03f,Random.Range(3,20)*0.03f,Random.Range(-10,10)*0.03f);
			clone.rotation = Random.rotation;
			clone.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10,10)*0.03f,Random.Range(-10,10)*0.03f,Random.Range(-10,10)*0.03f));
			i--;
		}
	}
	
	public void droploot(Transform[] loot, Vector3 pos){
		foreach(Transform t in loot){
			Transform clone = Instantiate(t);
			clone.parent = itementholder;
			clone.GetChild(1).position = pos + new Vector3(Random.Range(-10,10)*0.03f,Random.Range(0,20)*0.03f+0.5f,Random.Range(-10,10)*0.03f);
			clone.GetChild(1).rotation = Random.rotation;
			clone.GetChild(1).GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10,10)*0.03f,Random.Range(-10,10)*0.03f,Random.Range(-10,10)*0.03f));
			
		}
	}
	
	public bool additem(Transform itm){
		//todo
		return false;
	}
	
	public Transform entrans;
	
	public void doenemyturn(int i){	
		canmove = false;
		StartCoroutine(doenemyturn2(i));
	}
	
	public IEnumerator doenemyturn2(int i){
		if(i < entrans.childCount){entrans.GetChild(i).GetComponent<enemy>().brn.doturn(i);
		} else {
			doenemyturnatk(0);
		}
		yield return new WaitForEndOfFrame();
	}
	
	public void doenemyturnatk(int i){
		StartCoroutine(doenemyturnatk2(i));
	}
	
	public IEnumerator doenemyturnatk2(int i){
		if(i < entrans.childCount){
			entrans.GetChild(i).GetComponent<enemy>().brn.doatkturn(i);
		} else {
			if(diecall != null){
				diecall();
				diecall = null;
			}
			canmove = true;
		}
		yield return new WaitForEndOfFrame();
	}
	
	public Transform invitempre;
	public Transform inv;
	
	public void addinvitem(){
		RectTransform clone = Instantiate(invitempre as RectTransform);
		clone.parent = inv;
		int temp = inv.childCount+1;
		clone.anchoredPosition = new Vector2(Screen.width*((temp+1f)/temp),0);
		StartCoroutine(animateinv(clone,new Vector2((Screen.width*((temp-1f)/temp))*0.9095f,0),0));
		for(int i = inv.childCount-2; i > -1; i--){
			StartCoroutine(animateinv((inv.GetChild(i) as RectTransform),new Vector2((Screen.width*((i+1f)/temp))*0.9095f,0),inv.childCount-i+5));
		}
		
	}
	
	public void addinvitem(bool b){
		RectTransform clone = Instantiate(invitempre as RectTransform);
		clone.parent = inv;
		int temp = inv.childCount+1;
		for(int i = inv.childCount-1; i > -1; i--){
			(inv.GetChild(i) as RectTransform).anchoredPosition = new Vector2((Screen.width*((i+1f)/temp))*0.9095f,0);
		}
		
	}
	
	public void addinvitem(int i2){
		RectTransform clone = Instantiate(invitempre as RectTransform);
		clone.parent = inv;
		clone.SetSiblingIndex(i2);
		int temp = inv.childCount+1;
		for(int i = inv.childCount-1; i > -1; i--){
			(inv.GetChild(i) as RectTransform).anchoredPosition = new Vector2((Screen.width*((i+1f)/temp))*0.9095f,0);
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
	
	public void showitem(Transform itm, Transform itment){
		itm.parent = worlditemholder;
		itm.gameObject.SetActive(true);
		int temp = worlditemholder.childCount+1;
		(itm as RectTransform).anchoredPosition = new Vector2(Screen.width*((temp+1f)/temp),0);
		StartCoroutine(animateinv((itm as RectTransform),new Vector2((Screen.width*((temp-1f)/temp))*0.9095f,0),0));
		for(int i = worlditemholder.childCount-2; i > -1; i--){
			StartCoroutine(animateinv((worlditemholder.GetChild(i) as RectTransform),new Vector2((Screen.width*((i+1f)/temp))*0.9095f,0),worlditemholder.childCount-i+5));
		}	
	}
		
	public void hideitem(Transform itm, Transform itment){
		itm.parent = itment.parent;
		itm.gameObject.SetActive(false);
	}
	
	public void hideallitems(){
		foreach(Transform t in itementholder){
			if(t.GetChild(0).GetComponent<itementity>())t.GetChild(0).GetComponent<itementity>().hide();
		}
		foreach(Transform t2 in inv){
			t2.GetComponent<item>().onmove();
		}
	}
	
	public bool waspicked = false;
	
	public void pickup(){
		inv.BroadcastMessage("pickup_itemcall");
		waspicked = true;
	}
	
	public void onmove_player1(){
		BroadcastMessage("onmove_player");
	}
	
	public void onmove_player(){ //"waaa there's no reciever for the message"
	}
	
	public void restackpickups(){
		int temp = worlditemholder.childCount+1;
		for(int i = worlditemholder.childCount-1; i > -1; i--){
			StartCoroutine(animateinv((worlditemholder.GetChild(i) as RectTransform),new Vector2((Screen.width*((i+1f)/temp))*0.9095f,0),worlditemholder.childCount-i+5));
		}
	}
	
	public Transform dragicon;
	
	public void showdrag(Sprite s, int dam, int dur){
		dragicon.GetComponent<Image>().sprite = s;
		dragicon.gameObject.SetActive(true);
		if(dam != -1){
			dragicon.GetChild(0).GetComponent<TextMeshProUGUI>().text = ""+dam;
			dragicon.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+dur;
		} else if (dur != -1) {
			dragicon.GetChild(0).GetComponent<TextMeshProUGUI>().text = ""+dur;
			dragicon.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
		} else {
			dragicon.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
			dragicon.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
		}
	}
	
	public void hidedrag(){
		dragicon.gameObject.SetActive(false);
	}
	
	public TextMeshProUGUI vownum;
	
	public void addvow(vow v){
		vows.Add(v);
		int temp = basekarma;
		foreach(vow v2 in vows){
			temp += v2.karmavalue;
		}
		vownum.text = temp+"";
	}
	
	public void showkarma(){
		int temp = basekarma;
		foreach(vow v2 in vows){
			temp += v2.karmavalue;
		}
		vownum.text = temp+"";
	}
	
	public bool showtip = false;
	
	public void showtooltip(item i, bool b){
		showtip = b;
		if(b)StartCoroutine(showtooltip2(i));
	}
	
	public IEnumerator showtooltip2(item i){
		yield return new WaitForSeconds(1);
		if(showtip){
			tooltip.gameObject.SetActive(true);
			tttitle.text = i.nme;
			tttext.text = i.desc;
		}
		yield return new WaitUntil(() => !showtip);
			tooltip.gameObject.SetActive(false);
		
	}
}

[System.Serializable]
public class UEvent : UnityEvent<Transform, item>
{
}

