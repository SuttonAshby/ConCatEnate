using UnityEngine;
using System.Collections.Generic;
using System.Collections;
 /*
	This script should be for the cat

	The cat will either be wandering or headed to a balloon
	If wandering it should wander randomly,
	ELSE IF it sees a balloon stop wandering and head towards it

	*/
/// <summary>
/// Creates wandering behaviour for a CharacterController.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class CatWander : MonoBehaviour
{
	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;
	public LayerMask targetMask;
	public LayerMask obstacleMask;
	public float speed = 7;
	public float directionChangeInterval = 1;
	public float maxHeadingChange = 30;
	public List<Transform> visibleTargets = new List<Transform>();
 
	CharacterController controller;
	float heading;
	Vector3 targetRotation;
 
	void Awake ()
	{
		controller = GetComponent<CharacterController>();
 
		// Set random initial rotation
		heading = Random.Range(0, 360);
		transform.eulerAngles = new Vector3(0, heading, 0);
 
		StartCoroutine(NewHeading());
	}
	void Start() {
        StartCoroutine("FindTargetsWithDelay", .2f);
}

IEnumerator FindTargetsWithDelay(float delay) {
        while (true) {
                yield return new WaitForSeconds (delay);
                FindVisibleTargets ();
        }
}

void FindVisibleTargets() {
        visibleTargets.Clear ();
        Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

        for (int i = 0; i<targetsInViewRadius.Length; i++) {
                Transform target = targetsInViewRadius [i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
                        float dstToTarget = Vector3.Distance (transform.position, target.position);

                                if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
                                        visibleTargets.Add (target);
                        }
                }
        }
}


	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
				angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	public float smooth = 1f;
 
	private Vector3 targetAngles;

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "balloon"){
			GameObject parent = collider.gameObject.transform.parent.gameObject;
			if(parent.GetComponent<BalloonLogic>().chargedBalloon) {
				collider.gameObject.tag = "cat";
				collider.gameObject.layer = 0;
            	parent.tag = "cat";
				parent.layer = 0;
				parent.transform.SetParent(gameObject.transform);
			} else {
				GameManager.Instance.balloonsLeft --;
				Destroy(parent);
			}
			
        } else if(collider.gameObject.layer == 8) {
			//TODO get cat to turn sharply
		}
	}
	void Update ()
	{
		if (visibleTargets.Count > 0) {
			//float step =  speed * Time.deltaTime; // calculate distance to move
        	//transform.position = Vector3.MoveTowards(transform.position, visibleTargets[0].transform.position, step);
			transform.LookAt(visibleTargets[0].transform);
		}
		else {
			transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
		}
		var forward = transform.TransformDirection(Vector3.forward);
		controller.SimpleMove(forward * speed);
	}
 
	/// <summary>
	/// Repeatedly calculates a new direction to move towards.
	/// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
	/// </summary>
	IEnumerator NewHeading ()
	{
		while (true) {
			if (visibleTargets.Count == 0) {
				NewHeadingRoutine();
			}
			yield return new WaitForSeconds(directionChangeInterval);
		}
	}
 
	/// <summary>
	/// Calculates a new direction to move towards.
	/// </summary>
	void NewHeadingRoutine ()
	{
		var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
		var ceil  = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
		heading = Random.Range(floor, ceil);
		targetRotation = new Vector3(0, heading, 0);
	}
}