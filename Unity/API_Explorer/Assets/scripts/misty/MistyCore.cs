using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Networking;

namespace Misty {


	[RequireComponent(typeof(SocketClientComponent))]
	public class MistyCore : MonoBehaviour {
    

		// socket client for connection to real robot
		public SocketClientComponent scc;

		// ui for sim and real
        public APIExplorer ui;


		// Use this for initialization
		void Awake () {
			
			this.scc = GetComponent<SocketClientComponent> ();
			this.scc.enabled = false;

            this.ui = GetComponent<APIExplorer> ();

			//test api
			MysticalMisty.Instance.api = new MistyAPI(this);
			MysticalMisty.Instance.core = this;

			//simulation
            // not supported in explorer
		}

        /// <summary>
        /// Connect the specified host.
        /// </summary>
        /// <param name="host">Host.</param>
		public void Connect(string host){

            if (!string.IsNullOrEmpty(host))
            {
                MysticalMisty.Instance.api.HOST = host;
                //this.scc.enabled = true;

                try
                {
                    MysticalMisty.Instance.api.GetDeviceInformation((APIResponse resp) =>
                    {
                        Logger.Log(resp.rawJSON);
                        string fixedJson = resp.rawJSON.Substring(11, resp.rawJSON.Length - 32);
                        Logger.Log(fixedJson);
                        DeviceInfoResponse deviceInfo = JsonUtility.FromJson<DeviceInfoResponse>(fixedJson);
                        Logger.Log("ROBOT: " + deviceInfo.robotVersion);

                        MysticalMisty.Instance.core.ui.HideMainPopup();
                        //play a success noise
                        MysticalMisty.Instance.api.PlayAudioClip("EeeeeeE", null);

                    });
                }
                catch
                {
                    Debug.LogError("could not connect to robot");
                }
            }

		}

		public static void UIChange(string txt){
			//ui.statusTxt.text = txt;
		}

        /// <summary>
        /// Get the specified uri and callback.
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="uri">URI.</param>
        /// <param name="callback">Callback.</param>
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

        /// <summary>
        /// Post the specified uri, payload and callback.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="uri">URI.</param>
        /// <param name="payload">Payload.</param>
        /// <param name="callback">Callback.</param>
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

        /// <summary>
        /// Get the specified urlPath and callback.
        /// </summary>
        /// <param name="urlPath">URL path.</param>
        /// <param name="callback">Callback.</param>
		public void GET(string urlPath, System.Action<APIResponse> callback){
			Logger.Log ("need to get: " + urlPath);
			StartCoroutine(get (urlPath, callback));
		}

        /// <summary>
        /// Post the specified urlPath, payload and callback.
        /// </summary>
        /// <param name="urlPath">URL path.</param>
        /// <param name="payload">Payload.</param>
        /// <param name="callback">Callback.</param>
		public void POST(string urlPath, string payload, System.Action<APIResponse> callback){
			StartCoroutine(post(urlPath, payload, callback));
		}

	}

}
