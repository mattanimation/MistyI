using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Misty{

	public class TOFSensorComponent : MistySensorComponent {

		public float distance;
		public SensorPosition position;

		private Ray ray;
		private RaycastHit hitInfo;
		private float maxLen = 5.0f; //in meters
		public float debounceRate = 0.2f; //200ms
		private float lastReadTime = 0.0f;

		// Use this for initialization
		void Start () {
			this.position = SensorPosition.FRONT_MIDDLE;
		}
		
		// Update is called once per frame
		void Update () {

			if (Time.time >= lastReadTime + debounceRate) {
				ray = new Ray (transform.localPosition, transform.forward);
				if (Physics.Raycast (ray, out hitInfo, maxLen)) {
					//hitInfo.collider.transform
					//get dist from origin to collided object
					this.distance = Vector3.Distance (transform.localPosition, hitInfo.collider.transform.position);
					Debug.Log (string.Format ("distance is {0}", this.distance));
				} else {
					this.distance = maxLen;
				}

				//Debug.DrawLine (ray.origin, hitInfo.point, Color.red);
				Debug.DrawRay (ray.origin, ray.direction, Color.red);
				lastReadTime = Time.time;
			}

		}
	}

}
