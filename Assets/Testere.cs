using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Testere : MonoBehaviour {
    public int resim;
    GameObject[] Gidileceknoktalar;
    bool AradakiMesafeyiAL = true;
    bool İleriMiGeriMi = true;
    Vector3 AradakiMesafe;
    int AradakiMesafeSayacı;
	
	void Start ()
    {
        Gidileceknoktalar = new GameObject[transform.childCount];
        for (int i = 0; i < Gidileceknoktalar.Length; i++)
        {
            Gidileceknoktalar[i] = transform.GetChild(0).gameObject;
            Gidileceknoktalar[i].transform.SetParent(transform.parent);

        }
	}
	
	
	void FixedUpdate ()
    {
        transform.Rotate(0, 0, 6);
        NoktalaraGit();
	}
    void NoktalaraGit()
    {
        if (AradakiMesafeyiAL)
        {
            AradakiMesafe = (Gidileceknoktalar[AradakiMesafeSayacı].transform.position - transform.position).normalized;
            AradakiMesafeyiAL = false;
        }
        float mesafe = Vector3.Distance(transform.position, Gidileceknoktalar[AradakiMesafeSayacı].transform.position);
        transform.position += AradakiMesafe * Time.deltaTime*10;
        if (mesafe<0.5f)
        {
            AradakiMesafeyiAL = true;
            if (AradakiMesafeSayacı==Gidileceknoktalar.Length-1)
            {
                İleriMiGeriMi = false;
            }
            else if (AradakiMesafeSayacı==0)
            {
                İleriMiGeriMi = true;
            }
            if (İleriMiGeriMi)
            {
                AradakiMesafeSayacı++;

            }
            else
            {
                AradakiMesafeSayacı--;
            }
        }
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(Testere))]
[System.Serializable]
class testereEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Testere script = (Testere)target;
        if (GUILayout.Button("ÜRET", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject yeniobjen = new GameObject();
            yeniobjen.transform.parent = script.transform;
            yeniobjen.transform.position = script.transform.position;
            yeniobjen.name = script.transform.childCount.ToString();
        }
    }

}
#endif