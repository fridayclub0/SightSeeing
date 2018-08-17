using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
	public string playerName;
	public int playerID;
	public int money;
	public int accessoryID;
	public int weaponID;
	public int dressID;
	public int supportID;

	//ここでリスト化時に渡す引数をあてがいます   
	public Player(string Pname, int PID, int Money, int acID, int wpID, int drID, int spID)	{
		playerName = Pname;
		playerID = PID;
		money = Money;
		accessoryID = acID;
		weaponID = wpID;
		dressID = drID;
		supportID = spID;
	}
}
