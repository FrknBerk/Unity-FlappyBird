using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class kontrol : MonoBehaviour
{
    public Sprite[] KusSprite;
    SpriteRenderer spriteRenderer;
    bool ileriGeriKontrol = true;
    int kusSayac = 0;
    float kusAnimasyonZaman = 0;

    Rigidbody2D fizik;

    int puan = 0;
    public Text puanText;

    bool oyunBitti = true;

    OyunKontrol oyunkontrol;
    AudioSource []sesler;
    //AudioSource ses;
    //public AudioClip carpmaSesi;
    //public AudioClip puanSesi;
    //public AudioClip kanatSesi;
    int enYuksekPuan = 0;

    int reklamSayaci = 0;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        oyunkontrol = GameObject.FindGameObjectWithTag("oyunkontrol").GetComponent<OyunKontrol>();
        //ses = GetComponent<AudioSource>();
        sesler = GetComponents<AudioSource>();

        enYuksekPuan = PlayerPrefs.GetInt("Kayit"); //Ettiğimiz kayıtı çekiyoruz

        Debug.Log("En YÜksek Puan= "+enYuksekPuan);

        //reklam olaylari

        reklamSayaci = PlayerPrefs.GetInt("reklamSayaci");
        reklamSayaci++;
        PlayerPrefs.SetInt("reklamSayaci",reklamSayaci);
        Debug.Log(reklamSayaci);
    }

    
    void Update()
    {
        Animasyon();
        if (Input.GetMouseButtonDown(0) && oyunBitti)
        {
            fizik.velocity = new Vector2(0, 0); //hızı sıfır yaptık ondan
            fizik.AddForce(new Vector2(0,200)); //sonra kuvvet uyguladık
            //ses.clip = kanatSesi;
            //ses.Play();
            sesler[0].Play();
        }
        if (fizik.velocity.y > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 45);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -45);
        }
    }
    void Animasyon()
    {
        kusAnimasyonZaman += Time.deltaTime;
        if (kusAnimasyonZaman > 0.2f)
        {
            kusAnimasyonZaman = 0;
            if (ileriGeriKontrol)
            {
                spriteRenderer.sprite = KusSprite[kusSayac];
                kusSayac++;
                if (kusSayac == KusSprite.Length)
                {
                    kusSayac--;
                    ileriGeriKontrol = false;
                }
            }
            else
            {
                kusSayac--;
                spriteRenderer.sprite = KusSprite[kusSayac];
                if (kusSayac == 0)
                {
                    kusSayac++;
                    ileriGeriKontrol = true;
                }
            }
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag =="puan")
        {
            puan++;
            puanText.text = "puan = " + puan;
            //ses.clip = puanSesi;
            //ses.Play(); 
            sesler[1].Play();
        }
        if (col.gameObject.tag == "engel")
        {
            oyunBitti = false;
            //ses.clip = carpmaSesi;
            //ses.Play();
            sesler[2].Play();
            oyunkontrol.oyunBitti();
            GetComponent<CircleCollider2D>().enabled = false;

            if (puan > enYuksekPuan)
            {
                enYuksekPuan = puan;
                PlayerPrefs.SetInt("Kayit", enYuksekPuan); //Hafızaya kayıt işlemi yapıyor
            }

            Invoke("anaMenuyeDon",2); //Oyun bitti zaman anaMenuyeDon e tıklıyoruz
        }
    }

    void anaMenuyeDon()
    {
        PlayerPrefs.SetInt("puanKayit", puan);
        SceneManager.LoadScene("anaMenu");
    }

}
