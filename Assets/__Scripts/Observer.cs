
using System.Text.RegularExpressions;
using UnityEngine;
using GoogleMobileAds.Api;

/*
 * MainCamera
 */ 


public class Observer : MonoBehaviour
{
    //win and lose
    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;
    bool theEnd;


    // Ad
    InterstitialAd ad;
    int count = 0;

    void Start()
    {
        theEnd = false;

        #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #else
                string adUnitId = "unexpected_platform";
        #endif

        ad = new InterstitialAd(adUnitId);

        AdRequest request = new AdRequest.Builder().Build();
        ad.LoadAd(request);
    }

    private void Update()
    {
        StopedTimer();
        ShowAd();
    }


    void StopedTimer()
    {
       if((win.activeSelf || lose.activeSelf) && !theEnd) // кинуть в таймер 
        {
            theEnd = true;
        }
    }

    void ShowAd()
    {
       
        if(theEnd && count == 0)
        {
            if(ad.IsLoaded())
            {
                ad.Show();
            }
            count++;
        }
    }


}
