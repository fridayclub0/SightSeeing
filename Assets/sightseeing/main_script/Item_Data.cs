using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//持ち物リスト用
public class Item_Data : MonoBehaviour {
	
	[SerializeField]
	private int itemID;             //アイテムID
	[SerializeField]
	private Item.ItemType itemType;      //アイテムの種類


	public void SetData(Item item) {
		itemID = item.itemID;		
		itemType = item.itemType;
		transform.Find("Label").GetComponent<Text> ().text = item.itemName;
	}


	public Item GetData() {
		
		switch (itemType) {
		case Item.ItemType.Item:
			return GameObject.Find ("MyDataBase").GetComponent<ItemDire> ().GetItem (itemID);
		case Item.ItemType.Accessory:
			return GameObject.Find ("MyDataBase").GetComponent<AccessoryDire> ().GetItem (itemID);
		case Item.ItemType.Dress:
			return GameObject.Find ("MyDataBase").GetComponent<DressDire> ().GetItem (itemID);
		case Item.ItemType.Weapon:
			return GameObject.Find ("MyDataBase").GetComponent<WeaponDire> ().GetItem (itemID);
		default:
			Debug.Log ("Item_Data GetData Error");
			return null;
		}
	}

	public void onclick() {
		ItemDataBase DataBase = GameObject.Find("MyDataBase").GetComponent<ItemDataBase> ();

		switch (itemType) {
		case Item.ItemType.Item:
			break;
		case Item.ItemType.Accessory:
			DataBase.SetPlayerAccessory (itemID);
			break;
		case Item.ItemType.Dress:
			DataBase.SetPlayerDress (itemID);
			break;
		case Item.ItemType.Weapon:
			DataBase.SetPlayerWeapon (itemID);
			break;
		default:
			break;
		}
	}

}
