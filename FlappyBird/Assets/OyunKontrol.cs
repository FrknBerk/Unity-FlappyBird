using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyunKontrol : MonoBehaviour
{
    public GameObject gokyuzuBir;
    public GameObject gokyuzuIki;
    public float arkaPlanHiz=-1.5f;

    Rigidbody2D fizikBir;
    Rigidbody2D fizikIki;

    float uzunluk = 0;

    public GameObject engel;
    public int kacAdetEngel = 5;
    GameObject []engeller;

    float degisimZaman = 0;
    int sayac = 0;

    bool oyunbitti = true; 



    void Start()
    {
        fizikBir = gokyuzuBir.GetComponent<Rigidbody2D>();
        fizikIki = gokyuzuIki.GetComponent<Rigidbody2D>();

        fizikBir.velocity = new Vector2(arkaPlanHiz, 0);
        fizikIki.velocity = new Vector2(arkaPlanHiz, 0);

        uzunluk = gokyuzuBir.GetComponent<BoxCollider2D>().size.x;

        engeller = new GameObject[kacAdetEngel];

        for(int i =0; i<engeller.Length; i++)
        {
            engeller[i] = Instantiate(engel, new Vector2(-20,-20),Quaternion.identity);
            Rigidbody2D fizikEngel = engeller[i].AddComponent<Rigidbody2D>();
            fizikEngel.gravityScale = 0;
            fizikEngel.velocity = new Vector2(arkaPlanHiz, 0);
        }

    }


    void Update()
    {
        if (oyunbitti)
        {
            if (gokyuzuBir.transform.position.x <= -uzunluk)
            {
                gokyuzuBir.transform.position += new Vector3(uzunluk * 2, 0);
            }
            if (gokyuzuIki.transform.position.x <= -uzunluk)
            {
                gokyuzuIki.transform.position += new Vector3(uzunluk * 2, 0);
            }

            //-----------------------------------------------------------------
            degisimZaman += Time.deltaTime;
            if (degisimZaman > 2f)
            {
                degisimZaman = 0;
                float yEksenim = Random.Range(-0.50f, 1.10f);
                engeller[sayac].transform.position = new Vector3(18, yEksenim);
                sayac++;
                if (sayac >= engeller.Length)
                {
                    sayac = 0;
                }
            }
        }
        else
        {

        }           
    }
    public void oyunBitti()
    {
        for (int i = 0; i < engeller.Length; i++)
        {
            engeller[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            fizikBir.velocity = Vector2.zero;
            fizikIki.velocity = Vector2.zero;
        }
        oyunbitti = false;
    }
}
