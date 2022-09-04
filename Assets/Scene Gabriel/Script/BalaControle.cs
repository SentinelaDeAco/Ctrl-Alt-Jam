using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaControle : MonoBehaviour
{

    //Colocar Timer de destrioção de objeto.

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss")
        {
            Object.Destroy(this.gameObject);
        }
    }

}
