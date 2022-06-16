using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using TMPro;

public class DataReadExplore : MonoBehaviour
{
    public TextAsset threeMaData;
    public TextAsset oneMaData;
    private TextAsset dataUsed;
    private int tableSize = 0;
    private int sitesFound = 0;
    public Site closestSite;

    [SerializeField]
    public GameObject skullPrefab;
    public GameObject firePrefab;
    public GameObject stonePrefab;
    public GameObject organicPrefab;
    public GameObject symbolPrefab;
    public GameObject faunaPrefab;
    public GameObject labelPrefab;
    public GameObject player;
    public GameObject scoreLabel;
    public GameObject target;
    public GameObject particleSpawn;
    public LayerMask raycastLayers;

    [Serializable]
    public class Site
    {
        public string name;
        public int humanFinds;
        public int faunaFinds;
        public int stoneFinds;
        public int organicFinds;
        public int symbolFinds;
        public int fireFinds;
        public string summary;
        public float xCoordinate;
        public float yCoordinate;
        public bool found;
        public GameObject label;
    }

    [Serializable]
    public class SiteList
    {
        public Site[] site;
    }

    public SiteList cdvSiteList = new SiteList();

    // Start is called before the first frame update
    void Start()
    {
        ReadCSV();
        scoreLabel.GetComponent<TextMeshProUGUI>().SetText("Sites found:\n" + sitesFound + "/" + tableSize);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnSites();
        PointToClosestSite();
    }

