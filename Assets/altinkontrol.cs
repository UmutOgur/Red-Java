using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class altinkontrol : MonoBehaviour {

    public Sprite[] animasyonKareleri;
    SpriteRenderer spriterenderer;
    float zaman = 0;
    int animasyonKareleriSayacı = 0;

    void Start ()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }
	
	

	void Update ()
    {
        zaman += Time.deltaTime;
        if (zaman > 0.03f)
        {
            spriterenderer.sprite = animasyonKareleri[animasyonKareleriSayacı++];
            if (animasyonKareleri.Length == animasyonKareleriSayacı)
            {
                animasyonKareleriSayacı =0;
            }
            zaman = 0;
        }
    }
}
