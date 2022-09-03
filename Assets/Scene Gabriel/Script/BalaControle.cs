using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaControle : MonoBehaviour
{

    [SerializeField] private int dano = 1;
    [SerializeField] private float velocidade = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += Vector3.forward * velocidade * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Boss")
        {
            Object.Destroy(this.gameObject);
        }
    }

}