    void ReadCSV()
    {
        if (StaticVar.choseThreeMa)
        {
            dataUsed = threeMaData;
        }
        else
        {
            dataUsed = oneMaData;
        }
        string[] data = dataUsed.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);
        tableSize = data.Length / 12 - 1;
        cdvSiteList.site = new Site[tableSize];
        for (int i = 0; i < tableSize; i++)
        {
            cdvSiteList.site[i] = new Site();
            cdvSiteList.site[i].name = data[12 * (i + 1)];
            cdvSiteList.site[i].humanFinds = int.Parse(data[12 * (i + 1) + 1]);
            cdvSiteList.site[i].faunaFinds = int.Parse(data[12 * (i + 1) + 2]);
            cdvSiteList.site[i].stoneFinds = int.Parse(data[12 * (i + 1) + 3]);
            cdvSiteList.site[i].organicFinds = int.Parse(data[12 * (i + 1) + 4]);
            cdvSiteList.site[i].symbolFinds = int.Parse(data[12 * (i + 1) + 5]);
            cdvSiteList.site[i].fireFinds = int.Parse(data[12 * (i + 1) + 6]);
            cdvSiteList.site[i].summary = data[12 * (i + 1) + 7];
            cdvSiteList.site[i].xCoordinate = float.Parse(data[12 * (i + 1) + 10], CultureInfo.InvariantCulture.NumberFormat);
            cdvSiteList.site[i].yCoordinate = float.Parse(data[12 * (i + 1) + 11], CultureInfo.InvariantCulture.NumberFormat);
            cdvSiteList.site[i].found = false;
        }
    }
    void PointToClosestSite()
    {
        if (sitesFound < tableSize)
        {
            foreach (var Site in cdvSiteList.site)
            {
                if (Site.found == false && (new Vector2(Site.xCoordinate, Site.yCoordinate) - new Vector2(player.transform.position.x, player.transform.position.z)).magnitude <= (new Vector2(closestSite.xCoordinate, closestSite.yCoordinate) - new Vector2(player.transform.position.x, player.transform.position.z)).magnitude)
                {
                    closestSite = Site;
                }
            }
            //pointToSite.transform.LookAt(new Vector3(closestSite.xCoordinate, player.transform.position.y, closestSite.yCoordinate)); Active with scarf
            target.transform.localRotation = Quaternion.Euler(0, 0, 180+Vector2.SignedAngle(Vector2.up,new Vector2(player.transform.position.x,player.transform.position.z) - new Vector2(closestSite.xCoordinate,closestSite.yCoordinate)));
        }
        else
        {
            target.SetActive(false);
        }
    }

    void SpawnSites()
    {
        RaycastHit rayHit;
        foreach (var Site in cdvSiteList.site)
        {
            if (Site.found == false && (new Vector2(Site.xCoordinate, Site.yCoordinate) - new Vector2(player.transform.position.x, player.transform.position.z)).magnitude < 25)
            {
                Physics.Raycast(new Vector3(Site.xCoordinate, 1000, Site.yCoordinate), Vector3.down, out rayHit);
                Site.label = Instantiate(labelPrefab, new Vector3(rayHit.point.x,10,rayHit.point.z), Quaternion.Euler(90, 0, 0));
                Site.label.GetComponent<TextMeshPro>().SetText(Site.name);
                for (int i = Site.humanFinds; i > 0; i--)
                {
                    Physics.Raycast(new Vector3(Site.xCoordinate, 1000, Site.yCoordinate), Vector3.down, out rayHit);
                    Instantiate(skullPrefab, rayHit.point, Quaternion.Euler(-90, UnityEngine.Random.Range(0.0f, 360.0f), UnityEngine.Random.Range(-30.0f, 30.0f)));
                }
                for (int i = Site.fireFinds; i > 0; i--)
                {
                    Physics.Raycast(new Vector3(Site.xCoordinate, 1000, Site.yCoordinate), Vector3.down, out rayHit);
                    Instantiate(firePrefab, rayHit.point, Quaternion.Euler(-90, UnityEngine.Random.Range(0.0f, 360.0f), UnityEngine.Random.Range(-30.0f, 30.0f)));
                }
                for (int i = Site.organicFinds; i > 0; i--)
                {
                    Physics.Raycast(new Vector3(Site.xCoordinate, 1000, Site.yCoordinate), Vector3.down, out rayHit);
                    Instantiate(organicPrefab, rayHit.point, Quaternion.Euler(0, UnityEngine.Random.Range(0.0f, 360.0f), UnityEngine.Random.Range(-30.0f, 30.0f)));
                }
                for (int i = Site.stoneFinds; i > 0; i--)
                {
                    Physics.Raycast(new Vector3(Site.xCoordinate, 1000, Site.yCoordinate), Vector3.down, out rayHit);
                    Instantiate(stonePrefab, rayHit.point, Quaternion.Euler(-90, UnityEngine.Random.Range(0.0f, 360.0f), UnityEngine.Random.Range(-30.0f, 30.0f)));
                }
                for (int i = Site.symbolFinds; i > 0; i--)
                {
                    Physics.Raycast(new Vector3(Site.xCoordinate, 1000, Site.yCoordinate), Vector3.down, out rayHit);
                    Instantiate(symbolPrefab, rayHit.point, Quaternion.Euler(-90, UnityEngine.Random.Range(0.0f, 360.0f), UnityEngine.Random.Range(-30.0f, 30.0f)));
                }
                for (int i = Site.faunaFinds; i > 0; i--)
                {
                    Physics.Raycast(new Vector3(Site.xCoordinate, 1000, Site.yCoordinate), Vector3.down, out rayHit);
                    Instantiate(faunaPrefab, rayHit.point, Quaternion.Euler(-90, UnityEngine.Random.Range(0.0f, 360.0f), UnityEngine.Random.Range(-30.0f, 30.0f)));
                }
                Site.found = true;
                closestSite = cdvSiteList.site[0];
                sitesFound++;
                scoreLabel.GetComponent<TextMeshProUGUI>().SetText("Sites found:\n" + sitesFound + "/" + tableSize);
                transform.GetComponent<AudioSource>().Play();
                Instantiate(particleSpawn, rayHit.point, Quaternion.Euler(90,0,0)).GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
