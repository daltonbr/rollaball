using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

    private MeshRenderer objectMesh;
    
    void Awake()
    {
        objectMesh = this.GetComponent<MeshRenderer>();
    }

	void OnTriggerEnter () {
        //Debug.Log("Objeto transparente");
        // pequena gambiarra. O ideal seria trabalhar com o canal alpha
        // objectMesh.renderer.material.color.a = 100;
        objectMesh.enabled = false;  // por enquanto só desabilitamos o mesh
    }

    void OnTriggerExit()
    {
        objectMesh.enabled = true;
    }
}
