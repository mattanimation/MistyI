/// <summary>
/// Author: Matt Murray (@station)
/// 
/// All the data schemas for Misty
/// Socket
/// API
/// Etc
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Misty{

	public enum SensorType
	{
		DEFAULT,
		TOF,
		CAMERA,
		STRUCTURE,
		GPS,
		AXL,
		GYO,
		MAG
	}

	public enum MsgType {
		Default,
		TimeOfFlight
	}

	public enum SensorPosition{
		Default,
		FRONT_LEFT,
		FRONT_RIGHT,
		FRONT_MIDDLE,
		REAR
	}

	[System.Serializable]
	public class MsgPayload {
		public string created;
		public string expiry;

		public MsgPayload (){
			this.created = new DateTime ().ToUniversalTime ().ToString ();
			this.expiry = new DateTime ().ToUniversalTime ().ToString ();
		}
	}

	[System.Serializable]
	public class MistyMsg {
		
		public string eventName;
		public MsgType type;
		public MsgPayload Message;

		public MistyMsg(){
			this.eventName = "default";
			this.type = MsgType.Default;
			this.Message = new MsgPayload();
		}
	}

	/// <summary>
	/// TOF payload
	/// 
	/// </summary>
	[System.Serializable]
	public class TOFPayload: MsgPayload {
		public string sensorPosition;
		public string sensorId;
		public string distanceInMeters;
		public TOFPayload(){
			this.sensorPosition = "";
			this.sensorId = "";
			
		}
	}

	[System.Serializable]
	public class TOFData {
		public string sensorPosition;
		public string sensorId;
		public float distanceInMeters;

		public TOFData(){
			this.sensorPosition = "";
			this.sensorId = "";
			this.distanceInMeters = 0;

		}
	}



	/// <summary>
	/// Time of flight message.
	/// </summary>
	[System.Serializable]
	public class TOfFMsg: MistyMsg{
		public TOFPayload Message;
		public TOfFMsg(){
			this.Message = new TOFPayload ();
		}
	}

	[System.Serializable]
	public class PubSubMsg: MistyMsg {

		public string Operation;
		public string Type;
		public int DebounceMS;
		public string ReturnProperty;

		public PubSubMsg(){
			this.Operation = "";
			this.Type = "";
			this.DebounceMS = 0;
			this.ReturnProperty = "";
		}
	}

	// === === //
	[System.Serializable]
	public class Sensor {
		public SensorType type;
		public Sensor(){
			this.type = SensorType.DEFAULT;
		}
	}
	[System.Serializable]
	public class TOFSensor: Sensor {
		public float distance;

		public TOFSensor(){
			this.type = SensorType.TOF;
			this.distance = 0.0f;
		}
	}

	[System.Serializable]
	public class MistyMood {
		public float valence;
		public float arousal;
		public float dominance;

		public MistyMood(){
			this.valence = 0;
			this.arousal = 0;
			this.dominance = 0;
		}

		public MistyMood(float v, float a, float d){
			this.valence = v;
			this.arousal = a;
			this.dominance = d;
		}
	}

	[System.Serializable]
	public class MistyDriveTime {
		public float linear;
		public float angular;
		public float degrees;

		public MistyDriveTime(){
			this.linear = 0;
			this.angular = 0;
			this.degrees = 0;
		}

		public MistyDriveTime(float l, float a, float m){
			this.linear= l;
			this.angular = a;
			this.degrees = m;
		}
	}

	[System.Serializable]
	public class MistyDrive {
		public float linear;
		public float angular;

		public MistyDrive(){
			this.linear = 0;
			this.angular = 0;
		}

		public MistyDrive(float l, float a){
			this.linear= l;
			this.angular = a;
		}
	}

	[System.Serializable]
	public class MistyDriveLocation {
		public int x;
		public int y;

		public MistyDriveLocation(){
			this.x = 0;
			this.y = 0;
		}

		public MistyDriveLocation(int x, int y){
			this.x = x;
			this.y = y;
		}
	}

	[System.Serializable]
	public class MistyLocomotion {
		public float leftSpeed;
		public float rightSpeed;

		public MistyLocomotion(){
			this.leftSpeed = 0;
			this.rightSpeed = 0;
		}

		public MistyLocomotion(float l, float r){
			this.leftSpeed = l;
			this.rightSpeed = r;
		}
	}



	// === === //


	// ======== API DATA SCHEMAS ========== //

	[System.Serializable]
	public class APIResult{

		public string status;

		public APIResult(){
			this.status = "Success";
		}

		public string ToJSON(){
			return JsonUtility.ToJson (this);
		}
	}

	[System.Serializable]
	public class AudioClipResultData {
		public string audioClipId;
		public string audioClips;
		public string created;
		public string fullPath;

		public AudioClipResultData(){
			this.audioClipId = "";
			this.audioClips = "";
			this.created = "";
			this.fullPath = "";
		}
	}

	[System.Serializable]
	public class AudioClipResult: APIResult{

		public AudioClipResultData result;
		public string status;
		//[{"result":{"audioClipId":"7A71B73C","audioClips":null,"created":"2018-05-09T06:06:36.549701Z","fullPath":"C:\\Data\\Users\\DefaultAccount\\Music\\Curious\\001-EeeeeeE.wav"},"status":"Success"}]
		public AudioClipResult(){
			this.result = new AudioClipResultData();
			this.status = "Success";
		}
	}

	[System.Serializable]
	public class APIPayload {
		public APIPayload(){
		}

		public static APIPayload fromJSON(string json){
			return JsonUtility.FromJson<APIPayload> (json);
		}

		public string ToJSON(){
			return JsonUtility.ToJson (this);
		}
	}

	[System.Serializable]
	public class AudioClipInfo{
		public float duration;
		public string location;
		public string name;
		public bool userAddedAsset;

		public AudioClipInfo(){
			this.duration = 0.0f;
			this.location = null;
			this.name = "";
			this.userAddedAsset = false;
		}

		public AudioClipInfo(float duration, string location, string name, bool userAddedAsset){
			this.duration = duration;
			this.location = location;
			this.name = name;
			this.userAddedAsset = userAddedAsset;
		}

		public string toJSON(){
			return JsonUtility.ToJson (this);
		}
	}

	[System.Serializable]
	public class AudioClipsList: APIResult {
		public List<AudioClipInfo> result;

		public AudioClipsList(){
			this.result = new List<AudioClipInfo>();
			this.status = "Success";
		}
	}

	[System.Serializable]
	public class AudioPlayPayload: APIPayload {
		public string AssetId;
		public AudioPlayPayload(){
			this.AssetId = "";
		}
		public AudioPlayPayload(string assetId){
			this.AssetId = assetId;
		}
	}

	[System.Serializable]
	public class AudioDeletePayload: APIPayload {
		public string FileName;
		public AudioDeletePayload(){
			this.FileName = "";
		}
		public AudioDeletePayload(string fileName){
			this.FileName = fileName;
		}
	}

	/// <summary>
	/// Audio save payload.
	///   fileName - String  - Name of audio file uploaded to Misty with file extention (Accepts all audio format types)
	///   audioData           - String  - Series of numbers that represent audio data in bytes
	///   immediatelyApply    - Boolean - True or False used to indicate whether audio file will be immediately updated to audio list.
	/// </summary>
	[System.Serializable]
	public class AudioSavePayload: APIPayload {
		public string FileName;
		public string DataAsByteArrayString;
		public bool ImmediatelyApply;
		public bool OverwriteExisting;

		public AudioSavePayload(){
			this.FileName = "";
			this.DataAsByteArrayString = "";
			this.ImmediatelyApply = false;
			this.OverwriteExisting = false;
		}

		public AudioSavePayload(string fileName, byte[] data, bool immediatelyApply, bool overwriteExisting){
			this.FileName = fileName;
			this.DataAsByteArrayString = Misty.Utils.ByteArrayToStringArray(data);
			this.ImmediatelyApply = immediatelyApply;
			this.OverwriteExisting = overwriteExisting;
		}


	}

	//eyes
	[System.Serializable]
	public class ChangeEyesPayload: APIPayload {
		public float Valence;
		public float Arousal;
		public float Dominance;

		public ChangeEyesPayload(){
			this.Valence = 0;
			this.Arousal = 0;
			this.Dominance = 0;
		}

		public ChangeEyesPayload(MistyMood mood){
			this.Valence = mood.valence;
			this.Arousal = mood.arousal;
			this.Dominance = mood.dominance;
		}

		public ChangeEyesPayload(float v, float a, float d){
			this.Valence = v;
			this.Arousal = a;
			this.Dominance = d;
		}
	}

	//motion
	[System.Serializable]
	public class DrivePayload: APIPayload {
		public float LinearVelocity;
		public float AngularVelocity;

		public DrivePayload(){
			this.LinearVelocity = 0;
			this.AngularVelocity = 0;
		}

		public DrivePayload(MistyDrive md){
			this.LinearVelocity = md.linear;
			this.AngularVelocity = md.angular;
		}

		public DrivePayload(float v, float a){
			this.LinearVelocity = 0;
			this.AngularVelocity = 0;
		}
	}

	[System.Serializable]
	public class DriveTimePayload: APIPayload {
		public float LinearVelocity;
		public float AngularVelocity;
		public float Degrees;
		public float TimeMS;

		public DriveTimePayload(){
			this.LinearVelocity = 0;
			this.AngularVelocity = 0;
			this.Degrees = 0;
			this.TimeMS = 0;
		}

		public DriveTimePayload(MistyDriveTime dt, float t){
			this.LinearVelocity = dt.linear;
			this.AngularVelocity = dt.angular;
			this.Degrees = dt.degrees;
			this.TimeMS = t;
		}

		public DriveTimePayload(float v, float a, float d, float t){
			this.LinearVelocity = v;
			this.AngularVelocity = a;
			this.Degrees = d;
			this.TimeMS = t;
		}
	}

	[System.Serializable]
	public class DriveLocationPayload: APIPayload {
		public int X;
		public int Y;

		public DriveLocationPayload(){
			this.X = 0;
			this.Y = 0;
		}

		public DriveLocationPayload(MistyDriveLocation dl){
			this.X = dl.x;
			this.Y = dl.y;
		}

		public DriveLocationPayload(int x, int y){
			this.X = x;
			this.Y = y;
		}
	}
		
	[System.Serializable]
	public class LocomotionPayload: APIPayload {
		public float LeftTrackSpeed;
		public float RightTrackSpeed;

		public LocomotionPayload(){
			this.LeftTrackSpeed = 0;
			this.RightTrackSpeed = 0;
		}

		public LocomotionPayload(MistyLocomotion dl){
			this.LeftTrackSpeed = dl.leftSpeed;
			this.RightTrackSpeed = dl.rightSpeed;
		}

		public LocomotionPayload(float l, float r){
			this.LeftTrackSpeed = l;
			this.RightTrackSpeed = r;
		}
	}

	[System.Serializable]
	public class MistyArmData {
		public string arm;
		public float position;
		public float velocity;
		public MistyArmData(){
			this.arm = "";
			this.position = 0;
			this.velocity = 0;
		}
	}

	[System.Serializable]
	public class ArmPayload: APIPayload {
		public string Arm;
		public float Position;
		public float Velocity;

		public ArmPayload(){
			this.Arm = "";
			this.Position = 0;
			this.Velocity = 0;
		}

		public ArmPayload(MistyArmData ad){
			this.Arm = ad.arm;
			this.Position = ad.position;
			this.Velocity = ad.velocity;
		}

		public ArmPayload(string a, float p, float v){
			this.Arm = a;
			this.Position = p;
			this.Velocity = v;
		}
	}
		

	[System.Serializable]
	public class MistyHeadMoveData {
		public float pitch;
		public float roll;
		public float yaw;
		public float velocity;

		public MistyHeadMoveData(){
			this.pitch = 0;
			this.roll = 0;
			this.yaw = 0;
			this.velocity = 0;
		}
	}

	[System.Serializable]
	public class HeadMovePayload: APIPayload {
		public float Pitch; //-5 to 5
		public float Roll; //-5 to 5
		public float Yaw; //-5 to 5
		public float Velocity; //0 to 10

		public HeadMovePayload(){
			this.Pitch = 0;
			this.Roll = 0;
			this.Yaw = 0;
			this.Velocity = 0;
		}

		public HeadMovePayload(MistyHeadMoveData hmd){
			this.Pitch = hmd.pitch;
			this.Roll = hmd.roll;
			this.Yaw = hmd.yaw;
			this.Velocity = hmd.velocity;
		}

		public HeadMovePayload(float p, float r, float y, float v){
			this.Pitch = p;
			this.Roll = r;
			this.Yaw = y;
			this.Velocity = v;
		}

		public static HeadMovePayload fromJSON(string json){
			return JsonUtility.FromJson<HeadMovePayload> (json);
		}
	}

	[System.Serializable]
	public class MistyHeadLocationData {
		public float location;
		public float velocity;

		public MistyHeadLocationData(){
			this.location = 0;
			this.velocity = 0;
		}
	}

	[System.Serializable]
	public class HeadLocationPayload: APIPayload {
		public float Location;
		public float Velocity;

		public HeadLocationPayload(){
			this.Location = 0;
			this.Velocity = 0;
		}

		public HeadLocationPayload(MistyHeadLocationData hld){
			this.Location = hld.location;
			this.Velocity = hld.velocity;
		}

		public HeadLocationPayload(float l, float v){
			this.Location = l;
			this.Velocity = v;
		}
	}

	[System.Serializable]
	public class MistyHeadPositionData {
		public string axis;
		public float position;
		public float velocity;

		public MistyHeadPositionData(){
			this.axis = "";
			this.position = 0;
			this.velocity = 0;
		}
	}

	[System.Serializable]
	public class HeadPositionPayload: APIPayload {
		public string Axis;
		public float Position;
		public float Velocity;

		public HeadPositionPayload(){
			this.Axis = "";
			this.Position = 0;
			this.Velocity = 0;
		}

		public HeadPositionPayload(MistyHeadPositionData hpd){
			this.Axis = hpd.axis;
			this.Position = hpd.position;
			this.Velocity = hpd.velocity;
		}

		public HeadPositionPayload(string a, float p, float v){
			this.Axis = a;
			this.Position = p;
			this.Velocity = v;
		}
	}

	[System.Serializable]
	public class ImageAssetInfo {
		//{"height":270.0,"location":"C:\\data\\Programs\\WindowsApps\\MistyRobotics.Misty1HomeRobotCAN_1.1.6.0_arm__gand6cah5b678\\Assets\\Media\\Visual\\LowRes\\Angry.jpg","name":"Angry.jpg","width":480.0}
		public int height;
		public int width;
		public string location;
		public string name;

		private string mistyImageRootDir = "C:\\data\\Programs\\WindowsApps\\MistyRobotics.Misty1HomeRobotCAN_1.1.6.0_arm__gand6cah5b678\\Assets\\Media\\Visual\\LowRes\\";

		public ImageAssetInfo(){
			this.height = 270;
			this.width = 480;
			this.location = "";
			this.name = "";
		}

		public ImageAssetInfo(int height, int width, string location, string name){
			this.height = height;
			this.width = width;
			if (location == null)
				location = mistyImageRootDir;
			this.location = string.Format("{0}/{1}", location, name);
			this.name = name;
		}

		public string toJSON(){
			return JsonUtility.ToJson(this);
		}

		public static ImageAssetInfo FromJSON(string json){
			return JsonUtility.FromJson<ImageAssetInfo> (json);
		}

	}

	[System.Serializable]
	public class ImageListResult: APIResult {

		public List<ImageAssetInfo> result;

		public ImageListResult(){
			this.result = new List<ImageAssetInfo> ();
			this.status = "Success";
		}

		public static ImageListResult FromJSON(string json){
			return JsonUtility.FromJson<ImageListResult> (json);
		}
	}

	[System.Serializable]
	public class MistyImageData {
		public string fileName;
		public byte[] dataAsByteArray;
		public int width;
		public int height;
		public bool immediatelyApply;
		public bool overwriteExisting;

		public MistyImageData(){
			this.fileName = "";
			this.dataAsByteArray = new byte[0];
			this.width = 0;
			this.height = 0;
			this.immediatelyApply = false;
			this.overwriteExisting = false;
		}
	}

	[System.Serializable]
	public class ImageDataPayload: APIPayload {
		public string FileName;
		public string DataAsByteArrayString;
		public int Width;
		public int Height;
		public bool ImmediatelyApply;
		public bool OverwriteExisting;

		public ImageDataPayload(){
			this.FileName = "";
			this.DataAsByteArrayString = "";
			this.Width = 0;
			this.Height = 0;
			this.ImmediatelyApply = false;
			this.OverwriteExisting = false;
		}

		public ImageDataPayload(MistyImageData mid){
			this.FileName = mid.fileName;
			this.DataAsByteArrayString = Misty.Utils.ByteArrayToStringArray(mid.dataAsByteArray);
			this.Width = mid.width;
			this.Height = mid.height;
			this.ImmediatelyApply = mid.immediatelyApply;
			this.OverwriteExisting = mid.overwriteExisting;
		}

		public ImageDataPayload(string f, byte[] d, int w, int h, bool i, bool o){
			this.FileName = f;
			this.DataAsByteArrayString = Misty.Utils.ByteArrayToStringArray(d);
			this.Width = w;
			this.Height = h;
			this.ImmediatelyApply = i;
			this.OverwriteExisting = o;
		}
	}

	// === images ===
	[System.Serializable]
	public class ImageAssetPayload: APIPayload {
		public string FileName;
		public ImageAssetPayload(){
			this.FileName = "";

		}
	}



	[System.Serializable]
	public class WiFiPayload: APIPayload{
		public string NetworkName;
		public string Password;
		public WiFiPayload(){
			this.NetworkName = "";
			this.Password = "";
		}
		public WiFiPayload(string networkName, string password){
			this.NetworkName = networkName;
			this.Password = password;
		}
	}

	/// <summary>
	/// LED payload. for when sending or recieving led data in the API
	/// </summary>
	[System.Serializable]
	public class LEDPayload: APIPayload{
		public uint Red;
		public uint Green;
		public uint Blue;

		public LEDPayload(){
			this.Red = 0;
			this.Green = 0;
			this.Blue = 0;
		}

		public LEDPayload(uint _red, uint _green, uint _blue){
			this.Red = _red;
			this.Green = _green;
			this.Blue = _blue;
		}

		public static LEDPayload fromJSON(string json){
			return JsonUtility.FromJson<LEDPayload> (json);
		}
			
	}

	/// <summary>
	/// LED data. for the simulated state of the LED
	/// </summary>
	[System.Serializable]
	public class LEDData {
		
		public uint Red;
		public uint Green;
		public uint Blue;

		public LEDData(){
			this.Red = 0;
			this.Green = 0;
			this.Blue = 0;
		}
		public LEDData(uint _red, uint _green, uint _blue){
			this.Red = _red;
			this.Green = _green;
			this.Blue = _blue;
		}

		public static LEDData RedColor(){
			return new LEDData(255, 0, 0);
		}

		public static LEDData BlueColor(){
			return new LEDData (0, 0, 255);
		}

		public static LEDPayload fromJSON(string json){
			return JsonUtility.FromJson<LEDPayload> (json);
		}

	}


	[System.Serializable]
	public class APIResponse{
		public object result;
		public string status;
		public string rawJSON;

		public APIResponse(){
			this.rawJSON = "";
			this.status = "";
			this.result = null;
		}

		public APIResponse(string jsonString){
			this.rawJSON = jsonString;
			this.status = "";
			this.result = null;
		}
	}

	[System.Serializable]
	public class HardwareInfo{
		public HardwareInfo(){
		}
	}

	[System.Serializable]
	public class DeviceInfoResponse{

		public string currentProfileName;
		public HardwareInfo hardwareInfo;
		public string ipAddress;
		public string networkConnectivity;
		public string[] outputCapabilities;
		public string sensoryServiceAppVersion;
		public string[] sensorCapabilities;
		public string serialNumber;
		public string robotVersion;
		public string windowsOSVersion;

		public DeviceInfoResponse(){
			this.currentProfileName = "";
			this.hardwareInfo = new HardwareInfo ();
			this.ipAddress = "";
			this.networkConnectivity = "";
			this.outputCapabilities = new string[1];
			this.sensorCapabilities = new string[1];
			this.sensoryServiceAppVersion = "";
			this.windowsOSVersion = "";
		}
	}



}
