using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make this a scriptable object!

public class resourceopt : ScriptableObject
{
    public int id;
	public string nme;
	public int amnt;
	public int max; // also used for ability scaling
	
	public resourceopt(int i, string s, int i2, int i3){
		id = i;
		nme = s;
		amnt = i2;
		max = i3;
	}
}
