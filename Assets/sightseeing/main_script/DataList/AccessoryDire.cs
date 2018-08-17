using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryDire : MonoBehaviour {


	private Dictionary<int, Item> Lists;

	void Awake() {

		Lists = new Dictionary<int, Item> ();

		Lists.Add (0,
			new Item("古いアクセサリ", "oldaccessory", 0, "古くてボロいアクセサリ", Item.ItemType.Accessory)
		);

		Lists.Add (1,
			new Item("ただのアクセサリ", "accessory",  1, "木でできたスコップ。すぐ壊れてしまいそう", Item.ItemType.Accessory)
		);

	}

	public Item GetItem(int id){
		//Debug.Log (id);
		return Lists[id];
	}
}
