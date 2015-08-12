using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

    public MeshRenderer objectMesh;
    
    void Awake()
    {
        objectMesh = this.GetComponent<MeshRenderer>();
    }

	void OnTriggerStay () {
        Debug.Log("Objeto transparente");
       // objectMesh.renderer.material.color.a = 100;
        //renderer.material.color.a = 0;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
