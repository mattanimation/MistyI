using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using Misty;

/*
 * Simple class to manage the API GUI functions and connect stuff
 * */
public class APIExplorer : MonoBehaviour {

	public RectTransform debugUI;
	public RectTransform MainPopup;
	public RectTransform connectPanel;

	public InputField IPAddressInput;
	public Text statusTxt;

	public RectTransform APIExplorerView;
	public ColorPicker picker;

    public MistyCore core;


	// Use this for initialization
	void Start () {

        picker.onValueChanged.AddListener(this.OnColorChange);

        MysticalMisty.Instance.core = this.core;

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

	/// <summary>
	/// Connects to Misty if IP is valid
	/// </summary>
	public void ConnectHandle(){
		if (!string.IsNullOrEmpty (IPAddressInput.text)) {
			
			MysticalMisty.Instance.api.HOST = IPAddressInput.text;
			//this.scc.enabled = true;

			try{
				MysticalMisty.Instance.api.GetDeviceInformation ((APIResponse resp) => {
					Misty.Logger.Log (resp.rawJSON);
					string fixedJson = resp.rawJSON.Substring (11, resp.rawJSON.Length - 32);
					Misty.Logger.Log (fixedJson);
					DeviceInfoResponse deviceInfo = JsonUtility.FromJson<DeviceInfoResponse> (fixedJson);
					Misty.Logger.Log ("ROBOT: " + deviceInfo.robotVersion);

					HideMainPopup();
					//play a success noise
					MysticalMisty.Instance.api.PlayAudioClip ("EeeeeeE", null);

				});
			} catch{
				Debug.LogError ("could not connect to robot");
				statusTxt.text = "could not connect to robot";
			}
		}
	}

	/// <summary>
	/// Shows the main popup.
	/// </summary>
	public void ShowMainPopup(){
		MainPopup.gameObject.SetActive (true);
	}

	/// <summary>
	/// Hides the main popup.
	/// </summary>
	public void HideMainPopup(){
		MainPopup.gameObject.SetActive (false);
	}


	//api tests

	/// <summary>
	/// Tests the handle.
	/// </summary>
	public void TestHandle(){
		Misty.Logger.Log ("testing");

		//feel free to add to these examples of usage

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

