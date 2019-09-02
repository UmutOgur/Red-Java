using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KarakterKontrol : MonoBehaviour {
    
    public Sprite[] BeklemeAnim;
     public Sprite[] ZiplamaAnim;
    public Sprite[] YurumeAnim;
    public Text canText;
    public Text Altintext;
    public Image SiyahArkaPlan;
   
    float SiyahArkaPlanSayaci;
    int can = 100;
    SpriteRenderer spriteRendere;
    Vector3 vec;
    
    Vector3 KameraİlkPos;
    Vector3 KameraSonPos;
   
   int BeklemeAnimSayac = 0;
    int YurumeAnimSayac = 0;
    
    float horizontal = 0f;
    float BeklemeAnimZaman = 0;
    float YurumeAnimZaman = 0;
    int altinSayacı = 0;

    bool BirkereZipla = true;
    float AnaMenuyeDonZamanı = 0;
    AudioSource[] sesler;
    
    Rigidbody2D fizik;
    GameObject Kamera;
    void Start()
    {
        Time.timeScale = 1;
        spriteRendere = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        sesler = GetComponents<AudioSource>();
      
        Kamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (SceneManager.GetActiveScene().buildIndex>PlayerPrefs.GetInt("kacincilevel"))
        {
             PlayerPrefs.SetInt("kacincilevel", SceneManager.GetActiveScene().buildIndex);
            Altintext.text = " = "+altinSayacı;
        }
      
        KameraİlkPos = Kamera.transform.position - transform.position;
        canText.text = "  " + can;
        Altintext.text = " ="+altinSayacı;
     


    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            //if (BirkereZipla)
            //{
               fizik.AddForce(new Vector2(0, 500));
                BirkereZipla = true;
            //}
           

        }
    }
 
    void FixedUpdate()
    {
        KarakterHareket();
        Animasyon();
        if (can<=0)
        {
            canText.enabled = false;
            Time.timeScale = 0.4f;
            SiyahArkaPlanSayaci += 0.03f;
            SiyahArkaPlan.color = new Color(0, 0, 0, SiyahArkaPlanSayaci);
            AnaMenuyeDonZamanı += Time.deltaTime;
            if (AnaMenuyeDonZamanı>1)
            {
                SceneManager.LoadScene("anamenu");
            }
        }

    }
    void LateUpdate()
    {
        KameraKontrol();
    }
   

    void KarakterHareket()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vec = new Vector3(horizontal * 10, fizik.velocity.y, 0);
        fizik.velocity = vec;  
    }
    void OncollisionEnter2D(Collision2D col)
    {
        BirkereZipla =true;
    }
    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.gameObject.tag=="kursun")
        {
            can--;
            canText.text = "  " + can;
        }

        if (Col.gameObject.tag == "dusman")
        {
            can-=10;
            canText.text = "  " + can;
            sesler[0].Play();
                       
        }
        if (Col.gameObject.tag=="Dusman")
        {
            can -= 10;
            canText.text = "  " + can;
            sesler[0].Play();

        }
        if (Col.gameObject.tag == "testere")
        {
            can -= 10;
            canText.text = "  " + can;
            sesler[0].Play();


        }
        if (Col.gameObject.tag == "levelbitsin")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Col.gameObject.tag == "canver")
        {
            
            if (can<100&&can>90)
            {
                 can += 0;
                canText.text = "  " + can;
                Col.GetComponent<BoxCollider2D>().enabled = true;
                Col.GetComponent<canver>().enabled = false;
                
                Destroy(Col.gameObject, 9);

            }
            
          
            else
            {
                sesler[2].Play();
                can += 10;
                canText.text = "  " + can;
                Col.GetComponent<BoxCollider2D>().enabled = false;
                Col.GetComponent<canver>().enabled = true;
                Destroy(Col.gameObject, 3);
                

            }

        }
        if (Col.gameObject.tag == "altin")
        {
            altinSayacı++;
            Altintext.text = " ="+altinSayacı;
            sesler[1].Play();
           // Debug.Log(altinSayacı++);
            Destroy(Col.gameObject);
            
        }
        if (Col.gameObject.tag == "su")
        {
            can = 0;
        }
    }
  void KameraKontrol()
    {
        KameraSonPos = KameraİlkPos + transform.position;
        Kamera.transform.position = KameraSonPos;

    }
    void Animasyon()
    {
        if (BirkereZipla)
            {
            if (horizontal == 0)
            {
                BeklemeAnimZaman += Time.deltaTime;
                if (BeklemeAnimZaman > 0.01f)
                {
                    spriteRendere.sprite = BeklemeAnim[BeklemeAnimSayac++];
                    if (BeklemeAnimSayac == BeklemeAnim.Length)
                    {
                        BeklemeAnimSayac = 0;
                    }
                    BeklemeAnimZaman = 0;
                }
                
            }
            else if (horizontal > 0)
            {
                YurumeAnimZaman += Time.deltaTime;
                if (YurumeAnimZaman > 0.01f)
                {
                    spriteRendere.sprite = YurumeAnim[YurumeAnimSayac++];
                    if (YurumeAnimSayac == YurumeAnim.Length)
                    {
                        YurumeAnimSayac = 0;
                    }
                    YurumeAnimZaman = 0;
                }
                transform.localScale = new Vector3(1, 1, 1);
               
            }
            else if (horizontal < 0)
            {
                YurumeAnimZaman += Time.deltaTime;
                if (YurumeAnimZaman > 0.01f)
                {
                    spriteRendere.sprite = YurumeAnim[YurumeAnimSayac++];
                    if (YurumeAnimSayac == YurumeAnim.Length)
                    {
                        YurumeAnimSayac = 0;
                    }
                    YurumeAnimZaman = 0;
                }
                transform.localScale = new Vector3(-1, 1, 1);
               
            }
           
             }
        else
        {

            if (fizik.velocity.y > 0)
            {
                spriteRendere.sprite = ZiplamaAnim[0];

            }
            else
            {
                spriteRendere.sprite = ZiplamaAnim[1];
            }
            if (horizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

        }
    }
}
