using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectPool 
{


	private List<GameObject> pool;
	private GameObject prefab;
	private Transform folder;



	public ObjectPool(GameObject prefab, Transform folder)
	{
		this.prefab = prefab;
		this.folder = folder;
		pool = new List<GameObject>();
	}

	public void InitializePool(int count)
	{
		if(pool == null) return;
		if(pool.Count > 0) return;
		for(int n = 0; n < count; n++) pool.Add(AllocateInstance());
	}


	public void SetFolder(Transform folder)
	{
		this.folder = folder;
	}




	/// <summary>
	/// Returns a pooled instance.
	/// P.S : the returned instance is inactive.
	/// </summary>
	public GameObject GetInstance(){
		// if Local list and it's empty:
		if (pool.Count == 0) return AllocateInstance ();
		// if has a pool already :
		for (int n = 0; n < pool.Count; n++)
			if (!pool [n].activeSelf)
				return pool [n];
		// Need to allocate :
		return AllocateInstance ();
	}


	/// <summary>
	/// Allocating a new instance will create a copy of the prefab and add it to the pool and
	/// return the new instance.
	/// </summary>
	GameObject AllocateInstance(){
		GameObject instance = GameObject.Instantiate(prefab, Vector3.one * 1000, Quaternion.identity) as GameObject;
		// ISpawnable spawnable = instance.GetComponent<ISpawnable> ();
		instance.transform.SetParent (folder);
		instance.SetActive (false);
		pool.Add (instance);
		return instance;
	}



	/// <summary>
	/// Returns the active count of objects in the pool.
	/// </summary>
	public int GetActiveCount(){
		int count = 0;
		for (int n = 0; n < pool.Count; n++)
			if (pool [n].activeSelf)
				count++;
		return count;
	}




}
