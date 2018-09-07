using Misty;

public class MysticalMisty : Singleton<MysticalMisty> {
	protected MysticalMisty () {} // guarantee this will be always a singleton only - can't use the constructor!

	public string myGlobalVar = "whatever";

	public MistyCore core; //core has UI on it
	public MistyAPI api;

}
	

