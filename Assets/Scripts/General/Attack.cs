using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Assets/Resources/Attack", order = 1)]
public class Attack : ScriptableObject
{
	public string playerDescription;
	public string npcDescription;
	public int damage;

}