using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//itemクラス
public class Item {

	public string itemName;        //名前
	public string itemDataName;
	public int itemID;             //アイテムID
	public string itemDesc;        //アイテムの説明文
	public Sprite itemIcon;     //アイコン
	public ItemType itemType;      //アイテムの種類

	//アイテムタイプenum
	public enum ItemType
	{
		Item,
		Accessory,
		Dress,
		Weapon
	}

	//ここでリスト化時に渡す引数をあてがいます   
	public Item(string name, string dataname, int id, string desc, ItemType type)
	{
		itemName = name;
		itemDataName = dataname;
		itemID = id;
		itemIcon = Resources.Load<Sprite>("Icons/"+ itemDataName);
		itemDesc = desc;
		itemType = type;
	}

}

