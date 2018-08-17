using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressDire : MonoBehaviour {


	private Dictionary<int, Item> Lists;

	void Awake() {

		Lists = new Dictionary<int, Item> ();

		Lists.Add (0,
			new Item("ボロい服", "olddress", 0, "ボロボロの服。早く新しいのを買おう", Item.ItemType.Dress)
		);

		Lists.Add (1,
			new Item("ありふれた服", "ordanarydress" ,1, "そこら辺の人がきていそうな服", Item.ItemType.Dress)
		);


	}

	public Item GetItem(int id){
		//Debug.Log (id);
		return Lists[id];
	}
}
