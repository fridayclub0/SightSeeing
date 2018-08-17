using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class netplayer: MonoBehaviour {
	private Camera m_camera;
	private Rigidbody rigid;
	[SerializeField]
	private Animator anim;

	private List<Item> ItemList;
	private List<Item> AccessoryList;
	private List<Item> WeaponList;
	private List<Item> DressList;

	public float vertical; //入力鉛直成分
	public float horizontal; //入力水平成分
	public float circle; //アクションボタン
	public float camera_vertical;
	public float camera_horizontal;

	public Vector3 direction;
	private Vector3 Look;
	private Vector3 camera_forward;
	private Vector3 camera_up;

	public float velocity = 0f; //速さ　これ使ってんだっけ？
	public Vector3 input_direction; //入力ベクトル方向 x,z
	public Vector3 camera_input_direction; //カメラ用入力ベクトル方向 x,y

	//アニメーション用
	private bool Running;
	private bool Jumping;


	//ジャンプ
	[SerializeField]
	private float JumpTime;
	
	//接地・接壁判定
	public bool isGround;
	public bool nearGround;
	public bool nearWall;
	private Vector3 inclination; //地面との法線

	//速さを測るのに使う
	private Vector3 latest_pos;







	
	void Start () {
		/*
		if (!isLocalPlayer)
			return;
		*/


		rigid = GetComponent<Rigidbody> ();
		anim = transform.Find ("playerobj").GetComponent<Animator> ();


		m_camera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		direction = transform.forward * 0.1f; //速度ベクトル
		Running = false;
		JumpTime = 1f;

		ItemDataBase DataBaseSC = GameObject.Find ("MyDataBase").GetComponent<ItemDataBase> ();

		ItemList = DataBaseSC.getItemData();
		AccessoryList = DataBaseSC.getAccessoryData ();
		WeaponList = DataBaseSC.getWeaponData ();
		DressList = DataBaseSC.getDressData ();

		latest_pos = transform.position;
	}


	/* 
	void OnDrawGizmos()
	{
		//接地判定
		RaycastHit hit;
		var scale = transform.lossyScale.x;
		Physics.BoxCast (transform.position + transform.up * 0.6f, Vector3.one * scale, transform.up * -1f, out hit, transform.rotation, 0.2f);
		Gizmos.DrawRay (transform.position + transform.up * 0.6f, transform.forward * hit.distance);
		Gizmos.DrawWireCube (transform.position + transform.up * hit.distance, Vector3.one * scale * 2);

	}
	*/

	
	void Update () {
		/*
		if (!isLocalPlayer) 
			return;
		*/


		//入力ベクトルの初期化
		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");
		circle = Input.GetAxis ("Circle");

		camera_horizontal = Input.GetAxis ("Camera_Horizontal");
		camera_vertical = Input.GetAxis ("Camera_Vertical");

		Vector3 camera_right = m_camera.transform.right;
		camera_forward = new Vector3(m_camera.transform.forward.x, 0, m_camera.transform.forward.z);
		camera_up = m_camera.transform.up;

		Vector3 horizontal_direction = horizontal*camera_right;
		Vector3 vertical_direction = vertical * camera_forward;

		Vector3 camera_horizontal_direction = camera_horizontal * camera_right;
		Vector3 camera_vertical_direction = camera_vertical * camera_up;

		input_direction = horizontal_direction + vertical_direction;
		input_direction = input_direction.normalized;


		//camera_input_direction = camera_horizontal_direction + camera_vertical_direction;
		//camera_input_direction = camera_input_direction.normalized;




		//接地判定
		RaycastHit temp;
		RaycastHit hit;
		var scale = transform.lossyScale.x * 0.5f;
		nearGround = Physics.SphereCast (transform.position + new Vector3(0, scale + 0.10f, 0), scale, transform.up * -1f, out hit, 0.5f);
		inclination = hit.normal;
		isGround = Physics.SphereCast (transform.position + new Vector3(0, scale + 0.10f, 0), scale, transform.up * -1f, out hit, 0.12f);


		//接面判定
		RaycastHit wall_hit;
		scale = transform.lossyScale.x * 0.5f;
		nearWall = Physics.SphereCast (transform.position + transform.up - transform.forward * 0.1f, scale, transform.forward, out wall_hit, 0.2f);

		/*
		if (!isGround) {
			isGround = !Physics.CheckBox (transform.position, Vector3.one * scale, transform.rotation);
		}
		*/

		//float _Angle = Vector3.Angle(new Vector3(m_camera.transform.forward.x,0,m_camera.transform.forward.z), m_camera.transform.forward);



		//■ ダッシュ
		if (Input.GetKeyDown (KeyCode.R)) {
			if (Running == false) {
				Running = true;
			} else {
				Running = false;
			}
		}



		//速度ベクトル
		if (nearGround){
			float y = 3f; //サイコウソク 
			float k = 4f; //カソク係数
			float b = 5f; //減速率
			if (Running) y = 16f;
			Vector3 g = input_direction * k * y;
			Vector3 a = new Vector3 (0, 0, 0);

			if (input_direction.magnitude == 0)
				a = -b * direction;
			else
				a = g - k * direction;
			
			direction += a * Time.deltaTime;

			/*
			if (input_direction.magnitude != 0)
				direction = direction.magnitude * input_direction;
			*/

		} 


		
		//■ 重力
		//・最大値-20 これはどうかなあ
		//・接地時は重力を無視
		if (direction.y > -20f){
			direction += new Vector3 (0, -1f, 0);
		}
		if (isGround) {
			direction = new Vector3 (direction.x, 0f, direction.z);
		}




		//■ ジャンプ
		//・壁か地面に接していないと出来ない
		//・0.5秒おきにしか出来ない
		if (Input.GetKeyDown (KeyCode.F) & JumpTime >= 0.5f & (nearGround | nearWall)) {
			direction = new Vector3 (direction.x, 15f, direction.z);
			JumpTime = 0;

			if (nearWall) {
				Vector3 walljamp_vector = 1.01f * wall_hit.normal + input_direction.normalized;
				direction += walljamp_vector.normalized * 8f;
			}
		}
		if (JumpTime < 0.5f)
			JumpTime += Time.deltaTime;




		//Player方向
		Vector3 Look_direction = transform.forward;
		Look_direction = Vector3.Slerp (Look_direction, Look_direction + input_direction, 0.3f);
		Look_direction = new Vector3(Look_direction.x, 0, Look_direction.z);
		transform.LookAt (transform.position + Look_direction);



		//カメラ追従（保留　現在は意味をなしていない）
		/*
		Vector3 camera_horizontal_input_direction = camera_horizontal*camera_right;
		Vector3 camera_vertical_input_direction = camera_vertical * camera_up;
		Look = transform.forward;
		if (camera_horizontal != 0f) {
			Look += camera_horizontal_input_direction * 0.04f;
		}
		if (camera_vertical != 0f) {
			Look += camera_vertical_input_direction * 0.04f;
		}
		*/


		//カメラ方向
		Vector3 camera_height = new Vector3 (0, 2, 0);
		float camera_senkai = 0.04f;

		Vector3 camera_look = Vector3.Slerp(m_camera.transform.forward,camera_horizontal_direction,camera_senkai);
		camera_look = Vector3.Slerp(camera_look,camera_vertical*transform.up ,camera_senkai);

		//m_camera.transform.position = transform.position + camera_height - 7 * camera_look.normalized;//プレーヤーのローカル座標(0,3,-7)へ移動

		Vector3 slerp_pos = Vector3.Slerp (m_camera.transform.position, transform.position + camera_height - 7 * camera_look.normalized, 0.1f);
		Vector3 next_pos = transform.position + camera_height - 7 * camera_look.normalized;
		m_camera.transform.position = new Vector3 (next_pos.x, slerp_pos.y, next_pos.z);

		m_camera.transform.LookAt (m_camera.transform.position + camera_look);



		//■ 壁張り付き
		//・壁に接している時
		//・移動ベクトルが下に向いた時
		//・減速率が働きゆっくり落ちる
		//・接してるオブジェクトによって壁張り付きできるものと分けるのもいいかも
		float realvel = ( (transform.position - latest_pos) / Time.deltaTime ).magnitude;
		latest_pos = transform.position;
		if (nearWall) {
			Vector3 a = new Vector3 (0, 0, 0);
			float b = 20f; //減速率

			if (realvel < 1f)
				direction = direction / 1.2f;

			if (direction.y < 0)
				a = -b * direction;

			direction += a * Time.deltaTime;
		}



		//アニメーション
		anim.SetFloat("velocity", direction.magnitude);


	}

	void OnCollisionEnter(Collision col) {
		/*
		if (!isLocalPlayer) 
			return;
		*/

		/*
		Debug.Log (col.gameObject.tag);
		if (col.gameObject.tag == "Stage") 
			direction = new Vector3 (0, 0, 0);
		*/
	}

	/*
	void OnCollisionStay(Collision col) {
		if (col.gameObject.tag != "Stage") 
			direction = direction / 1.05f;
	}
	*/

	void FixedUpdate () {
		/*
		if (!isLocalPlayer) 
			return;
		*/

		velocity = direction.magnitude;

		float _Angle = Vector3.Angle(direction, inclination); //進行方向と地面との角度
		Vector3 cor_direction = direction.normalized + new Vector3 (0, Mathf.Tan ((_Angle - 90f) / 180f), 0); //補正込みの実際の移動ベクトル

		transform.position = Vector3.MoveTowards (transform.position, transform.position + cor_direction, velocity * Time.deltaTime);

	}


	//アイテム取得
	void GetItem(Item itemobj){
		switch (itemobj.itemType) {
		case Item.ItemType.Item:
			ItemList.Add (itemobj);
			break;
		case Item.ItemType.Accessory:
			AccessoryList.Add (itemobj);
			break;
		case Item.ItemType.Weapon:
			WeaponList.Add (itemobj);
			break;
		case Item.ItemType.Dress:
			DressList.Add (itemobj);
			break;
		default:
			break;
		}
	}

	void OnTriggerStay(Collider col) {
		if (col.gameObject.tag == "Item") {
			if (Input.GetKeyDown (KeyCode.L)) {
				GetItem( col.gameObject.GetComponent<Item_Object>().item );
				Destroy (col.gameObject);
			}
		}
	}

	/*
	[ClientRpc]
	public void RpcForce(NetworkInstanceId id, Vector3 force) {
		if (!isLocalPlayer && my_status.id != id)
			return;
		direction += force;
	}
	*/


}
