using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour {

    //数据类
    [Serializable]
    public class Data
    {
        public Sprite BG;
        public Sprite Lihui;
        public AudioClip BGM;
        public string Name;
        public string[] Dialogues;
        public AudioClip[] DialoguesBGM;
    }

    public Data[] Scenes;


    [Header("-----------------获取组件-----------------")]
    //获得组件
    private Text NameBox;
    private Text DialoguesBox;
    private Image BGBox;
    private Image lihuiBox;
    private AudioSource BGMBox;
    private AudioSource DialpguesBGMBox;

    [Header("-----------------index-----------------")]
    //index
    private int ScenesIndex = 0;
    private int DialoguesIndex = 0;//由于语音和对话同时所以共用而且必须每句话都有配音


    //变量
    public float TextSpeed = 0.02f;


	// Use this for initialization
	void Awake () {
        BGBox = GameObject.Find("BG").GetComponent<Image>();
        lihuiBox = GameObject.Find("lihui").GetComponent<Image>();
        NameBox = GameObject.Find("nameBox").GetComponent<Text>();
        DialoguesBox = GameObject.Find("DialoguesBox").GetComponent<Text>();
        BGMBox = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        DialpguesBGMBox = GameObject.Find("DialoguesBGM").GetComponent<AudioSource>();
    }

    void Start()
    {
        LoadScenes();
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            //判断是否还有对话
            if(DialoguesIndex==Scenes[ScenesIndex].Dialogues.Length)
            {
                //场景对话完成加载下一场景
                Debug.Log("场景" + ScenesIndex + "结束");
                if(ScenesIndex>=Scenes.Length-1)
                {
                    Debug.Log("游戏结束");
                    return;
                }
                else
                    ChangeScenes();
            }
            if (DialoguesIndex <= Scenes[ScenesIndex].DialoguesBGM.Length-1)
            {
                DialpguesBGMBox.clip = Scenes[ScenesIndex].DialoguesBGM[DialoguesIndex];
                DialpguesBGMBox.Play();
            }
            if (DialoguesIndex == 2 && ScenesIndex == 2)
            {
                DialoguesBox.fontSize = 150;
            }
            else
            {
                DialoguesBox.fontSize = 40;
            }
            DialoguesBox.text = "";
            StartCoroutine(LoadText());
            //DialoguesBox.text = Scenes[ScenesIndex].Dialogues[DialoguesIndex];
            DialoguesIndex++;
        }
	}
    private void ChangeScenes()
    {
        ScenesIndex++;
        DialoguesIndex = 0;
        LoadScenes();
    }

    private void LoadScenes()
    {
        BGBox.sprite = Scenes[ScenesIndex].BG;
        lihuiBox.sprite = Scenes[ScenesIndex].Lihui;
        NameBox.text = Scenes[ScenesIndex].Name;
        DialoguesBox.text = Scenes[ScenesIndex].Dialogues[DialoguesIndex];
        BGMBox.clip = Scenes[ScenesIndex].BGM;
        BGMBox.Play();
    }

    private IEnumerator LoadText()
    {
        foreach(char letter in Scenes[ScenesIndex].Dialogues[DialoguesIndex])
        {
            DialoguesBox.text += letter;
            yield return new WaitForSeconds(TextSpeed);
        }
    }
}