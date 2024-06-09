using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using System.Net;
using System.IO;
using TMPro;
using UnityEngine.Networking;
using System.ComponentModel;

public class AnimUIScript : MonoBehaviour
{

    private int[] animArray = new int[4];
    private string imName;
    private int hairCount;
    private int hairColor;
    private string ip;
    private GameObject text;
    private List<Material> hairs = new List<Material>();

    [SerializeField] private Button Download;

    void Start()
    {
        text = GameObject.Find("CheckText");
        text.SetActive(false);
        for (int i = 0; i < animArray.Length; i++)
        {
            animArray[i] = 1;
        }
        ip = "http://127.0.0.1:8080/";
        //ip = "http://51.250.96.220:8080";
        Download.GetComponent<Button>().onClick.AddListener(DownloadExp);
        getData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void getData()
    {
        GameObject ChooseHair = GameObject.Find("/ChooseHair");
        hairCount = ChooseHair.GetComponent<HairUIScript>().getCount();
        hairColor = ChooseHair.GetComponent<HairUIScript>().getColor();
        imName = ChooseHair.GetComponent<HairUIScript>().getImName();
        hairs = ChooseHair.GetComponent<HairUIScript>().getHairs();
    }

    public void ChangeMark(string name)
    {
        switch (name)
        {
            case "blink":
                if (animArray[0] == 1)
                    animArray[0] = 0;
                else
                    animArray[0] = 1;
                break;
            case "smile":
                if (animArray[1] == 1)
                    animArray[1] = 0;
                else
                    animArray[1] = 1;
                break;
            case "talk":
                if (animArray[2] == 1)
                    animArray[2] = 0;
                else
                    animArray[2] = 1;
                break;
            case "wow":
                if (animArray[3] == 1)
                    animArray[3] = 0;
                else
                    animArray[3] = 1;
                break;
        }
    }

    private void DownloadExp()
    {
        WWWForm form = new WWWForm();
        text.SetActive(true);
        if (File.Exists(Application.dataPath + "\\" + "Resources\\" + imName + ".png"))
        {
            form.AddBinaryData("fileUpload", File.ReadAllBytes(Application.dataPath + "\\" + "Resources\\" + imName + ".png"), "image/png");
            form.AddField("name", imName.Substring(7));
        }
        else
        {
            form.AddBinaryData("fileUpload", File.ReadAllBytes(Application.dataPath + "\\" + "Resources\\" + imName + ".jpg"), "image/jpg");
            form.AddField("name", imName.Substring(7));
        }
        form.AddField("list", animArray[0].ToString());
        form.AddField("list", animArray[1].ToString());
        form.AddField("list", animArray[2].ToString());
        form.AddField("list", animArray[3].ToString());

        StartCoroutine(Post(form));
        StartCoroutine(Check());
       
    }

    IEnumerator Post(WWWForm form)
    {
        UnityWebRequest www = UnityWebRequest.Post(ip+"/imDECA", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            StopAllCoroutines();
            Debug.Log(www.error);
            text.GetComponent<TextMeshProUGUI>().text = "Server is reloading. Please wait";
        }
        else
        {
            StopAllCoroutines();
            Debug.Log("Form upload complete! " + www.downloadHandler.text);
            WebClient client = new WebClient();
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(CreatePreFab);
            System.Uri uri = new System.Uri(ip+"/downLoad?name=" + imName.Substring(7));
            string path = Application.dataPath+ "\\Resources\\Faces\\" + imName.Substring(7)+".zip"; 
            client.DownloadFileAsync(uri, path);

        }
        www.Dispose();
    }

    private void CreatePreFab(object sender, AsyncCompletedEventArgs e)
    {
        GameObject NewFace = new GameObject();
        NewFace.AddComponent<AnimScript>();
        NewFace.GetComponent<AnimScript>().hairCount = hairCount;
        NewFace.GetComponent<AnimScript>().hairColor = hairColor;
        NewFace.GetComponent<AnimScript>().name = imName.Substring(7);
        NewFace.GetComponent<AnimScript>().setHair(hairs);
        NewFace.SetActive(false);
        string localPath = "Assets/"+ imName.Substring(7) + ".prefab";
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        PrefabUtility.SaveAsPrefabAsset(NewFace, localPath);
        text.GetComponent<TextMeshProUGUI>().text = "Donwload complete!";
        StartCoroutine(Success());

    }

    IEnumerator Success()
    {
        UnityWebRequest www = UnityWebRequest.Get(ip + "/success");
        yield return www.SendWebRequest();
        www.Dispose();
    }

    IEnumerator Check()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            UnityWebRequest wwwCh = UnityWebRequest.Get(ip+"/check");
            yield return wwwCh.SendWebRequest();
            if (wwwCh.isNetworkError || wwwCh.isHttpError)
            {
                Debug.Log(wwwCh.error);
            }
            else
            {
                string str = wwwCh.downloadHandler.text;
                string mes = str.Substring(12,str.Length-12);
                string[] numbers = mes.Split(',');
                
                numbers[0] = numbers[0].TrimStart('"');
                numbers[1] = numbers[1].TrimEnd('}');
                numbers[1] = numbers[1].TrimEnd('"');
                numbers[1] = numbers[1].TrimStart(' ');
                if (numbers[1] == "0")
                    text.GetComponent<TextMeshProUGUI>().text = "Preparing Data and Decoder";
                else
                    if (numbers[0] == numbers[1])
                    text.GetComponent<TextMeshProUGUI>().text = "Getting ready for download";
                else
                    if (numbers[1] != "0")
                        text.GetComponent<TextMeshProUGUI>().text = numbers[0] + " out of " + numbers[1];

            }
            wwwCh.Dispose();
        }
        
    }
}
