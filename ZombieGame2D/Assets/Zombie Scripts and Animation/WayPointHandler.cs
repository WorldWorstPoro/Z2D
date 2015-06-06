using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class WayPointHandler : MonoBehaviour {

	private GameObject[] waypoints;
	private GameObject player;
	private Vector2[] waypoint_positions;

	private int player_node_index;
	private int prev_player_node_index = -1;

	//In case more performance is needed
	//private int sync_delay = 0;

	private List<Path> m_paths;
	

	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Player");

		//Find all waypoints
		waypoints = FindObsWithTag("WayPoint");


		waypoint_positions = new Vector2[waypoints.Length];

		//Loops through and determines which waypoints can "see" each other
		for (int i = 0; i < waypoints.Length; ++i) {

			GameObject waypoint1 = waypoints[i];

			//Sets the waypoint position array to the corresponding value
			waypoint_positions[i] = waypoint1.transform.position;

			Vector3 waypoint_position = waypoint1.transform.position;

			WayPointScript waypoint_script1 = waypoint1.GetComponent<WayPointScript>();

			for (int j = i + 1; j < waypoints.Length; ++j){
				
				WayPointScript waypoint_script2 = waypoints[j].GetComponent<WayPointScript>();

				if (waypoint_script2.CanSeePosition(waypoint_position)){
					waypoint_script1.AddValidIndex(j);
					waypoint_script2.AddValidIndex(i);

					float distance_between = (waypoint_position - waypoint_script2.gameObject.transform.position).magnitude;

					waypoint_script1.AddDistanceToIndex(distance_between);
					waypoint_script2.AddDistanceToIndex(distance_between);
				}
			}
		}

		player_node_index = GetIndexOfNearestWayPoint(player.transform.position);
		
		prev_player_node_index = player_node_index;
		
		ResetPaths();
		
		EstablishShortestPaths(player_node_index);
	}

	//Resets all of the paths to be invalid
	private void ResetPaths(){
		m_paths = new List<Path> (waypoints.Length);

		for (int i = 0; i < waypoints.Length; ++i) {
			m_paths.Insert(i, new Path(null, Mathf.Infinity));
		}
	}

	//Stolen from the interwebs
	GameObject[] FindObsWithTag( string tag ) { 
		GameObject[] foundObs = GameObject.FindGameObjectsWithTag(tag); 

		Array.Sort(foundObs, new AlphanumComparatorFast().Compare);
		return foundObs;
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.M)) {
			for (int i = 0; i < waypoints.Length; ++i){
				for (int j = 0; j <waypoints.Length; ++j){
					Debug.Log("Node " + i.ToString() + " to " + j.ToString() + " distance " + waypoints[i].GetComponent<WayPointScript>().GetDistanceToIndex(j).ToString() );
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.N)) {
			Debug.Log("Player " + GetIndexOfNearestWayPoint(player.transform.position));
		}

		if (Input.GetKeyDown (KeyCode.B)) {
			foreach(Path path in m_paths){
				String log_string = "";

				foreach (int index in path.GetPathList()){
					log_string = log_string + " " + index.ToString();
				}

				Debug.Log(log_string);
			}
		}
	}

	void FixedUpdate(){
		//Removed as it does not impact performance greatly
		//++sync_delay;

		//if (sync_delay == 1) {
		player_node_index = GetIndexOfNearestWayPoint(player.transform.position);

		if (prev_player_node_index != player_node_index) {

			prev_player_node_index = player_node_index;

			ResetPaths ();

			EstablishShortestPaths (player_node_index);
		}
	}

	//Establishes the shortest paths to the waypoint of index dest_index
	public void EstablishShortestPaths(int dest_index){
		List<int> base_int_list = new List<int> ();
		base_int_list.Add (dest_index);

		m_paths [dest_index] = new Path (base_int_list, 0);

		List<int> indices_to_check = new List<int> ();

		indices_to_check.Add (dest_index);

		while (indices_to_check.Count != 0) {

			/*//debug
			String indicies_log = "Checkin indicies: ";
			
			foreach (int new_index in indices_to_check){
				indicies_log = indicies_log + " " + new_index.ToString();
			}
			
			Debug.Log(indicies_log);
			//End Debug */

			List<int> next_indices_to_check = new List<int>();

			foreach (int index in indices_to_check){
				next_indices_to_check.AddRange(CheckChildrenOfNodeIndex(index));
				next_indices_to_check = next_indices_to_check.Distinct().ToList();

				/*//debug
				String log_string = "Index: " + index.ToString() + " ";
				
				foreach (int new_index in m_paths [index].GetPathList()){
					log_string = log_string + " " + new_index.ToString();
				}
				
				Debug.Log(log_string);
				//End Debug*/
			}

			indices_to_check = next_indices_to_check;
		}
	}

	//Checks the children of the passed in node to see if they are the shortest path
	//Returns a list of nodes that need to be checked next.
	private List<int> CheckChildrenOfNodeIndex(int parent_index){
		List<int> valid_indices = waypoints [parent_index].GetComponent<WayPointScript>().GetConnectedIndices();

		List<int> next_indices_to_check = new List<int> ();

		foreach (int connected_index in valid_indices) {



			Path new_path = new Path(m_paths[parent_index]);

			new_path.AddNode(connected_index);

			//Debug.Log("Parent index " + parent_index.ToString() + " connected index " + connected_index.ToString() );

			new_path.AddDistance((waypoints[parent_index].transform.position - waypoints[connected_index].transform.position).magnitude);

			//Debug.Log("Total distance " + new_path.Distance.ToString() + " current distance " + m_paths[connected_index].Distance.ToString());

			if (new_path.Distance < m_paths[connected_index].Distance){
				m_paths[connected_index] = new_path;
				next_indices_to_check.Add(connected_index);
			}
		}

		return next_indices_to_check;
	}

	public class Path{

		private List<int> m_nodes_visited;
		private float m_total_distance;

		public Path(List<int> nodes_visited, float total_distance){
			m_total_distance = total_distance;
			m_nodes_visited = (nodes_visited);
		}

		public Path(Path copied_path){
			m_nodes_visited = new List<int>(copied_path.m_nodes_visited);
			m_total_distance = copied_path.m_total_distance;
		}

		public float Distance{
			get { return m_total_distance;}
		}

		public void AddDistance(float extra_distance){
			m_total_distance += extra_distance;
		}

		public void AddNode(int new_node){
			try
			{
			m_nodes_visited.Insert (0, new_node);
			}
			catch (NullReferenceException ){
				Debug.Log ("WayPointHandler tried to add to a null path");
			}
		}

		public List<int> GetPathList(){
			return m_nodes_visited;
		}
	}

	//Retrieves the index of the nearest waypoint
	public int GetIndexOfNearestWayPoint(Vector3 obj_position){
	
		int index_of_waypoint = -1;

		float dist_to_waypoint = Mathf.Infinity;

		for (int i = 0; i < waypoints.Length; ++i) {

			Vector3 temp_vector = (waypoints[i].transform.position - obj_position);

			temp_vector.z = 0;

			float temp_distance = temp_vector.magnitude;
			//Debug.Log("Distance to node " + i.ToString() + " is " + temp_distance.ToString());


			if ((temp_distance < 8) && (temp_distance < dist_to_waypoint)){

				//If the waypoint is within range, and is a shorter distance, then do raycasting
				if ( waypoints[i].GetComponent<WayPointScript>().CanSeePosition(obj_position)){
					dist_to_waypoint = temp_distance;
					index_of_waypoint = i;
				}
			}
		}

		return index_of_waypoint;
	}

	//Determines the next waypoint that should be moved towards
	public Vector2 GetNextWayPointLocation(Vector2 cur_position){
		int waypoint_index = GetIndexOfNearestWayPoint (cur_position);

		Vector2 next_location;

		List<int> path_list = m_paths [waypoint_index].GetPathList ();

		//If there is only one waypoint in the list, it is the destination one. Head towards it
		if (path_list.Count == 1) {
			next_location = waypoint_positions [path_list [0]];
		} else {
			//Check if we can "see" the next waypoint. If so, move towards it, if not move towards the closest waypoint
			if ( waypoints[path_list[1]].GetComponent<WayPointScript>().CanSeePosition(cur_position)){
				next_location = waypoint_positions[path_list[1]];
			}else{
				next_location = waypoint_positions [path_list [0]];
			}
		}

		return next_location;
	}
}


