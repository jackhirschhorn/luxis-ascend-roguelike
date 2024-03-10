using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class karmamenu : MonoBehaviour
{
	public Transform sets;
    public void open(){
		transform.gameObject.SetActive(!transform.gameObject.activeSelf);
		populate();
	}
	
	public void populate(){
		(transform.GetChild(0) as RectTransform).offsetMin = new Vector2((transform.GetChild(0) as RectTransform).offsetMin.x,0);
		for(int i = transform.GetChild(0).childCount-1; i >= 0; i--){
			Destroy(transform.GetChild(0).GetChild(i).gameObject);
		}
		for(int i = 0; i < master.MR.vows.Count; i+=2){
			Transform clone = Instantiate(sets);
			clone.parent = transform.GetChild(0);
			(clone as RectTransform).anchoredPosition = new Vector2(0,-60-(60*i));
			(clone as RectTransform).offsetMin = new Vector2(0,(clone as RectTransform).anchoredPosition.y);
			(clone as RectTransform).offsetMax = new Vector2(0,(clone as RectTransform).anchoredPosition.y);
			clone.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = (master.MR.vows[i].karmavalue+master.MR.vows[i+1].karmavalue)+"";
			clone.GetChild(1).GetComponent<TextMeshProUGUI>().text = master.MR.vows[i].description;
			clone.GetChild(2).GetComponent<TextMeshProUGUI>().text = master.MR.vows[i+1].description;
			if(i > 8){
				(transform.GetChild(0) as RectTransform).offsetMin = new Vector2((transform.GetChild(0) as RectTransform).offsetMin.x,(transform.GetChild(0) as RectTransform).offsetMin.y-120);
			}
		}
	}
}
