using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dummiesman;
using System.IO;
using System;
using System.Linq;

public class AnimScript : MonoBehaviour
{
    [SerializeField] private List<Material> hairs = new List<Material>();
    [SerializeField] public int hairCount;
    [SerializeField] public int hairColor;
    public string name;

    private List<GameObject> models = new List<GameObject>();
    private int currentIndex = -1;
    private int counter = 0;
    private string main;
    private int emot;

    private List<GameObject> modelsTalk = new List<GameObject>();
    private List<GameObject> modelsBlink = new List<GameObject>();
    private List<GameObject> modelsWow = new List<GameObject>();
    private List<GameObject> modelsSmile = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        main = Application.dataPath + "\\Resources\\Faces\\" + name;
        emot = 0;

        this.LoadHair(hairCount, hairColor);

        string filePath = main + "\\" + name + "Talk";
        int len = Directory.GetFiles(main + "\\" + name + "Talk", "*", SearchOption.TopDirectoryOnly).Length;
        filePath = filePath + "\\animation";

        if (Directory.Exists(main + "\\" + name + "Talk"))
        {
            for (int i = 0; i < len; i++)
            {
                filePath = filePath + " " + i.ToString() + "\\" + name + ".obj";
                var pPrefab = new OBJLoader().Load(filePath);
                GameObject pNewObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, this.transform);
                modelsTalk.Add(pNewObject);
                modelsTalk[i].transform.Rotate(new Vector3(0, 190, 0));
                modelsTalk[i].gameObject.transform.GetChild(0).transform.localScale = new Vector3(30, 29, 25);
                filePath = main + "\\" + name + "Talk" + "\\animation";
                Destroy(pPrefab);
            }
            foreach (var model in modelsTalk)
            {
                model.SetActive(false);
            }
            this.Talking();
        }

        if (Directory.Exists(main + "\\" + name + "Blink"))
        {
            filePath = main + "\\" + name + "Blink";
            len = Directory.GetFiles(main + "\\" + name + "Blink", "*", SearchOption.TopDirectoryOnly).Length;
            filePath = filePath + "\\animation";

            for (int i = 0; i < len; i++)
            {
                filePath = filePath + " " + i.ToString() + "\\" + name + ".obj";
                var pPrefab = new OBJLoader().Load(filePath);
                GameObject pNewObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, this.transform);
                modelsBlink.Add(pNewObject);
                modelsBlink[i].transform.Rotate(new Vector3(0, 190, 0));
                modelsBlink[i].gameObject.transform.GetChild(0).transform.localScale = new Vector3(30, 29, 25);
                filePath = main + "\\" + name + "Blink" + "\\animation";
                Destroy(pPrefab);
            }
            foreach (var model in modelsBlink)
            {
                model.SetActive(false);
            }
            this.Blinking();
        }


        if (Directory.Exists(main + "\\" + name + "Wow"))
        {
            filePath = main + "\\" + name + "Wow";
            len = Directory.GetFiles(main + "\\" + name + "Wow", "*", SearchOption.TopDirectoryOnly).Length;
            filePath = filePath + "\\animation";

            for (int i = 0; i < len; i++)
            {
                filePath = filePath + " " + i.ToString() + "\\" + name + ".obj";
                var pPrefab = new OBJLoader().Load(filePath);
                GameObject pNewObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, this.transform);
                modelsWow.Add(pNewObject);
                modelsWow[i].transform.Rotate(new Vector3(0, 190, 0));
                modelsWow[i].gameObject.transform.GetChild(0).transform.localScale = new Vector3(30, 29, 25);
                filePath = main + "\\" + name + "Wow" + "\\animation";
                Destroy(pPrefab);
            }
            foreach (var model in modelsWow)
            {
                model.SetActive(false);
            }
            this.Wowing();
        }

        if (Directory.Exists(main + "\\" + name + "Smile"))
        {
            filePath = main + "\\" + name + "Smile";
            len = Directory.GetFiles(main + "\\" + name + "Smile", "*", SearchOption.TopDirectoryOnly).Length;
            filePath = filePath + "\\animation";

            for (int i = 0; i < len; i++)
            {
                filePath = filePath + " " + i.ToString() + "\\" + name + ".obj";
                var pPrefab = new OBJLoader().Load(filePath);
                GameObject pNewObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, this.transform);
                modelsSmile.Add(pNewObject);
                modelsSmile[i].transform.Rotate(new Vector3(0, 190, 0));
                modelsSmile[i].gameObject.transform.GetChild(0).transform.localScale = new Vector3(30, 29, 25);
                filePath = main + "\\" + name + "Smile" + "\\animation";
                Destroy(pPrefab);
            }
            foreach (var model in modelsSmile)
            {
                model.SetActive(false);
            }
            this.Smiling();
        }

        
    }

    void Talking()
    {
        if (modelsTalk.Count == 0)
            return;
        if (currentIndex != -1)
        {
            models[currentIndex].SetActive(false);
            currentIndex = 0;
        }
        models = modelsTalk;
    }

    void Blinking()
    {
        if (modelsBlink.Count == 0)
            return;
        if (currentIndex != -1)
        {
            models[currentIndex].SetActive(false);
            currentIndex = 0;
        }
        models = modelsBlink;
    }

    void Wowing()
    {
        if (modelsWow.Count == 0)
            return;
        if (currentIndex != -1)
        {
            models[currentIndex].SetActive(false);
            currentIndex = 0;
        }
        models = modelsWow;
    }

    void Smiling()
    {
        if (modelsSmile.Count == 0)
            return;
        if (currentIndex != -1)
        {
            models[currentIndex].SetActive(false);
            currentIndex = 0;
        }
        models = modelsSmile;
    }


    private void LoadHair(int id, int mat)
    {
        UnityEngine.Object pPrefab;
        GameObject hairObject;

        string[] filesStr = Directory.GetFiles(Application.dataPath + "\\Resources\\Hair", "*", SearchOption.TopDirectoryOnly);
        List<string> files = filesStr.ToList<string>();
        files.RemoveAll(s => s.Substring(s.Length - 5).Contains("meta"));
        for (int i = 0; i < files.Count; i++)
        {

            int index = files[i].LastIndexOf("Hair", files[i].Length);
            int index2 = files[i].LastIndexOf(".prefab", files[i].Length);
            files[i] = files[i].Substring(index, index2 - index);
        }
        switch (id)
        {
            case 0:
                pPrefab = Resources.Load(files[id]);
                hairObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, this.transform);
                hairObject.transform.Rotate(new Vector3(0, 190, 0));
                hairObject.transform.localPosition = new Vector3(-0.02f, -0.15f, 1.29f);
                hairObject.transform.localScale = new Vector3(37f, 35f, 42f);
                hairObject.GetComponent<MeshRenderer>().material = hairs[mat];

                break;
            case 1:
                pPrefab = Resources.Load(files[id]);
                hairObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, this.transform);
                hairObject.transform.Rotate(new Vector3(0, 176.685f, 0));
                hairObject.transform.localPosition = new Vector3(-0.1f, -0.04f, 1.42f);
                hairObject.transform.localScale = new Vector3(0.32f, 0.29f, 0.33f);
                hairObject.GetComponent<MeshRenderer>().material = hairs[mat];

                break;
            case 2:
                pPrefab = Resources.Load(files[id]); 
                hairObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, this.transform);
                hairObject.transform.Rotate(new Vector3(0, -171.069f, 0));
                hairObject.transform.localPosition = new Vector3(0.16f, -1.41f, 0.65f);
                hairObject.transform.localScale = new Vector3(29f, 32f, 29f);
                hairObject.GetComponent<MeshRenderer>().material = hairs[mat];

                break;
            case 3:
                pPrefab = Resources.Load(files[id]); 
                hairObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity, this.transform);
                hairObject.transform.Rotate(new Vector3(0, -163.326f, 0));
                hairObject.transform.localPosition = new Vector3(0.17f, 0.69f, 0.84f);
                hairObject.transform.localScale = new Vector3(38f, 37f, 38f);
                hairObject.GetComponent<MeshRenderer>().material = hairs[mat];

                break;
        }


    }

    public void setHair(List<Material> hairToGet)
    {
        hairs = hairToGet;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter++;
        if (counter == 6) 
        {
            if (models.Count > 0)
            {
                if (currentIndex >= 0)
                {
                    models[currentIndex].SetActive(false);
                }

                currentIndex = (currentIndex + 1) % models.Count;

                models[currentIndex].SetActive(true);
                counter = 0;
            }
        }
    }

    void Update()
    {
        bool changed = false;
        if (Input.GetKey("space"))
        {
            emot++;
            changed = true;
            if (emot > 4)
            {
                emot = 0;
            }
        }

        if (changed)
        {
            switch (emot)
            {
                case 0:
                    this.Talking();
                    break;
                case 1:
                    this.Smiling();
                    break;  
                case 2:
                    this.Blinking();
                    break;
                case 3:
                    this.Wowing();
                    break;
            }
        }

    }
}
