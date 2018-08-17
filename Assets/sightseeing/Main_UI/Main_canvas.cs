using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Main_canvas : MonoBehaviour {
	public Animator animator;

	public GameObject ActiveGameObject;

	[SerializeField]
	private GameObject DataBase;

	[SerializeField]
	private GameObject ItemSlot;
	[SerializeField]
	private GameObject WeaponSlot;



	[SerializeField]
	private GameObject MainPanel;
	[SerializeField]
	private GameObject ToolsPanel;
	[SerializeField]
	private GameObject ProfilePanel;
	[SerializeField]
	private GameObject ItemContent;
	[SerializeField]
	private GameObject AccessoryContent;
	[SerializeField]
	private GameObject WeaponContent;
	[SerializeField]
	private GameObject DressContent;
	[SerializeField]
	private GameObject ItemInfoPanel;

	[SerializeField]
	private List<Item> items;
	[SerializeField]
	private List<Item> accessories;
	[SerializeField]
	private List<Item> weapons;
	[SerializeField]
	private List<Item> dresses;

	[SerializeField]
	private Player playerdata;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.N)) {
			if (animator.GetBool("main") == false) {
				animator.SetBool("main", true);
				EventSystem.current.SetSelectedGameObject (MainPanel.transform.GetChild(0).gameObject);
			} else {
				animator.SetBool ("main", false);
				animator.SetBool ("toolbox", false);
				animator.SetBool ("profile", false);
				EventSystem.current.SetSelectedGameObject (null);
			}
		}
			
	}



	public void ToolBox_OnClick() {
		Debug.Log ("ToolBox_OnClick");
		animator.SetBool ("toolbox", true);

		items = DataBase.GetComponent<ItemDataBase> ().getItemData ();
		accessories = DataBase.GetComponent<ItemDataBase> ().getAccessoryData ();
		dresses = DataBase.GetComponent<ItemDataBase> ().getDressData ();
		weapons = DataBase.GetComponent<ItemDataBase> ().getWeaponData ();

		CreateSlot(ItemContent, items);
		CreateSlot(AccessoryContent, accessories);
		CreateSlot(DressContent,dresses);
		CreateSlot(WeaponContent, weapons);

		EventSystem.current.SetSelectedGameObject (ToolsPanel.transform.GetChild(0).gameObject);
	}


	public void Profile_OnClick() {
		animator.SetBool ("profile", true);

		ItemDataBase DataBaseSC = DataBase.GetComponent<ItemDataBase> ();

		//ItemDire ItemDireSC = DataBase.GetComponent<ItemDire> ();
		AccessoryDire AccessoryDireSC = DataBase.GetComponent<AccessoryDire> ();
		WeaponDire WeaponDireSC = DataBase.GetComponent<WeaponDire> ();
		DressDire DressDireSC = DataBase.GetComponent<DressDire> ();



		ProfilePanel.transform.Find ("KihonPanel/playername").GetComponent<Text>().text = DataBaseSC.getPlayerName();
		ProfilePanel.transform.Find ("KihonPanel/money").GetComponent<Text>().text = "所持金："+DataBaseSC.getMoney().ToString();
		ProfilePanel.transform.Find ("SoubiPanel/accessory").GetComponent<Text> ().text = "アクセサリ：" + AccessoryDireSC.GetItem(DataBaseSC.getAccessoryID()).itemName;
		ProfilePanel.transform.Find ("SoubiPanel/dress").GetComponent<Text>().text = "衣装：" + DressDireSC.GetItem(DataBaseSC.getDressID()).itemName;
		//ProfilePanel.transform.Find ("SoubiPanel/weapon").GetComponent<Text>().text = "道具："+DataBaseSC.getWeaponID().ToString();
		ProfilePanel.transform.Find ("SoubiPanel/weapon").GetComponent<Text> ().text = "道具：" + WeaponDireSC.GetItem(DataBaseSC.getWeaponID()).itemName;
	}



	public void Item_OnClick() {
		Debug.Log ("Item_OnClick");
		animator.SetBool ("item", true);
		animator.SetBool ("accessories", false);
		animator.SetBool ("dress", false);
		animator.SetBool ("weapon", false);
	}

	public void Accessories_OnClick() {
		Debug.Log ("Accessories");
		animator.SetBool ("item", false);
		animator.SetBool ("accessories", true);
		animator.SetBool ("dress", false);
		animator.SetBool ("weapon", false);
	}

	public void Dress_OnClick() {
		Debug.Log ("Dress");
		animator.SetBool ("item", false);
		animator.SetBool ("accessories", false);
		animator.SetBool ("dress", true);
		animator.SetBool ("weapon", false);
	}

	public void Weapon_OnClick() {
		Debug.Log ("Weapon");
		animator.SetBool ("item", false);
		animator.SetBool ("accessories", false);
		animator.SetBool ("dress", false);
		animator.SetBool ("weapon", true);
	}

	public void CreateSlot(GameObject ContentPanel, List<Item> itemLists) {
		int i = 0;
		int slotsize = 30;
		//Vector2 DispX = new Vector2 (100f, 0f);

		foreach ( Transform n in ContentPanel.transform )
		{
			GameObject.Destroy(n.gameObject);
		}

		foreach (var item in itemLists) {
			// スロットのインスタンス化
			var instanceSlot = Instantiate (ItemSlot) as GameObject;
			// スロットゲームオブジェクトの名前を設定
			instanceSlot.name = "ItemSlot" + i;
			// 親をMainにする
			instanceSlot.transform.SetParent (ContentPanel.transform);
			// Scaleを設定しないと0になるので設定
			instanceSlot.transform.localScale = new Vector3 (1f, 1f, 1f);
			instanceSlot.GetComponent<Item_Data> ().SetData (item);

			//位置と大きさを指定
			instanceSlot.GetComponent<RectTransform> ().offsetMax = new Vector2 (0, 0);
			instanceSlot.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, slotsize);
			instanceSlot.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, -slotsize / 2) + new Vector2(0, -slotsize) * i;

			//アイテム数に応じてコンテントパネルのサイズ変更
			ItemContent.GetComponent<RectTransform> ().sizeDelta = new Vector2(0, slotsize * (i + 1));

			i++;
		}
	}

	public void SelectMotion() {
		//Debug.Log (EventSystem.current.currentSelectedGameObject);
		ActiveGameObject = EventSystem.current.currentSelectedGameObject;
		//Debug.Log(ActiveGameObject.gameObject.name);

		if (ActiveGameObject.tag == "itemslot"){
			//Debug.Log(ActiveGameObject.GetComponent<Item_Data> ().itemName);
			SetItemInfo (ActiveGameObject.GetComponent<Item_Data> ().GetData ()); 
		}
	}

	public void SetItemInfo(Item item) {
		//ItemInfoPanel.transform.FindChild ("ItemImage");
		ItemInfoPanel.transform.Find("ItemName").GetComponent<Text>().text = item.itemName;
		ItemInfoPanel.transform.Find("ItemDesc").GetComponent<Text>().text = item.itemDesc;
		ItemInfoPanel.transform.Find("ItemImage").GetComponent<Image>().sprite = item.itemIcon;

	}
	
}
