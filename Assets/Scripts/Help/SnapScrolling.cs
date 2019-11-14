using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SnapScrolling : MonoBehaviour
{
    [Range(1, 50)]
    [Header("Controllers")]
    public int panCount;

    [Range(0, 150)]
    public int panOffset;
    [Range(0, 20f)]
    public float snapSpeed;
    [Range(0, 5f)]
    public float scaleOffset;
    [Range(1f, 20f)]
    public float speedScale;
    [Header("Other Objects")]
    public GameObject panPrefab;
    public ScrollRect scrollRect;

    private GameObject[] instPans;
    private Vector2[] pansPos;
    private Vector2[] pansScale;

    // panel sprite
    [SerializeField] Sprite[] iconTutorial;

    private RectTransform contentRect;
    private Vector2 contentVector;

    private int selectedPanID;
    private bool isScrolling;

    private void Start()
    {
        contentRect = GetComponent<RectTransform>();
        instPans = new GameObject[panCount];
        pansPos = new Vector2[panCount];
        pansScale = new Vector2[panCount];
        //iconTutorial = new Sprite[panCount];
        for (int i = 0; i < panCount; i++)
        {
            instPans[i] = Instantiate(panPrefab, transform, false);
            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i - 1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset, instPans[i].transform.localPosition.y);
            pansPos[i] = - instPans[i].transform.localPosition;
        }
    }

    private void FixedUpdate()
    {
        if((contentRect.anchoredPosition.x >= pansPos[0].x || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x) && !isScrolling)
        {
            scrollRect.inertia = false;
        }
        float nearestPos = float.MaxValue;
        for(int i = 0; i < panCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
            if(distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;
            }
            float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, .5f, 1f);
            pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale + .3f, speedScale * Time.fixedDeltaTime);
            pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale + .3f, speedScale * Time.fixedDeltaTime);
            instPans[i].transform.localScale = pansScale[i];
            instPans[i].GetComponent<Image>().sprite = iconTutorial[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 400) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed*Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }


    public void PauseMenu()
    {
        Invoke("BackMenu", .5f);
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void InteractiveTutorial()
    {
        //if(PlayerPrefs.HasKey(ConstantsList.keyTutorial))
        //{
            PlayerPrefs.SetString(ConstantsList.keyTutorial, "true");
        //}

        SceneManager.LoadScene("Fight");
    }
}
