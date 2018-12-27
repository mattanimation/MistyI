using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Misty;

public class MistyUI : MonoBehaviour {

	public RectTransform debugUI;
	public Image curtain;
	public RectTransform MainPopup;
	public RectTransform choosePanel;
	public RectTransform connectPanel;

	public InputField IPAddressInput;
	public Text statusTxt;
	public Text simulatorURLTxt;

	public RectTransform APIExplorerView;
	public ColorPicker picker;


	// Use this for initialization
	void Start () {

		picker.onValueChanged.AddListener(this.OnColorChange);
	}
	
	// Update is called once per frame
	void Update () {

		//press d to toggle debug
		if (Input.GetKeyDown(KeyCode.D)) {
			ToggleDebugUI ();
		}

	}

	private void OnColorChange(Color color){
		
	}


	public void ToggleDebugUI(){
		if(this.debugUI != null)
			debugUI.gameObject.SetActive (!debugUI.gameObject.activeSelf);
	}

	public void ToggleAPIExplorerView(){
		if (this.APIExplorerView != null)
			APIExplorerView.gameObject.SetActive (!APIExplorerView.gameObject.activeSelf);
	}

	public void ConnectHandle(){
		if (!string.IsNullOrEmpty (IPAddressInput.text)) {
			MysticalMisty.Instance.core.Connect (IPAddressInput.text);
			//HideCurtain ();
		}
	}

	public void UseRobotHandle(){
		this.connectPanel.gameObject.SetActive (true);
		this.choosePanel.gameObject.SetActive (false);
	}

	public void UseSimulatorHandle(){
		MysticalMisty.Instance.core.Connect ("");
		//HideCurtain ();
	}
		
	public void ShowMainPopup(){
		MainPopup.gameObject.SetActive (true);
	}

	public void HideMainPopup(){
		MainPopup.gameObject.SetActive (false);
	}

	public void ShowCurtain(){
		curtain.enabled = true;
		curtain.CrossFadeAlpha (1.0f, 0.65f, false);	
	}

	public void HideCurtain(){
		curtain.CrossFadeAlpha (0.0f, 0.65f, false);
		Invoke("DisableCurtain", 0.66f);
	}


	private void DisableCurtain(){
		curtain.enabled = false;
	}

	//api tests

	public void TestHandle(){
		Misty.Logger.Log ("testing");

		//example play audio clip
		/*
		MysticalMisty.Instance.api.PlayAudioClip ("EeeeeeE", (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
			string modJson = obj.rawJSON;
			modJson = modJson.Remove(modJson.IndexOf("["),1);
			modJson = modJson.Remove(modJson.IndexOf("]"),1);
			Debug.Log(modJson);
			AudioClipResult res = JsonUtility.FromJson<AudioClipResult>(modJson);
			if(res.status == "Success"){
				Debug.Log("!!!!" + res.result.fullPath);
			}
		});
		*/

		//example get images list then setting
		MysticalMisty.Instance.api.GetListOfImages ((APIResponse obj) => {
			Misty.Logger.Log(obj.rawJSON);

			string fix = Misty.Utils.RemoveBrackets(obj.rawJSON);
			ImageListResult ilr = JsonUtility.FromJson<ImageListResult>(fix);
			ImageAssetInfo iai = ilr.result[0];
			Debug.Log(iai.location);
			Debug.Log(iai.name);

			//show the first one found
			MysticalMisty.Instance.api.ChangeDisplayImage(iai.name, null);

		});

	}

}
