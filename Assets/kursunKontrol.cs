using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunKontrol : MonoBehaviour {
   
    dusmankontrol dusman;
    Rigidbody2D fizik;
	void Start ()
    {
        dusman = GameObject.FindGameObjectWithTag("dusman").GetComponent<dusmankontrol>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(dusman.getYon()*1000);
       

	}
	
	
}
