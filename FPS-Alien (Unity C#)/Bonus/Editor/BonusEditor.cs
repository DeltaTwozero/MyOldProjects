using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PickupBonus))]
public class BonusEditor : Editor 
{
	public override void OnInspectorGUI ()
	{
		PickupBonus bonus = target as PickupBonus;

		if (bonus == null)
			return;

		bonus.Type = (BonusType)EditorGUILayout.EnumPopup ("Type:", bonus.Type);

		if (bonus.Type == BonusType.Ammo)
			bonus.AmmoType = (AmmoType)EditorGUILayout.EnumPopup ("Ammo type:", bonus.AmmoType);

		bonus.Quantity = EditorGUILayout.FloatField ("Quantity", bonus.Quantity);
	}
}