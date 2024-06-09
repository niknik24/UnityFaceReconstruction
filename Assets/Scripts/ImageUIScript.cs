using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Events;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class ImageUIScript : MonoBehaviour
{
    public GameObject img;
    public UnityEvent onNext;
    private string nameIm;
    private List<string> files;
    [SerializeField] private Button Next;
    [SerializeField] private GameObject rowObject;
    // Start is called before the first frame update
    void Start()
    {

        int len = Directory.GetFiles(Application.dataPath + "\\" + "Resources" +"\\"+ "Images", "*", SearchOption.TopDirectoryOnly).Length;
        string[] filesStr = Directory.GetFiles(Application.dataPath + "\\" + "Resources" + "\\" + "Images", "*", SearchOption.TopDirectoryOnly);
        files = filesStr.ToList<string>();
        files.RemoveAll(s => s.Substring(s.Length - 5).Contains("meta"));
        len = len / 2;
        for (int i = 0; i < files.Count; i++)
        {
            int index = files[i].LastIndexOf(".png", files[i].Length);
            int index2 = 0;
            if (index == -1)
            {
                index = files[i].LastIndexOf(".jpg");
                index2 = files[i].LastIndexOf("Images", index - 1);
            }
            else
                index2 = files[i].LastIndexOf("Images", index-1);
            files[i] = files[i].Substring(index2, index-index2);
        }



        for (int i = 0; i <= len/7; i++)
        {
            GameObject newRow = (GameObject)GameObject.Instantiate(rowObject, 
                Vector3.zero, Quaternion.identity, this.transform.Find("Scroll/List").gameObject.transform);
            newRow.transform.localPosition = new Vector3(0, 0, 0);
            for (int j = 0; j < 7; j++)
            {
                if (7*i+j >= len) { break; }
                GameObject image = (GameObject)GameObject.Instantiate(img, Vector3.zero, Quaternion.identity, newRow.transform);
                var spr = Resources.Load<Texture2D>(files[7*i+j]);
                var sprite = TextureToSprite(spr);
                image.GetComponent<Image>().sprite = sprite;
                image.transform.localPosition = new Vector3(0, 0, 0);
                if ((i == 0) && (j == 0))
                {
                    image.GetComponent<Button>().Select();
                    nameIm = files[0];
                }
                int d = 7*i+j;
                image.GetComponent<Button>().onClick.AddListener(delegate { ParameterOnClick(d); });
            }
            
        }

        GameObject scrollRect = this.transform.Find("Scroll").gameObject;
        scrollRect.GetComponent<UnityEngine.UI.ScrollRect>().enabled = false;
        GameObject scroll = this.transform.Find("Scroll/Scrollbar").gameObject;
        scroll.SetActive(false);

        if (len > 14)
        {
            GameObject newRow2 = (GameObject)GameObject.Instantiate(rowObject, Vector3.zero, Quaternion.identity, this.transform.Find("Scroll/List").gameObject.transform);
            scroll = this.transform.Find("Scroll/Scrollbar").gameObject;
            scrollRect.GetComponent<UnityEngine.UI.ScrollRect>().enabled = true;
            scroll.SetActive(true);
            scroll.GetComponent<UnityEngine.UI.Scrollbar>().value = 1;

        }
        
        Next.GetComponent<Button>().onClick.AddListener(NextPressed);

    }

    private void ParameterOnClick(int test)
    {
        nameIm = files[test];
    }

    private void NextPressed()
    {
        onNext.Invoke();
    }

    public string getNameIm()
    {
        return nameIm;
    }


    public static Sprite TextureToSprite(Texture2D texture) => Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 50f, 0, SpriteMeshType.FullRect);

    // Update is called once per frame
    void Update()
    {
        
    }
}
