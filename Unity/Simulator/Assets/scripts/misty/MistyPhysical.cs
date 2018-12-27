using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Misty;

public class MistyPhysical : MonoBehaviour {

	public GameObject body;
	public GameObject led;
	public GameObject head;
	public GameObject leftWheel;
	public GameObject rightWheel;
	public GameObject arms;

	//front TOF sensors
	public TOFSensor fl_tof;
	public TOFSensor fm_tof;
	public TOFSensor fr_tof;

	//rear TOF
	public TOFSensor rm_tof;

	//screen
	public GameObject screen;

	private Quaternion oldRot;
	private Quaternion newRot;

	private float rotSpeed;
	private bool _canMove = false;
	private float _moveTime = 2f;
	private float _moveEndTime = 0;
	private float _timeMotion = 0;


	// SIMULATED ROBOT STATE PROPS
	private LEDPayload _led;
	public LEDPayload LED{
		get{ return _led;}
		set{ _led = value;}
	}

	private HeadMovePayload _headMove;
	public HeadMovePayload HeadMove{
		get{ return _headMove; }
		set{ _headMove = value;}
	}


	// Use this for initialization
	void Start () {

		this.fl_tof = new TOFSensor ();
		this.fm_tof = new TOFSensor ();
		this.fr_tof = new TOFSensor ();
		this.rm_tof = new TOFSensor ();
		
	}

	void Update(){

		if (_canMove) {
			if (Time.time < _moveEndTime) {
				this.head.transform.localRotation = Quaternion.Slerp (oldRot, newRot, _timeMotion); //Quaternion.Lerp(oldRot, newRot, Time.deltaTime * float.Parse(hmp.Velocity.ToString()));
				_timeMotion += Time.deltaTime * (rotSpeed * 0.5f);

			} else {
				_canMove = false;
				_timeMotion = 0;
			}
		}
	}

	public void SetLED(Color col){
		Debug.Log ("setting led");
		Material lmat = led.GetComponent<Renderer> ().material;
		lmat.SetColor ("_Color", col);
	}


	public void MoveHead(HeadMovePayload hmp){
		oldRot = this.head.transform.localRotation;
		//normalize -5 to 5 to rotation range of head to -10(up) and 20 (down)
		float newVal = Utils.Remap (hmp.Pitch, -5, 5, -10, 20);
		Debug.Log ("NEW VAL! " + newVal.ToString ());
		newRot = Quaternion.Euler(newVal, oldRot.eulerAngles.y, oldRot.eulerAngles.z); //oldRot.eulerAngles.x
		Debug.Log(oldRot.eulerAngles);
		Debug.Log(newRot.eulerAngles);
		rotSpeed = hmp.Velocity;
		_canMove = true;
		_moveEndTime = Time.time + _moveTime;
	}

	public void ChangeImage(Texture2D tex){
		Renderer rndr = screen.GetComponent<Renderer> ();
		rndr.material.mainTexture = tex;
	}

}
