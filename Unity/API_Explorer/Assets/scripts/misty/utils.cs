using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;

namespace Misty{

	public static class Utils{

		public static string GetIPAddress()
		{
			IPHostEntry host;
			string localIP = "";
			host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (IPAddress ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					localIP = ip.ToString();
				}

			}
			return localIP;
		}

		public static string RemoveBrackets(string input){
			string ns = ""+input;
			ns = ns.Remove(0,1);
			ns = ns.Remove(ns.Length-1,1);
			return ns;
		}

		public static string ByteArrayToStringArray(byte[] bArr){
			string[] dsa = new string[bArr.Length];
			for (long i = 0; i < bArr.Length; i++) {
				dsa.SetValue (bArr [i].ToString(), i);
			}
			return String.Join (",", dsa);
		}

		public static float Remap (this float from, float fromMin, float fromMax, float toMin,  float toMax)
		{
			var fromAbs  =  from - fromMin;
			var fromMaxAbs = fromMax - fromMin;      

			var normal = fromAbs / fromMaxAbs;

			var toMaxAbs = toMax - toMin;
			var toAbs = toMaxAbs * normal;

			var to = toAbs + toMin;

			return to;
		}
	}

	public static class Logger{

		public static bool enabled = true;

		private static string _name ="MISTY";

		public static void Log(object value){
			if (Logger.enabled) {
				Debug.Log (string.Format("<color=purple>{0}</color>: {1}", _name, value));
			}
			
		}

		public static void LogError(object value){
			if (Logger.enabled) {
				Debug.LogError (string.Format ("<color=red>ERRROR</color><color=purple>{0}</color>{1}", _name, value));
			}
		}
	}


}
