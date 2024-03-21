using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

// @kurtdekker - ultra Cheesy grid with in-built editor for Unity3D
//
// To use:
//	make an empty game object
//	drag this script on it
// 	make a prefab out of it
//	select the prefab to edit the grid
//
// now you can cheesy-easy edit a smallish grid in the Unity editor window
//
public class roomgrid : MonoBehaviour
{
	[Header( "0 = empty, 1 = room, 2 = doorN, 3 = doorS, 4 = doorL, 5 = doorR")]
	int MaxValue = 6;

	//[Header( "Actual saved payload. Use GetCell(x,y) to read!")]
	//[Header( "WARNING: changing this will nuke your data!")]
	public string data;

	int across = 7;
	int down = 7;
	
	public int gridx,gridy;

	// stretch goals for you:
	// TODO: make an array of colors perhaps?
	// TODO: make a color mapper??
	// TODO: map above characters to graphics??

	// for you to get stuff out of the grid to use in your game
	public string getcell( int x, int y)
	{
		int n = GetIndex( x, y);
		return data.Substring( n, 1);
	}

	public Vector2 findcell(int i){
		int count = data.Split((i+"")).Length - 1;
		if(count == -1) return new Vector2(10,10);	
		int rando = Random.Range(0,count);
		int temp = 0;
		for(int i2 = 0; i2 <= rando;i2++){
			temp = data.IndexOf(i+"",temp+1);
		}
		if(temp == -1) return new Vector2(10,10);
		int tempx = (temp%7)-3;
		int tempy = (temp/7)-3;
		return new Vector2(tempx,tempy);
	}

	public void emptycell (Vector2 v){
		int temp = (int)(((v.y+3)*7)+(v.x+3));
		char[] chars = data.ToCharArray();
		chars[temp] = '0';
		data = new string(chars);
	}
	
	public List<Vector2> findcells(int i){
		List<Vector2> returno = new List<Vector2>();
		int temp = 0;
		while(temp != -1){
			temp = data.IndexOf(i+"",temp+1);
			if(temp == -1)break;
			int tempx = (temp%7)-3;
			int tempy = (temp/7)-3;
			returno.Add(new Vector2(tempx,tempy));
		}
		return returno;
	}
	
	public List<List<Vector2>> finddoorchecks(){
		List<List<Vector2>> returno = new List<List<Vector2>>();
		int temp = 0;
		while(temp != -1){
			List<Vector2> returne = new List<Vector2>();
			temp = data.IndexOf("1",temp+1);
			if(temp == -1)break;

			int tempx = (temp%7)-3;
			int tempy = (temp/7)-3;
			returne.Add(new Vector2(tempx,tempy));

			tempx = ((temp+1)%7)-3;
			tempy = (temp/7)-3;
			if(!getcell(tempx+3,tempy+3).Equals("1"))returne.Add(new Vector2(tempx,tempy));
			
			tempx = ((temp-1)%7)-3;
			tempy = (temp/7)-3;
			if(!getcell(tempx+3,tempy+3).Equals("1"))returne.Add(new Vector2(tempx,tempy));
			
			tempx = (temp%7)-3;
			tempy = ((temp+7)/7)-3;
			if(!getcell(tempx+3,tempy+3).Equals("1"))returne.Add(new Vector2(tempx,tempy));
			
			tempx = (temp%7)-3;
			tempy = ((temp-7)/7)-3;
			if(!getcell(tempx+3,tempy+3).Equals("1"))returne.Add(new Vector2(tempx,tempy));
			returno.Add(returne);
		}
		return returno;
	}

	#if UNITY_EDITOR
	void OnValidate()
	{
		if (data == null || data.Length != (across * down))
		{
			Undo.RecordObject( this, "Resize");

			if (across < 1) across = 1;
			if (down < 1) down = 1;

			// make a default layout
			data = "";
			for (int y = 0; y < down; y++)
			{
				for (int x = 0; x < across; x++)
				{
					string cell = "0";

					if (x == 0 || y == 0 || x == (across - 1) || y == (down - 1))
					{
						cell = "1";
					}
					if (x == 1 && y == 1)
					{
						cell = "2";
					}
					if (x == (across - 2) && y == (down - 2))
					{
						cell = "3";
					}
					data = data + cell;
				}
			}

			EditorUtility.SetDirty( this);
		}
	}
	void Reset()
	{
		OnValidate();
	}
#endif

	int GetIndex( int x, int y)
	{
		if (x < 0) return -1;
		if (y < 0) return -1;
		if (x >= across) return -1;
		if (y >= down) return -1;
		return x + y * across;
	}

	void ToggleCell( int x, int y)
	{
		int n = GetIndex( x, y);
		if (n >= 0)
		{
			var cell = data.Substring( n, 1);

			int c = 0;
			int.TryParse( cell, out c);
			c++;
			if (c >= MaxValue) c = 0;

			cell = c.ToString();

#if UNITY_EDITOR
			Undo.RecordObject( this, "Toggle Cell");
#endif
			// reassemble
			data = data.Substring( 0, n) + cell + data.Substring( n + 1);
#if UNITY_EDITOR
			EditorUtility.SetDirty( this);
#endif
		}
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(roomgrid))]
	public class CheesyGridEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			var grid = (roomgrid)target;

			EditorGUILayout.BeginVertical();

			//GUILayout.Label( "WARNING: Save and commit your prefab/scene OFTEN!");

			for (int y = 0; y < grid.down; y++)
			{
				GUILayout.BeginHorizontal();
				for (int x = 0; x < grid.across; x++)
				{
					int n = grid.GetIndex( x, y);

					var cell = grid.data.Substring( n, 1);

					// hard-coded some cheesy color map - improve it by all means!
					GUI.color = Color.gray;
					if (cell == "1") GUI.color = Color.white;
					if (cell == "2") GUI.color = Color.red;
					if (cell == "3") GUI.color = Color.green;

					if (GUILayout.Button( cell,  GUILayout.Width(20)))
					{
						grid.ToggleCell(x, y);
					}
				}
				GUILayout.EndHorizontal();
			}
			GUI.color = Color.yellow;

			//GUILayout.Label( "DANGER ZONE BELOW THIS AREA!");

			GUI.color = Color.white;

			EditorGUILayout.EndVertical();

			DrawDefaultInspector();
		}
	}
#endif
}
