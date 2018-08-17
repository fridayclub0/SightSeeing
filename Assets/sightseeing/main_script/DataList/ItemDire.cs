using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDire : MonoBehaviour {

	private Dictionary<int, Item> Lists;

	void Awake() {

		Lists = new Dictionary<int, Item> ();

		Lists.Add (0,
			new Item("キズぐすり", "potion", 0, "体力を20回復する", Item.ItemType.Item)
		);

		Lists.Add (1,
			new Item("いいキズぐすり", "superpotion", 1, "体力を50回復する", Item.ItemType.Item)
		);

	}

	public Item GetItem(int id){
		//Debug.Log (Lists[id]);
		return Lists[id];
	}
}
