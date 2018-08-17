using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDire : MonoBehaviour{
	
	private Dictionary<int, Item> Lists;

	void Awake() {
		
		Lists = new Dictionary<int, Item> ();

		Lists.Add (0,
			new Item("ボロのスコップ", "oldscop", 0, "壊れかけのスコップ。そんな装備で大丈夫か？", Item.ItemType.Weapon)
		);

		Lists.Add (1,
			new Item("鉄のスコップ", "ironscop", 1, "鉄でできた立派なスコップ。", Item.ItemType.Weapon)
		);

	}

	public Item GetItem(int id){
		//Debug.Log (id);
		return Lists[id];
	}

}
