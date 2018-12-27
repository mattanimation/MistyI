using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Misty;
using UniWebServer;

public class MysticalMisty : Singleton<MysticalMisty> {
	protected MysticalMisty () {} // guarantee this will be always a singleton only - can't use the constructor!

	public string myGlobalVar = "whatever";

	public MistyCore core; //core has UI on it
	public MistyAPI api;

	public EmbeddedWebServerComponent simulatedServer;
	public SimulatedAPI simAPI;


}
	

