using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KusunKontrol2 : MonoBehaviour {
    macekontrol mace;
    Rigidbody2D fizik1;

    void Start ()
    {
        mace = GameObject.FindGameObjectWithTag("Dusman").GetComponent<macekontrol>();
        fizik1 = GetComponent<Rigidbody2D>();
        fizik1.AddForce(mace.getYon() * 1000);
    }
	
	
	void Update () {
		
	}
}
