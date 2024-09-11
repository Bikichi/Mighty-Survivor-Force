using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private float _timeInterval;
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject _enemyPrefabs;
    [SerializeField]
    private GameObject _enemyPrefabs1;
    [SerializeField]
    private GameObject _enemyPahinhaNeon;
    [SerializeField]
    private GameObject _newEnemyPrefabs;
    [SerializeField]
    private GameObject _wolf;
    
    [SerializeField]
    private GameObject _bossArea;
    [SerializeField]
    private GameObject _boss;
    [SerializeField]
    private GameObject _bossEarthWorm;
    [SerializeField]
    private float _timeSpawnBoss;
    [SerializeField]
    private float _timeSpawnBossWorm;

    public static bool bossIsApper = false;
    public static bool boss1Die = false;

    [SerializeField]
    private GameObject _levelSlider;
    [SerializeField]
    private GameObject _listGoldFish;
    [SerializeField]
    private GameObject _listGoldFishAround;
    [SerializeField]
    private GameObject _salmon;
    [SerializeField]
    private GameObject _salmon1;
    [SerializeField]
    private GameObject _alligator;

    [SerializeField]
    private Transform spawnPoint;

    private float countBoss = 1;

    private bool _goldFishApaer = false;
    private bool _goldFishLineApaer = false;
    private bool _alligatorApaer = false;

    [SerializeField]
    private GameObject _uiBossApear;

    public static float spawnTime = 0;
    private void Awake()
    {
        Timer._curentTime = 0;
        Timer._curentTime = Time.timeSinceLevelLoad;
    }

    void Start()
    {
        bossIsApper = false;
        boss1Die = false;
        _timeInterval = Time.time;
    }

    private GameObject instanceBossArea;

    void Update()
    {
        if (((Timer._curentTime > 0 && Timer._curentTime < 90) || (Timer._curentTime > 120 && Timer._curentTime < 220)) || (Timer._curentTime > 360 && Timer._curentTime < 420) && bossIsApper == false)
        {
            InstancetiateEnemy(_enemyPrefabs);
        }
        if ((Timer._curentTime > 360 && Timer._curentTime < 390) && bossIsApper == false)
        {
            InstancetiateEnemy(_enemyPrefabs1);
        }

        if (((Timer._curentTime > 600 && Timer._curentTime < 690) || (Timer._curentTime > 840) && bossIsApper == false))
        {
            InstancetiateEnemy(_enemyPahinhaNeon);
        }

        float[] timeIntervals = { 30, 90, 150, 210, 270, 330, 390, 450, 510, 570, 630, 690 };

        if (!bossIsApper)
        {
            foreach (float startTime in timeIntervals)
            {
                if (Timer._curentTime > startTime && Timer._curentTime < startTime + 10)
                {
                    InstancetiateNewEnemy(0.6f);
                    break;
                }
            }
        }
        if ((Timer._curentTime > 540 && Timer._curentTime < 600) && bossIsApper == false)
        {
            InstancetiateNewEnemy(0.4f);
        }
        if (((Timer._curentTime > 90 && Timer._curentTime < 120) || (Timer._curentTime > 220 && Timer._curentTime < 300) || (Timer._curentTime > 440 && Timer._curentTime < 500)) && bossIsApper == false)
        {
            InstancetiateWolfEnemy(0.3f);
        }
        if ((Timer._curentTime > 500 && Timer._curentTime < 540) && bossIsApper == false)
        {
            InstancetiateWolfEnemy(0.25f);
        }
        if ((Timer._curentTime > 220 && Timer._curentTime < 300) && bossIsApper == false)
        {
            InstancetiateWolfEnemy(0.25f);
        }
        if ((Timer._curentTime > 420 && Timer._curentTime < 480) && bossIsApper == false)
        {
            InstancetiateWolfEnemy(0.15f);
        }
        if (countBoss == 1)
        {
            BossIntancetiate(_boss, _timeSpawnBoss,Vector3.zero);
        }
        if (Timer._curentTime > Timer._timeBossDie && boss1Die == true)
        {
            bossIsApper = false;
            boss1Die = false;
        }
        if (countBoss == 2)
        {
            BossIntancetiate(_bossEarthWorm, _timeSpawnBossWorm, new Vector3(0,1,0));
        }
        if (countBoss == 3)
        {
            BossIntancetiate(_bossEarthWorm, 900, Vector3.zero);
        }
        if (countBoss == 4)
        {
            BossIntancetiate(_bossEarthWorm, 1200, new Vector3(0, 1, 0));
        }
        if ((Timer._curentTime > 300 && Timer._curentTime < 360) || (Timer._curentTime > 390 && Timer._curentTime < 500) && bossIsApper == false)
        {
            InstancetiateSalmon(_salmon);
        }
        if ((Timer._curentTime > 690) && bossIsApper == false)
        {
            InstancetiateSalmon(_salmon1);
        }

        if (Timer._curentTime > 360 && _goldFishApaer == false)
        {
            StartCoroutine(SpawnGoldFishEnemy(_listGoldFishAround, 60, Vector3.zero));
        }

        if (Timer._curentTime > 390 && _goldFishLineApaer == false)
        {
            StartCoroutine(SpawnGoldFishEnemyLine(_listGoldFish, 60, Vector3.zero + new Vector3(-25, 0, 25)));
        }

        if (Timer._curentTime > 180 && _alligatorApaer == false)
        {
            StartCoroutine(SpawnAlligator(_alligator, 120, Vector3.zero + new Vector3(-25, 0, -25)));
        }


        if (bossIsApper)
        {
            DestroyAllEnemy();
            _levelSlider.SetActive(false);
        }
        else
        {
            Destroy(instanceBossArea);
            _levelSlider.SetActive(true);
        }

        DestroyByDistance();
    }

    private IEnumerator SpawnAlligator(GameObject alligator, float timeInterval,Vector3 pos)
    {
        while (true)
        {
            _alligatorApaer = true;
            IntanceAlligator(alligator, pos);
            yield return new WaitForSeconds(timeInterval);
            _alligatorApaer = false;
        }
    }
    private IEnumerator SpawnGoldFishEnemy(GameObject goldFish, float timeInterval,Vector3 pos)
    {
        while (true)
        {
            _goldFishApaer = true;
            IntanceGoldFish(goldFish, pos);
            yield return new WaitForSeconds(timeInterval);
            _goldFishApaer = false;
        }
    }
    private IEnumerator SpawnGoldFishEnemyLine(GameObject goldFish, float timeInterval,Vector3 pos)
    {
        while (true)
        {
            _goldFishLineApaer = true;
            IntanceGoldFish(goldFish, pos);
            yield return new WaitForSeconds(timeInterval);
            _goldFishLineApaer = false;
        }
    }

    public void DestroyAllEnemy()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "SardineMob Variant(Clone)")
            {
                Destroy(obj);
            }
        }

        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var en in enemy)
        {
            Destroy(en);
        }
    }

    public void IntanceGoldFish(GameObject goldFish, Vector3 pos)
    {
        GameObject goldFishLeft = Instantiate(goldFish, spawnPoint.position, goldFish.transform.rotation);
        Destroy(goldFishLeft, 12f);
    }
    public void IntanceAlligator(GameObject alligator, Vector3 pos)
    {
        Instantiate(alligator, spawnPoint.position, alligator.transform.rotation);
    }

    public void BossIntancetiate(GameObject boss, float timeSpawn , Vector3 pos)
    {
        if (!bossIsApper && Timer._curentTime > timeSpawn && boss1Die == false)
        {
            
            spawnTime = Timer._curentTime;
            GameObject UIBoss = Instantiate(_uiBossApear, gameObject.transform.position + pos, _uiBossApear.transform.rotation);
            Destroy(UIBoss, 1f);

            instanceBossArea = Instantiate(_bossArea, transform.position, Quaternion.identity);
            Instantiate(boss, transform.position + new Vector3(15,0,15), boss.transform.rotation);

            countBoss++;
            bossIsApper = true;
        }
    }

    public void InstancetiateEnemy( GameObject enemy)
    {
        if (Time.time - _timeInterval > 0.4f)
        {
            Vector3 offset = Random.onUnitSphere;
            if ((offset.x > 0.6f || offset.x <-0.6f) && (offset.z > 0.6f||offset.z <-0.6f))
           {
                offset.y = 0;
                Vector3 newOffset = transform.position + offset * Random.Range(15.2f, 15.20001f);

                Instantiate(enemy, newOffset, enemy.transform.rotation);
                _timeInterval = Time.time;
            }
        }
    }

    public void InstancetiateSalmon(GameObject salmon)
    {
        if (Time.time - _timeInterval > 0.5f)
        {
            Vector3 offset = Random.onUnitSphere;
            if ((offset.x > 0.6f || offset.x <-0.6f) && (offset.z > 0.6f||offset.z <-0.6f))
           {
                offset.y = 0;
                Vector3 newOffset = transform.position + offset * Random.Range(15.2f, 15.20001f);

                Instantiate(salmon, newOffset, salmon.transform.rotation);
                _timeInterval = Time.time;
            }
        }
    }

    public void InstancetiateNewEnemy(float timeSpawnInterval)
    {
        if (Time.time - _timeInterval > timeSpawnInterval)
        {
            Vector3 offset = Random.onUnitSphere;
            if ((offset.x > 0.5f || offset.x < -0.5f) && (offset.z > 0.5f || offset.z < -0.5f))
            {
                offset.y = 0;
                Vector3 newOffset = transform.position + offset * Random.Range(15.2f, 15.20001f);

                Instantiate(_newEnemyPrefabs, newOffset, _enemyPrefabs.transform.rotation);
                _timeInterval = Time.time;
            }
        }
    }
    public void InstancetiateWolfEnemy(float timeSpawn)
    {
        if (Time.time - _timeInterval > timeSpawn)
        {
            Vector3 offset = Random.onUnitSphere;
            if ((offset.x > 0.6f || offset.x < -0.6f) && (offset.z > 0.6f || offset.z < -0.6f))
            {
                offset.y = 0;
                Vector3 newOffset = transform.position + offset * Random.Range(15.2f, 15.20001f);

                Instantiate(_wolf, newOffset, _enemyPrefabs.transform.rotation);
                _timeInterval = Time.time;
            }
        }
    }

    public void DestroyByDistance()
    {
        GameObject destroyEnemy = GameObject.FindGameObjectWithTag("Enemy"); 
        GameObject destroyEnemyDef = GameObject.Find("SardineMob Variant(Clone)");
        GameObject destroyEnemyDef1 = GameObject.Find("SardineMob Variant 1(Clone)"); 
        GameObject destroyPiranha = GameObject.Find("Piranha Neon(Clone)");
        GameObject coin = GameObject.FindGameObjectWithTag("Coin");
        GameObject bomb = GameObject.FindGameObjectWithTag("Bomb");
        GameObject magnet = GameObject.FindGameObjectWithTag("Magnet");
        GameObject heal = GameObject.FindGameObjectWithTag("Heal");
        GameObject experience = GameObject.FindGameObjectWithTag("Experience");
        GameObject root = GameObject.FindGameObjectWithTag("Root");

        float distance;
        if(destroyEnemy != null)
        {
            distance = Vector3.Distance(destroyEnemy.transform.position, gameObject.transform.position);
            if (distance > 45)
            {
                Destroy(destroyEnemy);
            }
        }

        float distanceDef;
        if(destroyEnemyDef != null)
        {
            distanceDef = Vector3.Distance(destroyEnemyDef.transform.position, gameObject.transform.position);
            if (distanceDef > 45)
            {
                Destroy(destroyEnemyDef);
            }
        }
        float distanceDef1;
        if(destroyEnemyDef1 != null)
        {
            distanceDef1 = Vector3.Distance(destroyEnemyDef1.transform.position, gameObject.transform.position);
            if (distanceDef1 > 45)
            {
                Destroy(destroyEnemyDef1);
            }
        }
        float distancePiranha;
        if(destroyPiranha != null)
        {
            distancePiranha = Vector3.Distance(destroyPiranha.transform.position, gameObject.transform.position);
            if (distancePiranha > 45)
            {
                Destroy(destroyPiranha);
            }
        }
        
        float distanceCoin;
        if (coin != null)
        {
            distanceCoin = Vector3.Distance(coin.transform.position, gameObject.transform.position);
            if (distanceCoin > 60)
            {
                Destroy(coin);
            }
        }

        float distanceBomb;
        if (bomb != null)
        {
            distanceBomb = Vector3.Distance(bomb.transform.position, gameObject.transform.position);
            if (distanceBomb > 60)
            {
                Destroy(bomb);
            }
        }

        float distanceMagnet;
        if (magnet != null)
        {
            distanceMagnet = Vector3.Distance(magnet.transform.position, gameObject.transform.position);
            if (distanceMagnet > 60)
            {
                Destroy(magnet);
            }
        }
        float distanceHeal;
        if (heal != null)
        {
            distanceHeal = Vector3.Distance(heal.transform.position, gameObject.transform.position);
            if (distanceHeal > 60)
            {
                Destroy(heal);
            }
        }
        
        float distanceExperience;
        if (experience != null)
        {
            distanceExperience = Vector3.Distance(experience.transform.position, gameObject.transform.position);
            if (distanceExperience > 60)
            {
                Destroy(experience);
            }
        }
        
        float distanceRoot;
        if (root != null)
        {
            distanceRoot = Vector3.Distance(root.transform.position, gameObject.transform.position);
            if (distanceRoot > 60)
            {
                Destroy(root);
            }
        }
    }
}