//More code stolen from the interwebs
public class AlphanumComparatorFast : IComparer
{
	public int Compare(object x, object y)
	{
		string s1 = ((GameObject)x).name;
		if (s1 == null)
		{
			return 0;
		}
		string s2 = ((GameObject)y).name;
		if (s2 == null)
		{
			return 0;
		}
		
		int len1 = s1.Length;
		int len2 = s2.Length;
		int marker1 = 0;
		int marker2 = 0;
		
		// Walk through two the strings with two markers.
		while (marker1 < len1 && marker2 < len2)
		{
			char ch1 = s1[marker1];
			char ch2 = s2[marker2];
			
			// Some buffers we can build up characters in for each chunk.
			char[] space1 = new char[len1];
			int loc1 = 0;
			char[] space2 = new char[len2];
			int loc2 = 0;
			
			// Walk through all following characters that are digits or
			// characters in BOTH strings starting at the appropriate marker.
			// Collect char arrays.
			do
			{
				space1[loc1++] = ch1;
				marker1++;
				
				if (marker1 < len1)
				{
					ch1 = s1[marker1];
				}
				else
				{
					break;
				}
			} while (char.IsDigit(ch1) == char.IsDigit(space1[0]));
			
			do
			{
				space2[loc2++] = ch2;
				marker2++;
				
				if (marker2 < len2)
				{
					ch2 = s2[marker2];
				}
				else
				{
					break;
				}
			} while (char.IsDigit(ch2) == char.IsDigit(space2[0]));
			
			// If we have collected numbers, compare them numerically.
			// Otherwise, if we have strings, compare them alphabetically.
			string str1 = new string(space1);
			string str2 = new string(space2);
			
			int result;
			
			if (char.IsDigit(space1[0]) && char.IsDigit(space2[0]))
			{
				int thisNumericChunk = int.Parse(str1);
				int thatNumericChunk = int.Parse(str2);
				result = thisNumericChunk.CompareTo(thatNumericChunk);
			}
			else
			{
				result = str1.CompareTo(str2);
			}
			
			if (result != 0)
			{
				return result;
			}
		}
		return len1 - len2;
	}
}