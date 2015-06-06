using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WayPointScript : MonoBehaviour {
	
	private int m_index;
	//Parallel arrays for whether a waypoint with a given index is valid to travel to, and what the distance is.
	private List<int> m_valid_indices = new List<int> ();
	private List<float> m_index_distances = new List<float> ();

	// Use this for initialization
	void Start () {

	}
	
	public void AddValidIndex(int valid_index){
		m_valid_indices.Add (valid_index);
	}

	public void AddDistanceToIndex(float distance){
		m_index_distances.Add (distance);
	}

	//Unneeded due to GetDistanceToIndex returning -1
	/*public bool IsValidIndex(int index){
		return (m_valid_indices.Contains(index));
	}*/

	public List<int> GetConnectedIndices(){
		return m_valid_indices;
	}

	public float GetDistanceToIndex(int index_to_find){
		float distance = -1;
		for (int i = 0; i < m_valid_indices.Count; ++i){
			if (m_valid_indices[i] == index_to_find){
				distance = m_index_distances[i];
			}
		}

		return distance;
	}

	public bool CanSeePosition(Vector3 new_position){
		Vector3 dir = (Vector3)new_position - (Vector3)transform.position;

		dir.z = 0;

		RaycastHit2D hit;
		
		int layer_mask = 1 << 10;
		
		float max_distance = dir.magnitude;
		
		hit = Physics2D.Raycast(transform.position, dir, max_distance, layer_mask);

		return (hit.collider == null);
	}
}
