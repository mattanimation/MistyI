/// <summary>
/// Author: Matt Murray (@station)
/// 
/// Misty API C# Implementation for Unity
/// 
/// </summary>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Misty{

	/// <summary>
	/// API map.
	/// a map to hold the api routes
	/// </summary>
	public struct APIMap{

		// audio
		public static string AUDIO = "audio";
		public static string AUDIO_CLIPS = string.Format("{0}/clips", AUDIO);
		public static string AUDIO_PLAY = string.Format("{0}/play", AUDIO);
		public static string AUDIO_DELETE = string.Format("{0}/delete", AUDIO);

		//general
		public static string SYSTEM = "system";
		public static string SYSTEM_UPDATE = string.Format("{0}/update", SYSTEM);
		public static string SENSOR = "sensorreadings";
		public static string INFO = "info";
		public static string WIFI = "wifi";
		public static string INFO_HELP = string.Format("{0}/help", INFO);
		public static string INFO_HELP_COMMAND = string.Format("{0}/help?commmand=", INFO);
		public static string INFO_BATTERY = string.Format("{0}/battery", INFO);
		public static string INFO_DEVICE = string.Format("{0}/device", INFO);
		public static string INFO_UPDATE = string.Format("{0}/updates", INFO);

		//eyes
		public static string EYES = "eyes";
		public static string EYES_CHANGE = string.Format("{0}/change", EYES);

		//move
		public static string DRIVE = "drive";
		public static string DRIVE_TIME = string.Format("{0}/time", DRIVE);
		public static string DRIVE_LOCATION = string.Format("{0}/location", DRIVE);
		public static string DRIVE_TRACK = string.Format("{0}/track", DRIVE);
		public static string ARMS = "arms";
		public static string HEAD = "head";
		public static string ARMS_MOVE = string.Format("{0}/move", ARMS);
		public static string HEAD_MOVE = string.Format("{0}/move", HEAD);
		public static string HEAD_LOCATION = string.Format("{0}/location", HEAD);
		public static string HEAD_POSITION = string.Format("{0}/position", HEAD);

		//slam
		public static string SLAM = "slam";
		public static string MAP = "map";
		public static string TRACK = "track";
		public static string RECORD = "record";

		public static string SLAM_STATUS = string.Format("{0}/status", SLAM);

		public static string SLAM_MAP_START = string.Format("{0}/{1}/start", SLAM, MAP);
		public static string SLAM_MAP_STOP = string.Format("{0}/{1}/stop", SLAM, MAP);
		public static string SLAM_RESET = string.Format("{0}/reset", SLAM);

		public static string SLAM_TRACK_START = string.Format("{0}/{1}/start", SLAM, TRACK);
		public static string SLAM_TRACK_STOP = string.Format("{0}/{1}/stop", SLAM, TRACK);

		public static string SLAM_RECORD_START = string.Format("{0}/{1}/start", SLAM, RECORD);
		public static string SLAM_RECORD_STOP = string.Format("{0}/{1}/stop", SLAM, RECORD);

		public static string DRIVE_PATH = string.Format("{0}/path", DRIVE);
		public static string DRIVE_STOP = string.Format("{0}/stop", DRIVE);

		public static string SLAM_MAP_SMOOTH = string.Format("{0}/{1}/smooth", SLAM, MAP);
		public static string SLAM_MAP_RAW = string.Format("{0}/{1}/raw", SLAM, MAP);

		public static string SLAM_PATH = string.Format("{0}/path", SLAM);

		//face
		public static string FACE = "faces";
		public static string DETECT = "detection";
		public static string RECOG = "recognition";
		public static string TRAIN = "training";

		public static string FACE_CLEAR = string.Format("{0}/clearall", FACE);

		public static string FACE_DETECT_START = string.Format("{0}/{1}/start", FACE, DETECT);
		public static string FACE_DETECT_STOP = string.Format("{0}/{1}/stop", FACE, DETECT);

		public static string FACE_RECOG_START = string.Format("{0}/{1}/start", FACE, RECOG);
		public static string FACE_RECOG_STOP = string.Format("{0}/{1}/stop", FACE, RECOG);

		public static string FACE_TRAIN_START = string.Format("{0}/{1}/start", FACE, TRAIN);
		public static string FACE_TRAIN_CANCEL = string.Format("{0}/{1}/cancel", FACE, TRAIN);

		//image
		public static string IMAGE = "images";
		public static string IMAGE_CHANGE = string.Format("{0}/change", IMAGE);
		public static string IMAGE_DELETE = string.Format("{0}/delete", IMAGE);
		public static string IMAGE_REVERT = string.Format("{0}/revert", IMAGE);


		//led
		public static string LED = "led";
		public static string LED_CHANGE = string.Format("{0}/change", LED);



	}

	/// <summary>
	/// Misty API.
	/// THe main API class to communicate with Misty
	/// </summary>
	public class MistyAPI {

		public const string VERSION = "0.1";

		private string _host;
		public string HOST {
			get{ return _host; }
			set{ 
				_host = value;
				this.update_urls ();
			}
		}

		private int _port;
		public int PORT {
			get{ return _port; }
			set{ 
				_port = value;
				this.update_urls ();
			}
		}

		private string root_uri = "";
		private string api_uri = "";
		private string beta_api_uri = "";
		private string alpha_api_uri = "";

		private MistyCore core;

		/// <summary>
		/// Initializes a new instance of the <see cref="Misty.MistyAPI"/> class.
		/// </summary>
		/// <param name="mc">MistyCore instance</param>
		public MistyAPI(MistyCore mc){
			this.core = mc;
			this.PORT = 80;
		}

		/// <summary>
		/// Updates the urls.
		/// </summary>
		private void update_urls(){
			this.root_uri = string.Format ("http://{0}:{1}/", HOST, PORT);
			this.api_uri = string.Format ("{0}api/", this.root_uri);// {0}/", VERSION);
			this.beta_api_uri = string.Format("{0}api/beta/", this.root_uri);
			this.alpha_api_uri = string.Format("{0}api/alpha/", this.root_uri);
		}

		/// <summary>
		/// Handle GET requests for the API
		/// </summary>
		/// <param name="urlPath">URL path.</param>
		/// <param name="callback">Callback.</param>
		/// <param name="apiState">API state.</param>
		private void GET(string urlPath, System.Action<APIResponse> callback, string apiState=""){
			if (this.core == null) {
				Debug.LogError ("You need to make sure MistyCore was passed in on instansiation");
				return;
			}

			string full_url = string.Format ("{0}{1}", this.api_uri, urlPath);
			if (!string.IsNullOrEmpty (apiState)) {
				if(apiState == "beta")
					full_url = string.Format ("{0}{1}", this.beta_api_uri, urlPath);
				else if(apiState == "alpha")
					full_url = string.Format ("{0}{1}", this.alpha_api_uri, urlPath);
			}
			Logger.Log("GETTING: " + full_url);
			this.core.GET(full_url, callback);
		}

		/// <summary>
		/// Handle POST requests for the API
		/// </summary>
		/// <param name="urlPath">URL path.</param>
		/// <param name="payload">Payload.</param>
		/// <param name="callback">Callback.</param>
		/// <param name="apiState">API state.</param>
		private void POST(string urlPath, string payload, System.Action<APIResponse> callback, string apiState=""){
			if (this.core == null) {
				Debug.LogError ("You need to make sure MistyCore was passed in on instansiation");
				return;
			}
			string full_url = string.Format ("{0}{1}", this.api_uri, urlPath);
			if (!string.IsNullOrEmpty (apiState)) {
				if(apiState == "beta")
					full_url = string.Format ("{0}{1}", this.beta_api_uri, urlPath);
				else if(apiState == "alpha")
					full_url = string.Format ("{0}{1}", this.alpha_api_uri, urlPath);
			}
			Logger.Log (string.Format("POSTING: {0} to {1}", payload, full_url));
			this.core.POST (full_url, payload, callback);
		}


		// ========== GENERAL =========== //

		/// <summary>
		/// Gets the string sensor values.
		/// </summary>
		/// <param name="callback">Action to take on complete.</param>
		public void GetStringSensorValues(Action<APIResponse> callback){
			GET (APIMap.SENSOR, callback);
		}

		/// <summary>
		/// Gets the help.
		/// </summary>
		/// <param name="callback">Action to take on complete.</param>
		public void GetHelp(Action<APIResponse> callback){
			GET (APIMap.INFO_HELP, callback);
		}

		/// <summary>
		/// Gets the help about command.
		/// </summary>
		/// <param name="commandString">the comman you want help info on</param>
		/// <param name="callback">Action to take on complete.</param>
		public void GetHelpAboutCommand(string commandString, Action<APIResponse> callback){
			GET (string.Format("{0}{1}",APIMap.INFO_HELP_COMMAND, commandString), callback);
		}

		/// <summary>
		/// Gets the battery level.
		/// </summary>
		/// <param name="callback">Action to take on complete.</param>
		public void GetBatteryLevel(Action<APIResponse> callback){
			GET (APIMap.INFO_BATTERY, callback);
		}

		/// <summary>
		/// Gets the device information.
		/// </summary>
		/// <param name="callback">Action to take on complete.</param>
		public void GetDeviceInformation(Action<APIResponse> callback){
			GET (APIMap.INFO_DEVICE, callback);
		}

		/// <summary>
		/// Connects the wifi.
		/// </summary>
		/// <param name="networkName">Network name / SSID.</param>
		/// <param name="password">Password.</param>
		/// <param name="callback">Action to take on complete.</param>
		public void ConnectWiFi(string networkName, string password, Action<APIResponse> callback){
			WiFiPayload payload = new WiFiPayload (networkName, password);
			POST(APIMap.WIFI, JsonUtility.ToJson(payload), callback);
		}

		/// <summary>
		/// Performs the system update.
		/// </summary>
		/// <param name="callback">Action to take on complete.</param>
		public void PerformSystemUpdate(Action<APIResponse> callback){
			POST(APIMap.SYSTEM_UPDATE, "{}", callback);
		}

		/// <summary>
		/// Checks to see if there is an available update
		/// </summary>
		/// <param name="callback">Action to take on complete.</param>
		public void GetStoreUpdateAvailable(Action<APIResponse> callback){
			GET(APIMap.INFO_UPDATE, callback, "alpha");
		}

		// ========== END GENERAL =========== //

		// ========== MISC ========== //

		/// <summary>
		/// Changes the LED.
		/// </summary>
		/// <param name="color">Color.</param>
		/// <param name="callback">Action to take on complete.</param>
		public void ChangeLED(Color32 color, Action<APIResponse> callback){
			this.ChangeLEDByValue (color.r, color.g, color.b, callback);
		}

		/// <summary>
		/// Changes the LED by value.
		/// </summary>
		/// <param name="red" type="uint">Red.</param>
		/// <param name="green" type="uint">Green.</param>
		/// <param name="blue" type="uint">Blue.</param>
		/// <param name="callback" type="System.Action">Action to take on complete.</param>
		public void ChangeLEDByValue(uint red, uint green, uint blue, Action<APIResponse> callback){
			LEDPayload payload = new LEDPayload (red, green, blue);
			POST(APIMap.LED_CHANGE, JsonUtility.ToJson(payload), callback);
		}

		/// <summary>
		/// Changes the eyes.
		/// </summary>
		/// <param name="mood" type="MistyMood">Mood.</param>
		/// <param name="callback" type="System.Action">Action to take on complete.</param>
		public void ChangeEyes(MistyMood mood, Action<APIResponse> callback){
			this.ChangeEyesByValue (mood.valence, mood.arousal, mood.dominance, callback);
		}

		/// <summary>
		/// Changes the eyes by value.
		/// </summary>
		/// <param name="valence" type="float">Valence.</param>
		/// <param name="arousal" type="float">Arousal.</param>
		/// <param name="dominance" type="float">Dominance.</param>
		/// <param name="callback" type="System.Action">Action to take on complete.</param>
		public void ChangeEyesByValue(float valence, float arousal, float dominance, Action<APIResponse> callback){
			ChangeEyesPayload payload = new ChangeEyesPayload (valence, arousal, dominance);
			POST(APIMap.LED_CHANGE, JsonUtility.ToJson(payload), callback);
		}

		// ========== END MISC =========== //


		// ========== START AUDIO ========== //

		/// <summary>
		/// Gets the list of audio clips.
		/// </summary>
		/// <param name="callback">Action to take on complete.</param>
		public void GetListOfAudioClips(System.Action<APIResponse> callback){
			GET(APIMap.AUDIO_CLIPS, callback);
		}

		/// <summary>
		/// Plaies the audio clip.
		/// </summary>
		/// <param name="audioName" type="string">Audio name.</param>
		/// <param name="callback">Action to take on complete.</param>
		public void PlayAudioClip(string audioName, System.Action<APIResponse> callback){
			AudioPlayPayload payload = new AudioPlayPayload (audioName);
			POST (APIMap.AUDIO_PLAY, JsonUtility.ToJson(payload), callback);

			//WWWForm form = new WWWForm ();
			//form.AddField ("AssetId", audioName);
			//POST ("audio/play", form, callback);
		}

		public void DeleteAudioClip(string audioName, System.Action<APIResponse> callback){
			AudioDeletePayload payload = new AudioDeletePayload (audioName);
			POST (APIMap.AUDIO_DELETE, JsonUtility.ToJson(payload), callback);
		}

		public void SaveAudioAssetToRobot(string fileName, byte[] audioData, bool immediatelyApply, bool overwriteExistingFile, System.Action<APIResponse> callback){
			AudioSavePayload payload = new AudioSavePayload (fileName, audioData, immediatelyApply, overwriteExistingFile);
			POST (APIMap.AUDIO, JsonUtility.ToJson(payload), callback);
		}

		public void GetListOfAudioFiles(System.Action<APIResponse> callback){
			GET (APIMap.AUDIO, callback);
		}
		/* ========== END AUDIO ========== */


		/* ========== MOVE COMMANDS ========== */
		public void Drive(float linear, float angular, Action<APIResponse> callback){
			DrivePayload payload = new DrivePayload (linear, angular);
			POST (APIMap.DRIVE, JsonUtility.ToJson (payload), callback);
		}

		public void DriveTime(MistyDriveTime motion, float duration, Action<APIResponse> callback){
			
			this.DriveTimeByValue (motion.linear, motion.angular, motion.degrees, duration, callback);
		}

		public void DriveTimeByValue(float linear, float angular, float degrees, float duration, Action<APIResponse> callback){
			DriveTimePayload payload = new DriveTimePayload (linear, angular, degrees, duration);
			POST (APIMap.DRIVE_TIME, JsonUtility.ToJson (payload), callback);
		}

		public void DriveToLocation(int locationX, int locationY, Action<APIResponse> callback){
			DriveLocationPayload payload = new DriveLocationPayload (locationX, locationY);
			POST (APIMap.DRIVE_LOCATION, JsonUtility.ToJson (payload), callback);
		}

		public void LocomotionTrack(float left, float right, Action<APIResponse> callback){
			LocomotionPayload payload = new LocomotionPayload (left, right);
			POST (APIMap.DRIVE_TIME, JsonUtility.ToJson (payload), callback);
		}

		public void MoveArm(string side, float position, float velocity, Action<APIResponse> callback){
			ArmPayload payload = new ArmPayload (side, position, velocity);
			POST (APIMap.ARMS_MOVE, JsonUtility.ToJson (payload), callback, "beta");
		}

		public void MoveHead(float pitch, float roll, float yaw, float velocity, Action<APIResponse> callback){
			HeadMovePayload payload = new HeadMovePayload (pitch, roll, yaw, velocity);
			POST (APIMap.HEAD_MOVE, JsonUtility.ToJson (payload), callback, "beta");
		}

		public void MoveHeadToLocation(float location, float velocity, Action<APIResponse> callback){
			HeadLocationPayload payload = new HeadLocationPayload (location, velocity);
			POST (APIMap.HEAD_LOCATION, JsonUtility.ToJson (payload), callback, "beta");
		}

		public void SetHeadPosition(string axis, float position, float velocity, Action<APIResponse> callback){
			HeadPositionPayload payload = new HeadPositionPayload (axis, position, velocity);
			POST (APIMap.HEAD_POSITION, JsonUtility.ToJson (payload), callback, "beta");
		}

		// ========== END MOVE COMMANDS ========== //

		// ========== SLAM COMMANDS ========== //

		public void StartMapping(Action<APIResponse> callback){
			POST (APIMap.SLAM_MAP_START, "{}", callback);
		}

		public void StopMapping(Action<APIResponse> callback){
			POST (APIMap.SLAM_MAP_STOP, "{}", callback);
		}

		public void StartTracking(Action<APIResponse> callback){
			POST (APIMap.SLAM_TRACK_START, "{}", callback);
		}

		public void StopTracking(Action<APIResponse> callback){
			POST (APIMap.SLAM_TRACK_STOP, "{}", callback);
		}

		public void StartRecording(Action<APIResponse> callback){
			POST (APIMap.SLAM_RECORD_START, "{}", callback);
		}

		public void StopRecording(Action<APIResponse> callback){
			POST (APIMap.SLAM_RECORD_STOP, "{}", callback);
		}

		public void GetMap(Action<APIResponse> callback){
			GET (APIMap.SLAM_MAP_SMOOTH, callback);
		}

		public void GetRawMap(Action<APIResponse> callback){
			GET (APIMap.SLAM_MAP_RAW, callback);
		}

		public void GetPath(Action<APIResponse> callback){
			GET (APIMap.SLAM_PATH, callback);
		}

		public void FollowPath(string path, Action<APIResponse> callback){
			string payload = "{\"Path\":\""+path+"\"}";
			POST (APIMap.DRIVE_PATH, payload, callback);
		}

		public void FollowPathOther(float locationX, float locationY, Action<APIResponse> callback){
			string payload ="{\"Destination\": \"(" + locationX + "," + locationY + ")\"}";
			POST (APIMap.DRIVE_PATH, payload, callback);
		}

		public void GetStatus(Action<APIResponse> callback){
			GET (APIMap.SLAM_STATUS, callback);
		}

		public void StopRobot(Action<APIResponse> callback){
			POST (APIMap.DRIVE_STOP, "{}", callback);
		}

		public void ResetMapping(Action<APIResponse> callback){
			POST (APIMap.SLAM_RESET, "{}", callback);
		}

		// ========== END SLAM COMMANDS ========== //


		// ========== FACE COMMANDS ========= //

		public void StartFaceDetection(Action<APIResponse> callback){
			POST (APIMap.FACE_DETECT_START, "{}", callback, "beta");
		}

		public void StopFaceDetection(Action<APIResponse> callback){
			POST (APIMap.FACE_DETECT_STOP, "{}", callback, "beta");
		}

		public void StartFacialRecognition(Action<APIResponse> callback){
			POST (APIMap.FACE_RECOG_START, "{}", callback, "beta");
		}

		public void StopFacialRecognition(Action<APIResponse> callback){
			POST (APIMap.FACE_RECOG_STOP, "{}", callback, "beta");
		}

		public void StartFaceTraining(string faceID, Action<APIResponse> callback){
			string payload = "{\"FaceID\":\""+faceID+"\"}";
			POST (APIMap.FACE_TRAIN_START, payload, callback, "beta");
		}

		public void StopFaceTraining(Action<APIResponse> callback){
			POST (APIMap.FACE_TRAIN_CANCEL, "{}", callback, "beta");
		}

		public void ClearLearnedFaces(Action<APIResponse> callback){
			POST (APIMap.FACE_CLEAR, "{}", callback, "beta");
		}

		// ========== END FACE COMMANDS ========= //


		// ========== IMAGE COMMANDS ========= //

		public void ChangeDisplayImage(string filename, Action<APIResponse> callback){
			ImageAssetPayload p = new ImageAssetPayload ();
			p.FileName = filename;
			POST(APIMap.IMAGE_CHANGE, JsonUtility.ToJson(p), callback);
		}

		public void DeleteImage(string filename, Action<APIResponse> callback){
			ImageAssetPayload p = new ImageAssetPayload ();
			p.FileName = filename;
			POST(APIMap.IMAGE_DELETE, JsonUtility.ToJson(p), callback);
		}

		public void SaveImageAssetToRobot(string fileName, Texture imageData, int imageWidth, int imageHeight, bool immediatelyApply, bool overwriteExistingFile, Action<APIResponse> callback){
			ImageDataPayload payload = new ImageDataPayload (fileName, (imageData as Texture2D).EncodeToPNG (), imageWidth, imageHeight, immediatelyApply, overwriteExistingFile);
			POST (APIMap.IMAGE, JsonUtility.ToJson(payload), callback);
		}

		public void GetListOfImages(Action<APIResponse> callback){
			GET(APIMap.IMAGE, callback);
			
		}
		public void RevertDisplay(Action<APIResponse> callback){
			POST (APIMap.IMAGE_REVERT, "{}", callback);
		}

		// ========== END IMAGE COMMANDS ========= //


	}
}
