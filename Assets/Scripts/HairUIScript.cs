using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.ComponentModel;

public class HairUIScript : MonoBehaviour
{
    private List<string> files;
    private List<string> hairNames;
    private string[] colorNames;
    [SerializeField] private GameObject Face;
    [SerializeField] private Button Left;
    [SerializeField] private Button Right;
    [SerializeField] private Button LeftColor;
    [SerializeField] private Button RightColor;
    [SerializeField] private Button Download;
    [SerializeField] private GameObject image;
    public List<Material> hairs = new List<Material>();
    private GameObject hairObject;

    private int counter;
    private int counterColor;
    private string imName;
    


    public void getName()
    {
        GameObject ChooseImage = GameObject.Find("/ChooseImage");
        imName = ChooseImage.GetComponent<ImageUIScript>().getNameIm();
        var spr = Resources.Load<Texture2D>(imName);
        var sprite = TextureToSprite(spr);
        image.GetComponent<Image>().sprite = sprite;

    }

    public static Sprite TextureToSprite(Texture2D texture) => Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 50f, 0, SpriteMeshType.FullRect);
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        colorNames = new string[6]{ "Blonde", "DarkBlonde", "Brown", "Red", "Black", "Silver" };
        counterColor = 0;
        string[] filesStr = Directory.GetFiles(Application.dataPath + "\\Resources\\Hair", "*", SearchOption.TopDirectoryOnly);
        files = filesStr.ToList<string>();
        files.RemoveAll(s => s.Substring(s.Length - 5).Contains("meta"));
        
        UnityEngine.Object pPrefab = Resources.Load("Hair/Female hair 1");
        hairObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, Face.transform);
        hairObject.transform.Rotate(new Vector3(0, 190, 0));
        hairObject.transform.localPosition = new Vector3(-0.0058f, -0.1077f, 2.859f);
        hairObject.transform.localScale = new Vector3(1.15f, 1.1f, 1.5f);
        hairObject.GetComponent<MeshRenderer>().material = hairs[0];
        for (int i = 0; i < files.Count; i++)
        {

            int index = files[i].LastIndexOf("Hair", files[i].Length);
            int index2 = files[i].LastIndexOf(".prefab", files[i].Length);
            files[i] = files[i].Substring(index, index2 - index);
        }
        hairNames = new List<string>(files);
        for (int i = 0; i < files.Count; i++)
        {
            hairNames[i] = hairNames[i].Substring(5, hairNames[i].Length - 5);
        }

        Left.GetComponent<Button>().onClick.AddListener(PrevHair);
        Right.GetComponent<Button>().onClick.AddListener(NextHair);

        LeftColor.GetComponent<Button>().onClick.AddListener(PrevColor);
        RightColor.GetComponent<Button>().onClick.AddListener(NextColor);

        

        GameObject textColor = GameObject.Find("ChooseHair/HairColorObject");
        textColor = textColor.gameObject.transform.GetChild(1).gameObject;
        textColor.GetComponent<TextMeshProUGUI>().text = colorNames[0];
    }

    private void NextHair()
    {
        Destroy(hairObject);
        UnityEngine.Object pPrefab;
        counter++;
        if (counter > files.Count-1)
        {
            counter = 0;
        }
        GameObject textHair = GameObject.Find("ChooseHair/HairTypeObject");
        textHair = textHair.gameObject.transform.GetChild(1).gameObject;

        switch (counter)
        {
            case 0:
                pPrefab = Resources.Load(files[counter]);
                hairObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, Face.transform);
                hairObject.transform.Rotate(new Vector3(0, 190, 0));
                hairObject.transform.localPosition = new Vector3(-0.0058f, -0.1077f, 2.859f);
                hairObject.transform.localScale = new Vector3(1.15f, 1.1f, 1.5f);
                hairObject.GetComponent<MeshRenderer>().material = hairs[counterColor];

                textHair.GetComponent<TextMeshProUGUI>().text = hairNames[counter];
                break;
            case 1:
                pPrefab = Resources.Load(files[counter]);
                hairObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, Face.transform);
                hairObject.transform.Rotate(new Vector3(0, 170, 0));
                hairObject.transform.localPosition = new Vector3(-0.0041f, -0.1077f, 2.8257f);
                hairObject.transform.localScale = new Vector3(0.009f, 0.009f, 0.01f);
                hairObject.GetComponent<MeshRenderer>().material = hairs[counterColor];

                textHair.GetComponent<TextMeshProUGUI>().text = hairNames[counter];
                break;
            case 2:
                pPrefab = Resources.Load(files[counter]);
                hairObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, Face.transform);
                hairObject.transform.Rotate(new Vector3(0, 180, 0));
                hairObject.transform.localPosition = new Vector3(0.008f, -0.1584f, 2.8272f);
                hairObject.transform.localScale = new Vector3(1f, 1f, 1f);
                hairObject.GetComponent<MeshRenderer>().material = hairs[counterColor];

                textHair.GetComponent<TextMeshProUGUI>().text = hairNames[counter];
                break;
            case 3:
                pPrefab = Resources.Load(files[counter]);
                hairObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, Face.transform);
                hairObject.transform.Rotate(new Vector3(0, 190, 0));
                hairObject.transform.localPosition = new Vector3(0.0048f, -0.0949f, 2.8392f);
                hairObject.transform.localScale = new Vector3(1.3f, 1.3f, 1.5f);
                hairObject.GetComponent<MeshRenderer>().material = hairs[counterColor];

                textHair.GetComponent<TextMeshProUGUI>().text = hairNames[counter];
                break;
        }

       
    }

    private void PrevHair()
    {
        Destroy(hairObject);
        UnityEngine.Object pPrefab;
        counter--;
        if (counter == -1)
        {
            counter = files.Count-1;
        }
        GameObject textHair = GameObject.Find("ChooseHair/HairTypeObject");
        textHair = textHair.gameObject.transform.GetChild(1).gameObject;

        switch (counter)
        {
            case 0:
                pPrefab = Resources.Load(files[counter]);
                hairObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, Face.transform);
                hairObject.transform.Rotate(new Vector3(0, 190, 0));
                hairObject.transform.localPosition = new Vector3(-0.0058f, -0.1077f, 2.859f);
                hairObject.transform.localScale = new Vector3(1.15f, 1.1f, 1.5f);
                hairObject.GetComponent<MeshRenderer>().material = hairs[counterColor];
                
                textHair.GetComponent<TextMeshProUGUI>().text = hairNames[counter];
                break;
            case 1:
                pPrefab = Resources.Load(files[counter]);
                hairObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, Face.transform);
                hairObject.transform.Rotate(new Vector3(0, 170, 0));
                hairObject.transform.localPosition = new Vector3(-0.0041f, -0.1077f, 2.8257f);
                hairObject.transform.localScale = new Vector3(0.009f, 0.009f, 0.01f);
                hairObject.GetComponent<MeshRenderer>().material = hairs[counterColor];

                textHair.GetComponent<TextMeshProUGUI>().text = hairNames[counter];
                break;
            case 2:
                pPrefab = Resources.Load(files[counter]);
                hairObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, Face.transform);
                hairObject.transform.Rotate(new Vector3(0, 180, 0));
                hairObject.transform.localPosition = new Vector3(0.008f, -0.1584f, 2.8272f);
                hairObject.transform.localScale = new Vector3(1f, 1f, 1f);
                hairObject.GetComponent<MeshRenderer>().material = hairs[counterColor];

                textHair.GetComponent<TextMeshProUGUI>().text = hairNames[counter];
                break;
            case 3:
                pPrefab = Resources.Load(files[counter]);
                hairObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, Face.transform);
                hairObject.transform.Rotate(new Vector3(0, 190, 0));
                hairObject.transform.localPosition = new Vector3(0.0048f, -0.0949f, 2.8392f);
                hairObject.transform.localScale = new Vector3(1.3f, 1.3f, 1.5f);
                hairObject.GetComponent<MeshRenderer>().material = hairs[counterColor];

                textHair.GetComponent<TextMeshProUGUI>().text = hairNames[counter];
                break;
        }
    }

    private void NextColor()
    {
        counterColor++;
        if (counterColor == hairs.Count)
        {
            counterColor = 0;
        }
        hairObject.GetComponent<MeshRenderer>().material = hairs[counterColor];

        GameObject textColor = GameObject.Find("ChooseHair/HairColorObject");
        textColor = textColor.gameObject.transform.GetChild(1).gameObject;
        textColor.GetComponent<TextMeshProUGUI>().text = colorNames[counterColor];
    }

    private void PrevColor()
    {
        counterColor--;
        if (counterColor == -1)
        {
            counterColor = hairs.Count-1;
        }
        hairObject.GetComponent<MeshRenderer>().material = hairs[counterColor];

        GameObject textColor = GameObject.Find("ChooseHair/HairColorObject");
        textColor = textColor.gameObject.transform.GetChild(1).gameObject;
        textColor.GetComponent<TextMeshProUGUI>().text = colorNames[counterColor];
    }

    public int getCount()
    {
        return counter;
    }

    public int getColor()
    {
        return counterColor;
    }

    public string getImName()
    {
        return imName;
    }

    public List<Material> getHairs()
    {
        return hairs;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
