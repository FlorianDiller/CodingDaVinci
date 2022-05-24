using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using TMPro;

public class DataReadExplore : MonoBehaviour
{
    public TextAsset textAssetData;
    private int tableSize = 0;
    private int sitesFound = 0;
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
        scoreLabel.GetComponent<TextMeshPro>().SetText("Sites found:\n" + sitesFound + "/" + tableSize);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnSites();
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);
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
    void SpawnSites()
    {
        RaycastHit rayHit;
        foreach (var Site in cdvSiteList.site)
        {
            if ((new Vector2(Site.xCoordinate, Site.yCoordinate) - new Vector2(player.transform.position.x, player.transform.position.z)).magnitude < 25 && Site.found == false)
            {
                Physics.Raycast(new Vector3(Site.xCoordinate, 1000, Site.yCoordinate), Vector3.down, out rayHit);
                Instantiate(labelPrefab, rayHit.point + 5 * Vector3.up, Quaternion.Euler(90, 0, 0)).GetComponent<TextMeshPro>().SetText(Site.name);
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
                sitesFound++;
                scoreLabel.GetComponent<TextMeshPro>().SetText("Sites found:\n" + sitesFound + "/" + tableSize);
            }
        }
    }
}