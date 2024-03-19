using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="theme")]
public class theme : ScriptableObject
{
    public List<Transform> tiles = new List<Transform>();
	public List<Transform> doors = new List<Transform>();
	public List<Transform> walls = new List<Transform>();
	public List<Transform> wallconnectors = new List<Transform>();
	public List<Transform> deco = new List<Transform>();
	public List<Transform> enemy_melee = new List<Transform>();
	public List<Transform> enemy_ranged = new List<Transform>();
	public List<Transform> enemy_heavy = new List<Transform>();
	public List<Transform> enemy_magic = new List<Transform>();
	public List<Transform> enemy_fast = new List<Transform>();
	public List<Transform> enemy_miniboss = new List<Transform>();
	public List<Transform> enemy_boss = new List<Transform>();
}
