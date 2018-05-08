using System.Collections.Generic;
using UnityEngine;

public class ObjectManagerScript : MonoBehaviour {

    GameObject tilePrefab;
    GameObject skeletonPrefab;
    Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();

	// Use this for initialization
	void Start () {
        tilePrefab = (GameObject)Resources.Load("prefabs/tiles/tile01", typeof(GameObject));
        skeletonPrefab = (GameObject)Resources.Load("prefabs/creatures/Skeleton/skeleton_static", typeof(GameObject));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Change(string id, string field, string new_value)
    {
        if (!objects.ContainsKey(id))
        {
            Debug.LogWarning("Changing object, but object is null!");
            return;
        }
        // todo: implement
    }

    public void Move(string id, long x, long y)
    {

        if (!objects.ContainsKey(id))
        {
            Debug.LogWarning("Moving object, but object is null!");
            return;
        }
        GameObject o = objects[id];
        o.transform.position = new Vector3(-x, 0, y);
    }

    public void DestroyObject(string id)
    {
        GameObject o = objects[id];
        if (o != null)
        {
            Debug.Log("Destroying object, id=" + id);
            Destroy(o);
        } else
        {
            Debug.LogWarning("Destroying object, but object is null!");
        }
    }

    public void InstantiateObject(string object_type, string id, long x, long y)
    {
        Debug.Log("Creating object: " + object_type + ", id: " + id + ", x: " + x + ", y: " + y);
        if (objects.ContainsKey(id))
        {
            Debug.LogWarning("object already exists!");
            return;
        }
        GameObject created = null;
        switch (object_type)
        {
            case "TILE":
                 created = Instantiate(tilePrefab, new Vector3(-x, 0, y), Quaternion.identity);
                break;
            case "CREATURE":
                created = Instantiate(skeletonPrefab, new Vector3(-x, 0, y), Quaternion.identity);
                break;
        }
        if (created != null)
        {
            objects.Add(id, created);
        }
    }
}
