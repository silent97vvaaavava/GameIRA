using UnityEngine;

/* Template Prefabs
 * скрывает уничтоженные стены из префабов
 * имитирует случаайное расположение стен 
 */ 


public class WallBot : MonoBehaviour
{
    public GameObject[] wall;

    private void Awake()
    {
        //Debug.Log(_Enemy.CountHit);
        if (_Enemy.CountHit < wall.Length)
        {
            for (int i = 0; i < _Enemy.CountHit; i++)
            {
                int rnd = Random.Range(0, wall.Length - 1);
                for (int j = 0; j < wall.Length; j++)
                {
                    if (j == rnd)
                    {
                        if (wall[j].activeSelf)
                        {
                            wall[j].SetActive(false);
                            break;
                        }
                        else
                        {
                            rnd = Random.Range(0, wall.Length - 1);
                            j = -1;    
                        }
                    }

                }

            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
     

  
}

