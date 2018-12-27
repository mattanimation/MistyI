using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using UnityEngine;

/// <summary>
/// https://github.com/sta/websocket-sharp
/// </summary>

namespace Misty
{

	public class BaseWSBehavior : WebSocketBehavior {

		protected void OnError(object sender, ErrorEventArgs e){
			Logger.LogError (e);
		}


	}

	public class PubSub : BaseWSBehavior
	{
		protected override void OnMessage (MessageEventArgs e)
		{
			
			Logger.Log ("got PUB SUB message!");
			Logger.Log (e.Data.ToString ());
			PubSubMsg pMsg = JsonUtility.FromJson<PubSubMsg>(e.Data.ToString());
			//Logger.Log (pMsg.ts);
			//Logger.Log (pMsg.EventName);

			Send (JsonUtility.ToJson(pMsg));
		}
	}

	public class Echo : BaseWSBehavior
	{
		protected override void OnMessage (MessageEventArgs e)
		{
			Send (e.Data);
		}
	}

	public class SocketServer
	{
		private WebSocketServer wssv;

		public string host = "";
		public int port = 9005;

		public SocketServer(string _host, int _port){
			Network.InitializeSecurity();
			host = _host;
			port = _port;

			string socketPath = string.Format ("ws://{0}:{1}", host, port);
			Logger.Log ("attempting to connect to " + socketPath);
			wssv = new WebSocketServer (socketPath);

		}

		public void Setup(){
			
			wssv.AddWebSocketService<PubSub> ("/pubsub");
			wssv.AddWebSocketService<Echo> ("/echo");
		}

		private void HandleError(){
			
		}

		//public static void Main (string[] args)
		//{
			
			//wssv.Start ();
			//Console.ReadKey (true);
			//wssv.Stop ();
		//}

		public void Start(){
			wssv.Start ();
		}

		public void Stop(){
			wssv.Stop ();
		}
	}


}