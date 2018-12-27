using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniWebServer;
using Misty;

[RequireComponent(typeof(EmbeddedWebServerComponent))]
public class SimulatedAPI : MonoBehaviour, IWebResource {

	EmbeddedWebServerComponent server;

	private string api_uri = "/api";
	private Dictionary<string, System.Action<Request, Response>> apiMap;

	// Use this for initialization
	void Start () {

		//https://github.com/simonwittber/uniwebserver
		server = GetComponent<EmbeddedWebServerComponent>();
		this.apiMap = new Dictionary<string, System.Action<Request, Response>> ();
		string BETA = "beta";

		this.apiMap.Add(APIMap.INFO, handle_info);
		this.apiMap.Add(APIMap.WIFI, handle_wifi);
		this.apiMap.Add(APIMap.SENSOR, handle_sensor);
		this.apiMap.Add(APIMap.SYSTEM, handle_system);
		this.apiMap.Add(APIMap.SYSTEM_UPDATE, handle_system_update);
		this.apiMap.Add(APIMap.INFO_HELP, handle_info_help);
		this.apiMap.Add(APIMap.INFO_BATTERY, handle_info_battery);
		this.apiMap.Add(APIMap.INFO_DEVICE, handle_info_device);
		this.apiMap.Add(APIMap.INFO_HELP_COMMAND, handle_info_help_command);

		this.apiMap.Add(APIMap.LED_CHANGE, handle_led_change);
		this.apiMap.Add(APIMap.EYES_CHANGE, handle_eyes_change);

		this.apiMap.Add (APIMap.IMAGE, handle_get_image_list);
		this.apiMap.Add(APIMap.IMAGE_CHANGE, handle_image_change);
		this.apiMap.Add(APIMap.IMAGE_DELETE, handle_image_delete);
		this.apiMap.Add(APIMap.IMAGE_REVERT, handle_image_revert);

		this.apiMap.Add(APIMap.AUDIO, handle_get_audio_list);
		this.apiMap.Add(APIMap.AUDIO_CLIPS, handle_audio_clips);
		this.apiMap.Add(APIMap.AUDIO_PLAY, handle_audio_play);
		this.apiMap.Add(APIMap.AUDIO_DELETE, handle_audio_delete);

		this.apiMap.Add(APIMap.FACE_CLEAR, handle_face_clear);
		this.apiMap.Add(APIMap.FACE_DETECT_START, handle_detect_start);
		this.apiMap.Add(APIMap.FACE_DETECT_STOP, handle_detect_stop);
		this.apiMap.Add(APIMap.FACE_RECOG_START, handle_recog_start);
		this.apiMap.Add(APIMap.FACE_RECOG_STOP, handle_recog_stop);
		this.apiMap.Add(APIMap.FACE_TRAIN_CANCEL, handle_train_cancel);

		this.apiMap.Add(APIMap.DRIVE_LOCATION, handle_drive_location);
		this.apiMap.Add(APIMap.DRIVE_PATH, handle_drive_path);
		this.apiMap.Add(APIMap.DRIVE_STOP, handle_drive_stop);
		this.apiMap.Add(APIMap.DRIVE_TIME, handle_drive_time);
		this.apiMap.Add(APIMap.DRIVE_TRACK, handle_drive_track);

		this.apiMap.Add(APIMap.ARMS_MOVE, handle_arms_move);
		this.apiMap.Add(APIMap.HEAD_LOCATION, handle_head_location);
		this.apiMap.Add(APIMap.HEAD_POSITION, handle_head_position);
		this.apiMap.Add(string.Format("{0}/{1}", BETA, APIMap.HEAD_MOVE), handle_head_move);


		//add all routes
		foreach (string route in this.apiMap.Keys) {
			server.AddResource(string.Format("{0}/{1}", this.api_uri, route), this);	
		}


	}

	public void HandleRequest (Request request, Response response)
	{
		Debug.Log (request.body);
		Debug.Log (request.path);
		Debug.Log (request.formData);
		Debug.Log (request.uri);

		Debug.Log (request.method);

		// look at all routes
		foreach (string route in this.apiMap.Keys) {
			//Debug.Log (route);
			//Debug.Log (request.path.IndexOf (route));
			if (request.path.IndexOf(route) != -1) {
				this.apiMap [route] (request, response);
				return;
			}
		}

		response.statusCode = 200;
		response.message = "OK.";
		response.Write ("default response");

	}

	//GENERAL
	private void handle_info(Request request, Response response){
		response.statusCode = 200;
		response.message = "INFO";

		switch (request.method) {
		case "GET":
			response.Write("I am Misty info");
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}

	}

