using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopkeeper_talk : npcdialoguetree
{
	public override void showdialogue(int i){
		stage = i;
		master.MR.activenpcdialogue = this;
		master.MR.npctextui.gameObject.SetActive(true);
		master.MR.npctextui.GetChild(1).GetChild(0).GetComponent<Image>().sprite = face;
		switch(stage){
			case 0:
				master.MR.npctextui.GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = sets[0].strings[0];
				master.MR.npctextui.GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = sets[1].strings[0];
				master.MR.npctextui.GetChild(3).gameObject.SetActive(true);
				master.MR.npctextui.GetChild(4).gameObject.SetActive(false);
				master.MR.npctextui.GetChild(5).gameObject.SetActive(false);
				break;
			case 1:
				master.MR.npctextui.GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = sets[0].strings[1];
				master.MR.npctextui.GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = sets[1].strings[1];
				break;
			case 2:
				stage = 0;
				this.enddialogue(0);
				break;
		}
	}

	public override void enddialogue(int i){
		stage = 0;
		master.MR.npctextui.gameObject.SetActive(false);
		
	}

	public override void button(int i){
		switch(stage){
			case 0:
				switch(i){
					case 0:
						showdialogue(1);
						break;
					case 1:
						
						break;
					case 2:
						
						break;
				}
				break;
			case 1:
				switch(i){
					case 0:
						showdialogue(2);
						break;
					case 1:
						
						break;
					case 2:
						
						break;
				}
				break;
		}
	}
}
