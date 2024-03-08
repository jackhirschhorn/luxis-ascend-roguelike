using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : entity
{
    public static player pc;
	public int gold = 0;
	public int inventorysize = 2;
	public int karma = 0;
	
	
	
	public void Awake(){
		pc = this;
	}
	
	
	
	
}
