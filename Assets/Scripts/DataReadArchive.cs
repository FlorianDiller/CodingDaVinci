using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using TMPro;

public class DataReadArchive : MonoBehaviour
{
    public TextAsset threeMaData;
    public TextAsset oneMaData;
    private TextAsset dataUsed;
    public int tableSize = 0;
    public int sitesArchived = 0;
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
    public GameObject summaryLabel;
    public GameObject symbolLabel;
    public GameObject summaryContainer;
    public GameObject audioObject;
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
        public bool archived;
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
        SpawnSites();
        scoreLabel.GetComponent<TextMeshProUGUI>().SetText("Sites archived:\n" + sitesArchived + "/" + tableSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("x") || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            if (!summaryContainer.activeSelf && !player.GetComponent<PauseMenu>().isPaused)
            {
                ArchiveSite();
            }
            else
            {
                if(summaryContainer.activeSelf)
                {
                    player.GetComponent<PauseMenu>().isPaused = false;
                    summaryContainer.SetActive(false);
                    audioObject.GetComponent<AudioSource>().enabled = true;
                    transform.GetComponents<AudioSource>()[1].Play();
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (summaryContainer.activeSelf)
            {
                player.GetComponent<PauseMenu>().isPaused = false;
                summaryContainer.SetActive(false);
                audioObject.GetComponent<AudioSource>().enabled = true;
                transform.GetComponents<AudioSource>()[1].Play();
            }
            else
            {
                //PauseMenu
            }
        }
    }

    void ReadCSV()
    {
        if(StaticVar.choseThreeMa)
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
            cdvSiteList.site[i].archived = false;
        }
    }
    void ArchiveSite()
    {
        foreach (var Site in cdvSiteList.site)
        {
            if ((new Vector2(Site.xCoordinate, Site.yCoordinate) - new Vector2(player.transform.position.x, player.transform.position.z)).magnitude < (new Vector2(closestSite.xCoordinate, closestSite.yCoordinate) - new Vector2(player.transform.position.x, player.transform.position.z)).magnitude && !Site.archived)
            {
                closestSite = Site;
            }
        }
        if ((new Vector2(closestSite.xCoordinate, closestSite.yCoordinate) - new Vector2(player.transform.position.x, player.transform.position.z)).magnitude < 25 && !closestSite.archived)
        {
            summaryLabel.GetComponent<TextMeshProUGUI>().SetText(closestSite.summary);
            symbolLabel.GetComponent<TextMeshProUGUI>().SetText("");
            if(closestSite.humanFinds>0)
            {
                symbolLabel.GetComponent<TextMeshProUGUI>().SetText(symbolLabel.GetComponent<TextMeshProUGUI>().text + "<sprite=1>");
            }
            if (closestSite.fireFinds > 0)
            {
                symbolLabel.GetComponent<TextMeshProUGUI>().SetText(symbolLabel.GetComponent<TextMeshProUGUI>().text + "<sprite=0>");
            }
            if (closestSite.faunaFinds > 0)
            {
                symbolLabel.GetComponent<TextMeshProUGUI>().SetText(symbolLabel.GetComponent<TextMeshProUGUI>().text + "<sprite=2>");
            }
            if (closestSite.stoneFinds > 0)
            {
                symbolLabel.GetComponent<TextMeshProUGUI>().SetText(symbolLabel.GetComponent<TextMeshProUGUI>().text + "<sprite=5>");
            }
            if (closestSite.organicFinds > 0)
            {
                symbolLabel.GetComponent<TextMeshProUGUI>().SetText(symbolLabel.GetComponent<TextMeshProUGUI>().text + "<sprite=4>");
            }
            if (closestSite.symbolFinds > 0)
            {
                symbolLabel.GetComponent<TextMeshProUGUI>().SetText(symbolLabel.GetComponent<TextMeshProUGUI>().text + "<sprite=3>");
            }
            summaryContainer.transform.rotation = Quaternion.Euler(0, 180, UnityEngine.Random.Range(-5, 5));
            summaryContainer.SetActive(true);
            sitesArchived++;
            player.GetComponent<PauseMenu>().isPaused = true;
            scoreLabel.GetComponent<TextMeshProUGUI>().SetText("Sites archived:\n" + sitesArchived + "/" + tableSize);
            closestSite.archived = true;
            closestSite.label.GetComponent<TextMeshPro>().color = new Color(255, 255 , 255 , 255);
            audioObject.GetComponent<AudioSource>().enabled = false;
            transform.GetComponents<AudioSource>()[0].Play();
            closestSite = cdvSiteList.site[0];
        }
    }

    void SpawnSites()
    {
        RaycastHit rayHit;
        foreach (var Site in cdvSiteList.site)
        {
            Physics.Raycast(new Vector3(Site.xCoordinate, 1000, Site.yCoordinate), Vector3.down, out rayHit, Mathf.Infinity);
            Site.label = Instantiate(labelPrefab, new Vector3(rayHit.point.x, 10, rayHit.point.z), Quaternion.Euler(90, 0, 0));
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
        }
    }
}
