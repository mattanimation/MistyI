using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using UnityEngine;

/// <summary>
/// https://github.com/sta/websocket-sharp
/// </summary>

namespace Misty
{
		
	public class SocketClient{

		private WebSocket ws;

		public string host = "";
		public int port = 9005;

		public SocketClient(string _host, int _port){
			Network.InitializeSecurity();
			host = _host;
			port = _port;

			string socketPath = string.Format ("ws://{0}:{1}", host, port);
			Logger.Log ("attempting to connect to " + socketPath);
			ws = new WebSocket (socketPath);

		}

		public void Setup(){

			//wssv.AddWebSocketService<PubSub> ("/pubsub");
			//wssv.AddWebSocketService<Echo> ("/echo");

			ws.OnOpen += this.HandleOnOpen;
			ws.OnClose += this.HandleOnClose;
			ws.OnError += this.HandleOnError;
			ws.OnMessage += this.HandleOnMessage;
		}

		private void HandleOnError(object sender, ErrorEventArgs e){

		}

		private void HandleOnClose(object sender, CloseEventArgs e){
			
		}

		private void HandleOnOpen(object sender, EventArgs e){
			
		}

		private void HandleOnMessage(object sender, MessageEventArgs e){
			
		}

		//public static void Main (string[] args)
		//{

		//wssv.Start ();
		//Console.ReadKey (true);
		//wssv.Stop ();
		//}

		public void Start(){
			//wssv.Start ();
			ws.Connect();
		}

		public void Stop(){
			//wssv.Stop ();
			ws.Close();
		}
	}
}