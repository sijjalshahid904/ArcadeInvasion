using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpidersManager : MonoBehaviour
{
    public GameObject projectile;
    public GameObject boss;
    private List<GameObject> spiders = new List<GameObject>();
    private StartGame sg;
    private float startDelay = 0.5f;
    private float spawnInterval = 1f;
    bool evenOdd = false;
    private float[] oddUp = { 9.5f, 11.5f, 13.5f, 15.5f };
    private float[] evenUp = { 10.5f, 12.5f, 14.5f };
    private float[] oddRight = { 3f, 5f, 7f };
    private float[] evenRight = { 2f, 4f, 6f, 8f };
    private int spawned = 0;
    private float[] posY = { 1f, 3f, 5f, 7f };
    private float[] posX = { 9f, 11f, 13f, 15f };
    public int wavenumber = 0;
    public int spidersCount;
    private float[] sizes = { 0.02f, 0.025f, 0.03f, 0.035f };
    // Start is called before the first frame update
    void Start()
    {
        sg = GameObject.Find("GameStart").GetComponent<StartGame>();
        SpawnWaves();
    }

    // Update is called once per frame
    void Update()
    {
        if (wavenumber == 4 && CountAliveSpiders() == 0 && sg.level == 1)
        {

            sg.Boss.gameObject.SetActive(true);
            GameObject Boss = Instantiate(boss, new Vector3(13, 5, 0), Quaternion.Euler(0, -90, 90));
            SpiderMove bossMove = Boss.GetComponent<SpiderMove>();
            Vector3 pos = new Vector3(0.3f, 0.3f, 0.3f);
            bossMove.transform.localScale = pos;
            bossMove.hits = 50;
            bossMove.pos = new Vector3(7, 5, 0);
            wavenumber++;
            //sg.isWin = true;
        }
    }
    void SpawnRandomSpiders()
    {
        if (wavenumber % 2 == 1)
        {
            if (evenOdd == false)
            {
                int i = 0;
                while (i < oddUp.Length)
                {
                    GameObject spider = Instantiate(projectile, new Vector3(oddUp[i], 9, 0), Quaternion.Euler(90, -90, 90));
                    spiders.Add(spider.gameObject);
                    SpiderMove spiderMove = spider.GetComponent<SpiderMove>();
                    int rand = Random.Range(0, 4);
                    Vector3 pos = new Vector3(sizes[rand], sizes[rand], sizes[rand]);
                    spiderMove.transform.localScale = pos;
                    spiderMove.hits = rand;
                    spiderMove.pos = new Vector3(oddUp[i], posY[spawned], 0);
                    i++;
                }
                evenOdd = true;
                spawned = spawned + 1;
            }
            else if (evenOdd == true)
            {
                int i = 0;
                while (i < evenUp.Length)
                {
                    GameObject spider = Instantiate(projectile, new Vector3(evenUp[i], 9, 0), Quaternion.Euler(90, -90, 90));
                    spiders.Add(spider.gameObject);
                    SpiderMove spiderMove = spider.GetComponent<SpiderMove>();
                    int rand = Random.Range(0, 4);
                    Vector3 pos = new Vector3(sizes[rand], sizes[rand], sizes[rand]);
                    spiderMove.transform.localScale = pos;
                    spiderMove.hits = rand;
                    spiderMove.pos = new Vector3(evenUp[i], posY[spawned], 0);
                    i++;
                }
                evenOdd = false;
                spawned = spawned + 1;
            }
        }
        else
        {
            if (evenOdd == false)
            {
                int i = 0;
                while (i < oddRight.Length)
                {
                    GameObject spider = Instantiate(projectile, new Vector3(16, oddRight[i], 0), Quaternion.Euler(0, -90, 90));
                    spiders.Add(spider.gameObject);
                    SpiderMove spiderMove = spider.GetComponent<SpiderMove>();
                    spiderMove.right = true;
                    int rand = Random.Range(0, 4);
                    Vector3 pos = new Vector3(sizes[rand], sizes[rand], sizes[rand]);
                    spiderMove.transform.localScale = pos;
                    spiderMove.hits = rand;
                    spiderMove.rotateIt();
                    spiderMove.pos = new Vector3(posX[spawned], oddRight[i], 0);
                    i++;
                }
                evenOdd = true;
                spawned = spawned + 1;
            }
            else if (evenOdd == true)
            {
                int i = 0;
                while (i < evenRight.Length)
                {
                    GameObject spider = Instantiate(projectile, new Vector3(16, evenRight[i], 0), Quaternion.Euler(0, -90, 90));
                    spiders.Add(spider.gameObject);
                    SpiderMove spiderMove = spider.GetComponent<SpiderMove>();
                    spiderMove.right = true;
                    int rand = Random.Range(0, 4);
                    Vector3 pos = new Vector3(sizes[rand], sizes[rand], sizes[rand]);
                    spiderMove.transform.localScale = pos;
                    spiderMove.hits = rand;
                    spiderMove.rotateIt();
                    spiderMove.pos = new Vector3(posX[spawned], evenRight[i], 0);
                    i++;
                }
                evenOdd = false;
                spawned = spawned + 1;
            }
        }
    }
    void SpawnWaves()
    {
        StartCoroutine(SpawnWaveRoutine());
    }

    IEnumerator SpawnWaveRoutine()
    {
        for (wavenumber = 0; wavenumber < 4; wavenumber++)
        {
            yield return new WaitForSeconds(startDelay);
            yield return StartCoroutine(WaitForSpidersToBeDestroyed());
            spawned = 0;
            while (spawned < wavenumber + 1)
            {
                SpawnRandomSpiders();
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
    IEnumerator WaitForSpidersToBeDestroyed()
    {
        while (CountAliveSpiders() > 0)
        {
            yield return null;
        }
    }
    int CountAliveSpiders()
    {
        int aliveCount = 0;
        foreach (GameObject spider in spiders)
        {
            if (spider != null)
            {
                aliveCount++;
            }
        }
        return aliveCount;
    }
}
