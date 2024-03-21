using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_Data : ScriptableObject
{	
	public List<Param> param = new List<Param> ();

	[System.SerializableAttribute]
	public class Param
	{
		
		public int index;
		public int hp;
		public int mp;
		public string name;
	}
}