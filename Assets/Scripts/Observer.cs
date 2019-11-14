
using System.Text.RegularExpressions;
using UnityEngine;
using GoogleMobileAds.Api;

/*
 * MainCamera
 */ 


public class Observer : MonoBehaviour
{

    [HideInInspector] public bool existsBulletPlayer;
    public int existsBulletEnemy = 0;
    public float lengthHighScreen;
    [HideInInspector] public float length;

    public GameObject l1;
    public GameObject l2;
    BoxCollider2D coll;

    [SerializeField] public MovingAimArrow MovingAim;
    [SerializeField] public GameObject arrow;

    public int countBulletEnemy = 0;
    public int countBulletPLayer = 0;
    [HideInInspector] public const string keyField = "Field";


    //win and lose
    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;
    bool theEnd;
    [SerializeField] TimerMain timeStop;
    [SerializeField] CreateWall wallStop;


    // Ad
    InterstitialAd ad;
    int count = 0;

    void Start()
    {
        theEnd = false;

        if(coll == null)
        {
            coll = GameObject.Find("MarginBorder").GetComponent<BoxCollider2D>();
        }

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
        
        lengthHighScreen = coll.size.y;
        var boundsY1 = l1.GetComponent<BoxCollider2D>().bounds.size.y / 2;
        var boundsY2 = l2.GetComponent<BoxCollider2D>().bounds.size.y / 2;
        length = Vector2.Distance(l1.transform.position, l2.transform.position) - (boundsY1+boundsY2);
        //if(!PlayerPrefs.HasKey(keyField))
        //{
            PlayerPrefs.SetFloat(keyField, length);
        //}

        if(arrow.activeSelf == true)
        {
            MovingAim.enabled = false;
        }
        else
        {
            MovingAim.enabled = true;
        }

        StopedTimer();
        ShowAd();
    }


    void StopedTimer()
    {
       if((win.activeSelf || lose.activeSelf) && !theEnd)
        {
            timeStop.enabled = false;
            wallStop.enabled = false;
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