	private void handle_wifi(Request request, Response response){

		response.statusCode = 200;
		response.message = "WIFI";
		response.Write("I am Misty info");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_sensor(Request request, Response response){

		response.statusCode = 200;
		response.message = "SENSOR";
		response.Write("I am Misty info");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_system(Request request, Response response){

		response.statusCode = 200;
		response.message = "SYSTEM";
		response.Write("I am Misty info");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_system_update(Request request, Response response){

		response.statusCode = 200;
		response.message = "SYSTEM UPDATE";
		response.Write("I am Misty info");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_info_help(Request request, Response response){
		
		response.statusCode = 200;
		response.message = "HELP";
		response.Write("I am Misty info");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_info_battery(Request request, Response response){

		response.statusCode = 200;
		response.message = "BATTERY";
		response.Write("I am Misty info");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_info_device(Request request, Response response){

		response.statusCode = 200;
		response.message = "DEVICE";
		response.Write("I am Misty info");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_info_help_command(Request request, Response response){

		response.statusCode = 200;
		response.message = "HELP COMMAND";
		response.Write("I am Misty info");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	//EYES
	private void handle_eyes_change(Request request, Response response){

		response.statusCode = 200;
		response.message = "CHANGE EYES";
		response.Write("CHANGE EYES");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	//LED
	private void handle_led_change(Request request, Response response){
		
		response.statusCode = 200;
		response.message = "CHANGE LED";

		switch (request.method) {
		case "GET":
			LEDPayload lp = new LEDPayload (255, 0, 0);
			response.Write (lp.ToJSON());
			break;
		case "POST":
			Debug.Log ("LED POST");
			LEDPayload ledData = LEDPayload.fromJSON (request.body);
			Debug.Log (ledData);
			MysticalMisty.Instance.core.physical.SetLED (new Color (ledData.Red, ledData.Green, ledData.Blue));
			response.Write (new APIResult().ToJSON());
			break;
		default:
			
			response.Write("CHANGE LED");
			break;
		}
	}

	//IMAGE
	private void handle_get_image_list(Request request, Response response){

		response.statusCode = 200;
		response.message = "GET IMAGES";

		switch (request.method) {
		case "GET":
			ImageListResult ilr = new ImageListResult ();
			ilr.result = MysticalMisty.Instance.core.simulatedState.images;
			response.Write(ilr.ToJSON());
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_image_change(Request request, Response response){

		response.statusCode = 200;
		response.message = "CHANGE IMAGE";
		response.Write("CHANGE IMAGE");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_image_delete(Request request, Response response){

		response.statusCode = 200;
		response.message = "IMAGE DELETE";

		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			response.Write("IMAGE  DELETE");
			break;
		}
	}

	private void handle_image_revert(Request request, Response response){

		response.statusCode = 200;
		response.message = "IMAGE REVERT";
		response.Write("IMAGE REVERT");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	//AUDIO
	private void handle_get_audio_list(Request request, Response response){
		response.statusCode = 200;
		response.message = "GETAUDIO CLIPS";

		switch (request.method) {
		case "GET":
			AudioClipsList acr = new AudioClipsList ();
			acr.result = MysticalMisty.Instance.core.simulatedState.audioFiles;
			response.Write(acr.ToJSON());
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_audio_clips(Request request, Response response){

		response.statusCode = 200;
		response.message = "AUDIO CLIPS";

		switch (request.method) {
		case "GET":
			response.Write("{\"clips\":[]}");
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_audio_delete(Request request, Response response){

		response.statusCode = 200;
		response.message = "AUDIO DELETE";
		response.Write("AUDIO DELETE");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_audio_play(Request request, Response response){

		response.statusCode = 200;
		response.message = "AUDIO PLAY";
		response.Write("AUDIO PLAY");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	//FACE
	private void handle_face_clear(Request request, Response response){

		response.statusCode = 200;
		response.message = "FACE CLEAR";
		response.Write("FACE CLEAR");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_detect_start(Request request, Response response){

		response.statusCode = 200;
		response.message = "DETECT START";
		response.Write("DETECT START");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_detect_stop(Request request, Response response){

		response.statusCode = 200;
		response.message = "FACE CLEAR";
		response.Write("FACE CLEAR");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_recog_start(Request request, Response response){

		response.statusCode = 200;
		response.message = "RECOG START";
		response.Write("RECOG START");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_recog_stop(Request request, Response response){

		response.statusCode = 200;
		response.message = "RECOG STOP";
		response.Write("RECOG STOP");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_train_cancel(Request request, Response response){

		response.statusCode = 200;
		response.message = "TRAIN CANCEL";
		response.Write("TRAIN CANCEL");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	//MOVE
	private void handle_drive_location(Request request, Response response){

		response.statusCode = 200;
		response.message = "TRAIN CANCEL";
		response.Write("DRIVE LOCATION");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_drive_path(Request request, Response response){

		response.statusCode = 200;
		response.message = "TRAIN CANCEL";
		response.Write("DRIVE PATH");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_drive_stop(Request request, Response response){

		response.statusCode = 200;
		response.message = "TRAIN CANCEL";
		response.Write("DRIVE STOP");

		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_drive_time(Request request, Response response){

		response.statusCode = 200;
		response.message = "TRAIN CANCEL";
		response.Write("DRIVE TIME");
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_drive_track(Request request, Response response){

		response.statusCode = 200;
		response.message = "TRAIN CANCEL";
		response.Write("DRIVE TRACK");	
		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_arms_move(Request request, Response response){

		response.statusCode = 200;
		response.message = "TRAIN CANCEL";
		response.Write("ARMS MOVE");

		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_head_location(Request request, Response response){

		response.statusCode = 200;
		response.message = "TRAIN CANCEL";
		response.Write("HEAD LOCATION");

		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_head_position(Request request, Response response){

		response.statusCode = 200;
		response.message = "TRAIN CANCEL";
		response.Write("HEAD POSIITION");

		switch (request.method) {
		case "GET":
			break;
		case "POST":
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

	private void handle_head_move(Request request, Response response){

		response.statusCode = 200;
		response.message = "HEAD MOVE";
		response.Write("HEAD MOVE");	

		switch (request.method) {
		case "GET":
			break;
		case "POST":
			HeadMovePayload hmp = HeadMovePayload.fromJSON (request.body.ToString ());
			MysticalMisty.Instance.core.physical.MoveHead (hmp);
			break;
		case "PUT":
			break;
		case "DELETE":
			break;
		default:
			break;
		}
	}

}
