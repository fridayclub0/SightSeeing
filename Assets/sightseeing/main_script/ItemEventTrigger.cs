using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemEventTrigger : EventTrigger {
	[SerializeField]
	private GameObject canvas;


	void Start(){
		canvas = GameObject.Find ("Canvas");
		//Debug.Log (canvas);
	}

	public override void OnSelect(BaseEventData data)
	{
		canvas.GetComponent<Main_canvas> ().SelectMotion ();
	}
}
