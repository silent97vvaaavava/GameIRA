using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioController : MonoBehaviour
{

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        
        if(objs.Length > 1)
        {
            for(int i = 1; i < objs.Length; i++)
            {
                Destroy(objs[i]);
            }
        }
        
        

        if(SceneManager.GetActiveScene().name == "Fight")
        {
            for (int i = 0; i < objs.Length; i++)
            {
                Destroy(objs[i]);
            }
        }
        else DontDestroyOnLoad(objs[0]);

    }

    public void PlayTapSound(AudioSource value)
    {
        value.Play();
    } 
    
    public void StopTimerSound(AudioSource value)
    {
        value.Stop();
        
    }
}
