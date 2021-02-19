using UnityEngine;
using UnityEngine.UI;


/* включение инвенторя
 * выбор буста во время боя
 * добавление при покупке, выбивании из сундука или окончании боя
 * передача данных для работы буста
 */

public class _Inventory : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] GameObject[] boosters;
    [SerializeField] GameObject winWindow, loseWindow;
    [SerializeField] GameObject inventory;

    private GameObject boost { get; set; }

    public static string useBoost { get; private set; }
    public static string useBoostSecond { get; private set; }
    public static string dontUseBoost { get; private set; }

    public static int boostCount { get; private set; } // счетчик использованных бустов в раунде, не больше 2

    private void Awake()
    {
        ViewCountBoosters();
        boostCount = 0;
        dontUseBoost = "You cannot use Boosters";
    }

    public void GetBoost(GameObject getBoost)
    {
        boost = getBoost;
    }

    private void Update()
    {
        // при обновлении раунда освобождать переменную буста
        DeleteNameBoost();


        // при окончании боя
        GetRandomBoost();
        Debug.Log(useBoost);
        Debug.Log(useBoostSecond);
    }
    private void LateUpdate()
    {
        HidenInventory();
    }

    public void UseBooster()
    {
        if (boost != null)
        {
            if (boostCount == 0)
            {
                ViewLoot.nameBoost = boost.name;
                useBoost = boost.name;
            }
            else
            if(boostCount == 1)
            {
                useBoostSecond = boost.name;
                ViewLoot.nameBoost = boost.name;
            } 
            else
            if(boostCount == 2)
            {
                ViewLoot.nameBoost = dontUseBoost;
            }
            boostCount++;
            Debug.Log(boostCount);
            Lottery();
            SubtractCountBoost(boost);
            Debug.Log(boost.name);
        } 
    }

    // буст добавления жизни
    public static void AddHealth(Image heart, Sprite addHealth)
    {
        if(useBoost == "Healing" || useBoostSecond == "Healing")
        {
            if (useBoost == "Healing")
            {
                useBoost = "";
            }
            else 
                if(useBoostSecond == "Healing")
            {
                useBoostSecond = "";
            }
            heart.sprite = addHealth;
        }

    }

    // буст защиты
    public static Sprite Shield(Sprite heartAdd, Sprite heartWasted)
    {
        if(useBoost == "Shield")
        {
            useBoost = "";
            return heartAdd;
        }
        else
        {
            return heartWasted;
        }
    }


    // кража 20% стен у врага
    // добавить алерт выводящий сообщении о невозможности использовать
    // ДОДЕЛАТЬ ПОЗЖЕ!
    public static void Thief(ref float length, ref Slider slider, ref float deltaLength, ref float lengthField)
    {
        if(useBoost == "Thief" || useBoostSecond == "Thief")
        {
            slider.value = slider.value + 0.2f;
            deltaLength -= length * 0.2f;
            lengthField = length - deltaLength;
            _Enemy.CountHit++;
            if (useBoost == "Thief")
            {
                useBoost = "";
            } else
            if(useBoostSecond == "Thief")
            {
                useBoostSecond = "";
            }
        }

    }

    // показывает позицию врага
    public static float Location()
    {
        if((useBoost == "Location" || useBoostSecond == "Location") && _Timer.Timer > 0)
        {
            return -0.1f;
        }
        else
        {
            if (useBoost == "Location")
            {
                useBoost = "";
            }
            else
            if (useBoostSecond == "Location")
            {
                useBoostSecond = "";
            }
            return _Timer.Timer;
        }

    }


    // запрещает огонь противнику
    public static void Strike(ref bool shot)
    {
        if ((useBoost == "Strike" || useBoostSecond == "Strike") && !shot)
        {
            shot = true;
        }
        else
        if (useBoost == "Strike")
        {
            useBoost = "";
        } else
        if(useBoostSecond == "Strike")
        {
            useBoostSecond = "";
        }
    }

    // враг уничтожает свои стены
    // поменять тэг врага стен на свой
    public static void Misunderstood(GameObject bullet)
    {
        if((useBoost == "Misunderstood" || useBoostSecond == "Misunderstood") && !_Enemy.SHOT)
        {
            bullet.tag = "Player";
            useBoost = "";
        }
        else
        {
            bullet.tag = "Enemy";
        }
    }


    // двойной выстрел с выбором направления для каждого
    public static bool DoubleShot()
    {
        if (useBoost == "Ammunition" || useBoostSecond == "Ammunition")
        {
            if (_Timer.Timer > (_Timer.newTime - .2f))
            {
                if (useBoost == "Ammunition")
                {
                    useBoost = "";
                }
                else
                if (useBoostSecond == "Ammunition")
                {
                    useBoostSecond = "";
                }
            }
            return true;
        } else
        {
            return false;
        }

    }


    void DeleteNameBoost()
    {
        if (_Timer.Timer > (_Timer.newTime - .2f))
        {
            if (useBoost == "" || useBoostSecond == "")
            {
                if(boostCount > 0)
                {
                    boostCount = 0;
                }

                if(boost != null)
                {
                    boost = null;
                }
                return;
            }
            else
            {
                if (boost != null)
                {
                    boost = null;
                }

                useBoostSecond = useBoost = "";
                boostCount = 0;
            }
                       
        }
    }
        
    // рандомно добавляет один буст 
    void Lottery()
    {
        if (useBoost == "Lottery" || useBoostSecond == "Lottery")
        {
            var rnd = Random.Range(0, 9);
            var countBoost = PlayerPrefs.GetInt(boosters[rnd].name) + 1;
            PlayerPrefs.SetInt(boosters[rnd].name, countBoost);
            useBoost = "";
        }
    }

    // при использовании буста количество уменьшается
    void SubtractCountBoost(GameObject boost)
    {
        var textCountBoost = boost.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        var countBoost = PlayerPrefs.GetInt(boost.name) - 1;
        PlayerPrefs.SetInt(boost.name, countBoost);

        textCountBoost.text = countBoost.ToString();
    }

    // отображение количества бустов
    void ViewCountBoosters()
    {
        foreach(var boost in boosters)
        {
            var countBoost = boost.transform.GetChild(1).GetChild(0).GetComponent<Text>();
            countBoost.text = PlayerPrefs.GetInt(boost.name).ToString();
        }
    }

    // не дать использовать буст врагу

    // выпадание рандомного буста 
    void GetRandomBoost()
    {
        // в случае победы 
        if(winWindow.activeSelf)
        {
            var getBoost = winWindow.transform.GetChild(0).GetChild(2).GetComponent<Image>();
            if (!getBoost.sprite)
            {
                var rnd = Random.Range(0, 9);
                getBoost.sprite = boosters[rnd].GetComponent<Image>().sprite;
                PlayerPrefs.SetInt(boosters[rnd].name, (PlayerPrefs.GetInt(boosters[rnd].name) + 1));
            }
        }
        else 
        // в случае проигрыша
            if(loseWindow.activeSelf)
        {
            if (PlayerPrefs.GetInt(loseWindow.name) % 3 == 0) // для каждого третьего проигрыша давать буст
            {
                var getBoost = loseWindow.transform.GetChild(0).GetChild(2).GetComponent<Image>();
                if (!getBoost.sprite)
                {
                    var rnd = Random.Range(0, 9);
                    getBoost.sprite = boosters[rnd].GetComponent<Image>().sprite;
                    PlayerPrefs.SetInt(boosters[rnd].name, (PlayerPrefs.GetInt(boosters[rnd].name) + 1));
                }
            }
            else
            {
                var getBoost = loseWindow.transform.GetChild(0).GetChild(2).GetComponent<Image>();
                var color = getBoost.color;
                color.a = 0f;
                getBoost.color = color;
            }
        }
    }

    // hide the inventory when time end
    void HidenInventory()
    {
        if(_Timer.Timer < 0 && inventory.activeSelf)
        {
            inventory.SetActive(false);
        }
    }
}
