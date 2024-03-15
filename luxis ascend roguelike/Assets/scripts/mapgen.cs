using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapgen : MonoBehaviour
{
    public List<theme> themes = new List<theme>();
	public List<Transform> rooms = new List<Transform>();
	public Transform saferoom;
	public Transform map;
	
	public int floorsize = 5;
	
	public void generatefloorcall(){
		StartCoroutine(generatefloor());
	}
	
	public IEnumerator generatefloor(){
		yield return new WaitForEndOfFrame();
		//generate start room
		Transform clone = Instantiate(saferoom);
		clone.parent = map;
		clone.position = new Vector3(0,0,0);
		int curfloorsize = 1;
		while(curfloorsize < floorsize){
			clone = Instantiate(rooms[Random.Range(0,rooms.Count)]);
			int rando1 = Random.Range(2,5);
			Vector2 connectfind = clone.GetComponent<roomgrid>().findcell(rando1);
			while(connectfind == new Vector2(10,10)){
				rando1 = Random.Range(2,5);
				connectfind = clone.GetComponent<roomgrid>().findcell(rando1);
				Debug.Log(connectfind);
				yield return new WaitForEndOfFrame();
			}
			int rando2 = (rando1 == 2?3:(rando1 == 3?2:(rando1 == 4?5:4)));
			Transform par = map.GetChild(Random.Range(0,map.childCount));
			Vector2 connectfind2 = par.GetComponent<roomgrid>().findcell(rando2);
			while(connectfind2 == new Vector2(10,10)){
				par = map.GetChild(Random.Range(0,map.childCount));
				connectfind2 = par.GetComponent<roomgrid>().findcell(rando2);
				Debug.Log(connectfind2);
				yield return new WaitForEndOfFrame();
			}	
			int offsetx = (int)(connectfind.x-(connectfind.x != 0?(connectfind2.x > 0?connectfind2.x-1:connectfind2.x+1):0));
			int offsety = (int)(connectfind.y-(connectfind.y != 0?(connectfind2.y > 0?connectfind2.y-1:connectfind2.y+1):0));
			
			Debug.Log(offsetx + " " + offsety);

			clone.parent = map;
			clone.position = par.position+new Vector3((offsetx*5)+(offsetx != 0?(offsetx >= 1?1:-1):0),0,(offsety*5)+(offsety != 0?(offsety >= 1?1:-1):0));
			clone.GetComponent<roomgrid>().emptycell(connectfind);
			par.GetComponent<roomgrid>().emptycell(connectfind2);
			curfloorsize++;
		}
	}
}
