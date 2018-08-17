using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//マップ配置用
public class Item_Object : MonoBehaviour {
	public Animator animator;
	public Item item;

	[SerializeField]
	public int itemID;             //アイテムID
	[SerializeField]
	public Item.ItemType itemType;      //アイテムの種類

	void Start() {
		animator = GetComponent<Animator> ();
		//Inspectorからセットしておくこと

		switch (itemType) {
		case Item.ItemType.Item:
			item = GameObject.Find ("MyDataBase").GetComponent<ItemDire> ().GetItem (itemID);
			break;
		case Item.ItemType.Accessory:
			item = GameObject.Find ("MyDataBase").GetComponent<AccessoryDire> ().GetItem (itemID);
			break;
		case Item.ItemType.Dress:
			item = GameObject.Find ("MyDataBase").GetComponent<DressDire> ().GetItem (itemID);
			break;
		case Item.ItemType.Weapon:
			item = GameObject.Find ("MyDataBase").GetComponent<WeaponDire> ().GetItem (itemID);
			break;
		default:
			break;
		}

	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			animator.SetBool ("Founded", true);
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Player") {
			animator.SetBool ("Founded", false);
		}
	}

}
