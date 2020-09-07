using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class anaMenu : MonoBehaviour
{
    public Text puanText;
    public Text puan;

    void Start()
    {
        int enYuksekScore = PlayerPrefs.GetInt("Kayit");
        int puanGelen = PlayerPrefs.GetInt("puanKayit");
        int reklamSayaci = PlayerPrefs.GetInt("reklamSayaci");

        puanText.text = "Score = " + enYuksekScore;
        puan.text = "Puan = " + puanGelen;

            

        if (reklamSayaci ==3)
        {
            GameObject.FindGameObjectWithTag("reklam").GetComponent<reklam>().reklamiGoster();
            PlayerPrefs.SetInt("reklamSayaci", 0);
        }
        
    }

    
    void Update()
    {
        
    }
    public void oyunaGit()
    {
        SceneManager.LoadScene("level1");
    }
    public void oyundanCık()
    {
        Application.Quit();
    }
}
