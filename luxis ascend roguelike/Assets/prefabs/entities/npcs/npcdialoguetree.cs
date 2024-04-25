using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcdialoguetree : MonoBehaviour
{
   public int stage = 0;
   public Sprite face;
   public List<npcdialogueset> sets = new List<npcdialogueset>();
   
   public virtual void showdialogue(int i){
	   
   }
   
   public virtual void enddialogue(int i){
	   
   }
   
   public virtual void button(int i){
	   
   }
   
}
