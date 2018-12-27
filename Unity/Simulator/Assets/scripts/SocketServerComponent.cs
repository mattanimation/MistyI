using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Misty {
	
	public class SocketServerComponent : MonoBehaviour {

		public SocketServer ss;

		public string host = "";
		public int port = 80;

		// Use this for initialization
		void Start () {
			
			Logger.Log ("<color=blue>Starting </color>");
			if (string.IsNullOrEmpty (host)) {
				host = Utils.GetIPAddress ();
			}
			ss = new SocketServer (host,port);
			ss.Setup ();
			ss.Start ();
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnApplicationQuit(){
			if (ss != null)
				ss.Stop ();
		}
	}
}
