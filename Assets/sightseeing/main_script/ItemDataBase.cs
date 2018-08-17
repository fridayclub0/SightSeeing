using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//ローカル用データベース
public class ItemDataBase : MonoBehaviour {

	private List<Item> ItemList = new List<Item>();
	private List<Item> AccessoryList = new List<Item>();
	private List<Item> DressList = new List<Item>();
	private List<Item> WeaponList = new List<Item>();

	private Player playerdata;

	public List<Item> getItemData() {
		return ItemList;
	}

	public List<Item> getAccessoryData() {
		return AccessoryList;
	}

	public List<Item> getDressData() {
		return DressList;
	}

	public List<Item> getWeaponData() {
		return WeaponList;
	}





	public string getPlayerName() {
		return playerdata.playerName;
	}

	public int getMoney() {
		return playerdata.money;
	}

	public int getAccessoryID() {
		return playerdata.accessoryID;
	}

	public int getWeaponID() {
		return playerdata.weaponID;
	}

	public int getDressID() {
		return playerdata.dressID;
	}

	public int getSupportID() {
		return playerdata.supportID;
	}





	public void SetPlayerAccessory(int id) {
		playerdata.accessoryID = id;
	}

	public void SetPlayerDress(int id) {
		playerdata.dressID = id;
	}

	public void SetPlayerWeapon(int id) {
		playerdata.weaponID = id;
	}
		
	void Start()
	{	
		ItemDire itemDire = GetComponent<ItemDire> ();
		AccessoryDire accessoryDire = GetComponent<AccessoryDire> ();
		DressDire dressDire = GetComponent<DressDire> ();
		WeaponDire weaponDire = GetComponent<WeaponDire> ();

		playerdata = new Player ("テストプレイヤー",0,0,0,0,0,0);

		ItemList.Add (itemDire.GetItem(0));
		AccessoryList.Add (accessoryDire.GetItem (0));
		DressList.Add (dressDire.GetItem (0));
		WeaponList.Add (weaponDire.GetItem (0));

	}

}
