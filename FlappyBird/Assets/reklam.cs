using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class reklam : MonoBehaviour
{
    public InterstitialAd interstital;

    static reklam reklamKontrol;

    void Start()
    {
        if (reklamKontrol == null)
        {
            DontDestroyOnLoad(gameObject);
            reklamKontrol = this; // burda reklam class ımızı reklamKontrol içine atadık 
            #if UNITY_ANDROID
                        string appId = "ca - app - pub - 5261915096342503~8948158484";
            #elif UNITY_IPHONE
                                            string appId="ca-app-pub-5261915096342503~8948158484";
            #else
                                            string appId = "unexpected_platform";
            #endif

                        MobileAds.Initialize(appId);

            #if UNITY_ANDROID
                        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
            #elif UNITY_IPHONE
                                    string appId = "ca-app-pub-3940256099942544/1033173712";
            #else
                                    string = "unexpected_platform";
            #endif

                        interstital = new InterstitialAd(adUnitId);

            AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator)
                .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")
                .Build();
            interstital.LoadAd(request);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    
    public void reklamiGoster()
    {
        if (interstital.IsLoaded())
        {
            interstital.Show();
        }
        reklamKontrol = null;
        Destroy(gameObject);
    }
}
