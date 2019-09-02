using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class macekontrol : MonoBehaviour
{
    public int resim;
    GameObject[] Gidileceknoktalar;
    bool AradakiMesafeyiAL = true;
    bool İleriMiGeriMi = true;
    Vector3 AradakiMesafe;
    int AradakiMesafeSayacı;
    GameObject karakter1;
    RaycastHit2D ray;
    public LayerMask layermask;
    public GameObject kursun1;
    int hız = 5;
    float atesZamanı = 0;
    public Sprite onTaraf;
    public Sprite arkaTaraf;
    SpriteRenderer spriterenderer;

    void Start()
    {
        Gidileceknoktalar = new GameObject[transform.childCount];
        karakter1 = GameObject.FindGameObjectWithTag("Player");
        spriterenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < Gidileceknoktalar.Length; i++)
        {
            Gidileceknoktalar[i] = transform.GetChild(0).gameObject;
            Gidileceknoktalar[i].transform.SetParent(transform.parent);

        }
    }


    void FixedUpdate()
    {
        BeniGordumu();
        if (ray.collider.tag == "Player")
        {
            hız = 7;
            spriterenderer.sprite = onTaraf;
            atesET();
        }
        else
        {
            hız = 4;
            spriterenderer.sprite = arkaTaraf;
        }

        NoktalaraGit1();
    }
    void atesET()
    {
        atesZamanı += Time.deltaTime;
        if (atesZamanı > Random.Range(0.4f, 1))
        {
            Instantiate(kursun1, transform.position, Quaternion.identity);
            atesZamanı = 0;
        }
    }
    void BeniGordumu()
    {
        Vector3 rayYonum = karakter1.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYonum, 1000, layermask);
        Debug.DrawLine(transform.position, ray.point, Color.green);
    }
    void NoktalaraGit1()
    {
        if (AradakiMesafeyiAL)
        {
            AradakiMesafe = (Gidileceknoktalar[AradakiMesafeSayacı].transform.position - transform.position).normalized;
            AradakiMesafeyiAL = false;
        }
        float mesafe = Vector3.Distance(transform.position, Gidileceknoktalar[AradakiMesafeSayacı].transform.position);
        transform.position += AradakiMesafe * Time.deltaTime * hız;
        if (mesafe < 0.5f)
        {
            AradakiMesafeyiAL = true;
            if (AradakiMesafeSayacı == Gidileceknoktalar.Length - 1)
            {
                İleriMiGeriMi = false;
            }
            else if (AradakiMesafeSayacı == 0)
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
    public Vector3 getYon()
    {
        return (karakter1.transform.position - transform.position).normalized;
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(macekontrol))]
[System.Serializable]
class macekontrolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        macekontrol script = (macekontrol)target;
        EditorGUILayout.Space();
        if (GUILayout.Button("ÜRET", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject yeniobjen = new GameObject();
            yeniobjen.transform.parent = script.transform;
            yeniobjen.transform.position = script.transform.position;
            yeniobjen.name = script.transform.childCount.ToString();
        }
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layermask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("arkaTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("kursun1"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }

}
#endif