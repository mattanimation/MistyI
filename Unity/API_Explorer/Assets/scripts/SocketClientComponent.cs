using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Misty {

	public class SocketClientComponent : MonoBehaviour {

		public SocketClient sc;

		public string host = "";
		public int port = 80;

		// Use this for initialization
		void Start () {

			Logger.Log ("<color=blue>Starting SocketClient</color>");
			if (string.IsNullOrEmpty (host)) {
				host = Utils.GetIPAddress ();
			}
			sc = new SocketClient (host,port);
			sc.Setup ();
			sc.Start ();
		}

		// Update is called once per frame
		void Update () {

		}

		void OnApplicationQuit(){
			if (sc != null)
				sc.Stop ();
		}
	}
}