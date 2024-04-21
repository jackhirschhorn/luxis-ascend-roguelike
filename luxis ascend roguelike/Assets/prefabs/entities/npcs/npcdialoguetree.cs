using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcdialoguetree : MonoBehaviour
{
   public int stage = 0;
   public List<npcdialogueset> sets = new List<npcdialogueset>();
   
   public virtual void showdialogue(int i){
	   
   }
   
   public virtual void enddialogue(int i){
	   
   }
   
   public virtual void button(int i){
	   
   }
   
}
