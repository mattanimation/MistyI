using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UniWebServer;

namespace Misty {


	[RequireComponent(typeof(SocketServerComponent))]
	[RequireComponent(typeof(SocketClientComponent))]
	[RequireComponent(typeof(MistyUI))]
	public class MistyCore : MonoBehaviour {

		// socket server for sim
		public SocketServerComponent ssc;

		// socket client for connection to real robot
		public SocketClientComponent scc;

		// ui for sim and real
		public MistyUI ui;

		// 
		public GameObject simulation;

		public MistyPhysical physical;

		public MistySim simulatedState;

		// Use this for initialization
		void Awake () {

			this.ssc = GetComponent<SocketServerComponent> ();
			this.ssc.enabled = false;
			this.scc = GetComponent<SocketClientComponent> ();
			this.scc.enabled = false;

			this.ui = GetComponent<MistyUI> ();

			//test api
			MysticalMisty.Instance.api = new MistyAPI(this);
			MysticalMisty.Instance.core = this;

			//simulation
			MysticalMisty.Instance.simulatedServer = simulation.GetComponent<EmbeddedWebServerComponent>();
			MysticalMisty.Instance.simAPI = simulation.GetComponent<SimulatedAPI> ();

			this.simulatedState = new MistySim ();
		}


		public void Connect(string host){

			if (!string.IsNullOrEmpty (host)) {
				MysticalMisty.Instance.api.HOST = host;
				//this.scc.enabled = true;

				try{
					MysticalMisty.Instance.api.GetDeviceInformation ((APIResponse resp) => {
						Logger.Log (resp.rawJSON);
						string fixedJson = resp.rawJSON.Substring (11, resp.rawJSON.Length - 32);
						Logger.Log (fixedJson);
						DeviceInfoResponse deviceInfo = JsonUtility.FromJson<DeviceInfoResponse> (fixedJson);
						Logger.Log ("ROBOT: " + deviceInfo.robotVersion);

						MysticalMisty.Instance.core.ui.HideCurtain();
						MysticalMisty.Instance.core.ui.HideMainPopup();
						//play a success noise
						MysticalMisty.Instance.api.PlayAudioClip ("EeeeeeE", null);

					});
				} catch{
					Debug.LogError ("could not connect to robot");
				}

			} else {
				//do simulator
				// TODO : build out simulator more
				MysticalMisty.Instance.simulatedServer.StartServer();
				string hostURL = String.Format ("http://localhost:{0}", MysticalMisty.Instance.simulatedServer.port);
				MysticalMisty.Instance.core.ui.simulatorURLTxt.text = String.Format("Connect to: {0}", hostURL);
				Logger.Log (String.Format ("test connect at: {0}/api/info/help", hostURL));
				this.ssc.enabled = true;
				MysticalMisty.Instance.api.HOST = "localhost"; //this.scc.host;
				MysticalMisty.Instance.api.PORT = MysticalMisty.Instance.simulatedServer.port;
				MysticalMisty.Instance.core.ui.HideCurtain();
				MysticalMisty.Instance.core.ui.HideMainPopup();
			}
		}

		public static void UIChange(string txt){
			//ui.statusTxt.text = txt;
		}

		private IEnumerator get(string uri, Action<APIResponse> callback){
			UnityWebRequest www = UnityWebRequest.Get (uri);

			yield return www.SendWebRequest ();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log (www.error);
			} else {
				if (www.downloadHandler.isDone) {
					if (www.responseCode == 200) {
						if(callback != null)
							callback (new APIResponse (www.downloadHandler.text));
					}
				} else {
					Logger.LogError ("warning not done");
				}
			}
		}

		private IEnumerator post( string uri, string payload, Action<APIResponse> callback){

			//works :shrug:
			UnityWebRequest www = new UnityWebRequest(uri);
			www.useHttpContinue = false;
			www.chunkedTransfer = false;
			www.method = UnityWebRequest.kHttpVerbPOST;
			UploadHandler uploader = new UploadHandlerRaw(new System.Text.UTF8Encoding().GetBytes(payload));
			//uploader.contentType = "application/x-www-form-urlencoded";
			www.uploadHandler = uploader;
			www.downloadHandler = new DownloadHandlerBuffer ();

			yield return www.SendWebRequest();

			if(www.isNetworkError || www.isHttpError) {
				Debug.Log(www.error);
			}
			else {
				// Show results as text
				//Debug.Log(www.downloadHandler.text);
				// Or retrieve results as binary data
				//byte[] results = www.downloadHandler.data;
				APIResponse resp = new APIResponse(www.downloadHandler.text);
				if(callback != null)
					callback(resp);
			}


		}

		public void GET(string urlPath, System.Action<APIResponse> callback){
			Logger.Log ("need to get: " + urlPath);
			StartCoroutine(get (urlPath, callback));
		}

		public void POST(string urlPath, string payload, System.Action<APIResponse> callback){
			StartCoroutine(post(urlPath, payload, callback));
		}

	}

}
