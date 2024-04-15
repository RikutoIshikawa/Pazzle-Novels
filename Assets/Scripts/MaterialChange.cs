using System.Collections.Generic;   //オブジェクトのリストを扱うための名前空間
using UnityEngine;                  //Unityのゲームエンジンの機能を使うための名前空間
using System.IO;                    //ファイルに対する基本的なサポートを提供する型を使用するための名前空間

//シナリオから素材表示切替、パネル切り替え、音源切り替えをするスクリプト
public class MaterialChange : MonoBehaviour
{
    //////////////////
    //シナリオデータ//
    //////////////////

    //シナリオとなる外部テキスト
    [SerializeField] TextAsset _textFile;

    //テキストを１行ごとに保存するためのリスト
    List<string> _lineList = new List<string>();

    //////////////////////////////////////////////
    //外部テキストにより表示切り替えをするパネル//
    //////////////////////////////////////////////

    public GameObject panelObject;

    ////////////////////////////////////////////////////////
    //外部テキストからシナリオを１行づつ読み込む初期化関数//
    ////////////////////////////////////////////////////////
    
    void Awake()
    {
        //テキストファイルの読み込み
        StringReader reader = new StringReader(_textFile.text);

        //ファイルの末尾に達するまで繰り返し
        while (reader.Peek() != -1)
        {
            //１行読み込み
            string line = reader.ReadLine();

            //取得した行をリストに追加
            _lineList.Add(line);
        }
    }
    
    ////////////////
    //スタート関数//
    ////////////////
    
    void Start()
    {
        //パネルを非表示にしておく
        SetPanelVisibility(false);
    }

    ////////////////////
    //アップデート関数//
    ////////////////////
    
    void Update()
    {
        //キーを受け取ったらパネルの表示設定を変更する
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePanelVisibility();
        }
    }

    ///////////////////////////////////
    // 現在の行の文を取得するメソッド//
    ///////////////////////////////////
    
    public string GetCurrentLine()
    {
        //現在の行が最後の行かどうか判定
        if (GameManager.Instance.lineNumber >= _lineList.Count)
        {
            //最後の行を繰り返し実行する
            return _lineList[_lineList.Count-1];
        }

        //そうでなければ現在の行を返却する
        return _lineList[GameManager.Instance.lineNumber];
    }

    //////////////////////////////////////////////////
    //渡された文が「命令」かどうかを判定するメソッド//
    //////////////////////////////////////////////////
    
    public bool Judgement_Order(string line)
    {
        //文が"&"で始まっている場合
        if (line[0] == '&')
        {
            //命令であると返す
            return true;
        }
            return false;
    }

    //////////////////////////
    //命令を実行するメソッド//
    //////////////////////////
    
    public void ExecuteLine(string line)
    {
        // 命令文をスペースで区切って分割
        string[] words = line.Split(' ');

        ////////////////////
        //命令の判別と実行//
        ////////////////////
        
        // 命令の種類によって異なる処理を行う
        switch (words[0])
        {
            //////////////////////
            //BG_Operationの実行//
            //////////////////////
            
            //img：背景画像を配置する命令
            case "&img":
                GameManager.Instance.BG_script.PutImage(words[1], words[2]);
                break;
            //rmimg：背景画像を削除する命令
            case "&rmimg":
                GameManager.Instance.BG_script.RemoveImage(words[1]);
                break;

            ////////////////////////
            //Item_Operationの実行//
            ////////////////////////
            
            //item：アイテムを配置する命令
            case "&item":
                GameManager.Instance.I_script.PutImage(words[1], words[2]);
                break;
            //rmitem：アイテムを削除する命令
            case "&rmitem":
                GameManager.Instance.I_script.RemoveImage(words[1]);
                break;

            /////////////////////////////
            //Character_Operationの実行//
            /////////////////////////////
            
            //character：キャラクターを配置する命令
            case "&character":
                GameManager.Instance.C_script.PutImage(words[1], words[2]);
                break;
            //rmcharacter2：キャラクターを削除する命令
            case "&rmcharacter":
                GameManager.Instance.C_script.RemoveImage(words[1]);
                break;

            ////////////////////
            //パネルの表示切替//
            ////////////////////
            
            //togglePanel：設定したパネルの表示/非表示を切り替える
            case "&togglePanel":
                TogglePanelVisibility();
            break;
            
            //////////////////////
            //SoundManagerの実行//
            //////////////////////
            
            //sound：BGMを流す命令
            case "&playsound":
                //キーワードにより流すBGMを変える
                if (words[1] == "S")
                {
                    SoundManager.instance.playBGM(SoundManager.BGM.StartBGM);
                }
                else if (words[1] == "L")
                {
                    SoundManager.instance.playBGM(SoundManager.BGM.LifeBGM);
                }
                else if (words[1] == "U")
                {
                    SoundManager.instance.playBGM(SoundManager.BGM.UnrestBGM);
                }
                else if (words[1] == "B")
                {
                    SoundManager.instance.playBGM(SoundManager.BGM.BattleBGM);
                }
                else if (words[1] == "F")
                {
                    SoundManager.instance.playBGM(SoundManager.BGM.FinalBGM);
                }
                else
                {
                    return;
                }
                break;

            //sound：BGMをストップする命令
            case "&stopsound":
                if (words[1] == "S")
                {
                    SoundManager.instance.stopBGM(SoundManager.BGM.StartBGM);
                }
                else if (words[1] == "L")
                {
                    SoundManager.instance.stopBGM(SoundManager.BGM.LifeBGM);
                }
                else if (words[1] == "U")
                {
                    SoundManager.instance.stopBGM(SoundManager.BGM.UnrestBGM);
                }
                else if (words[1] == "B")
                {
                    SoundManager.instance.stopBGM(SoundManager.BGM.BattleBGM);
                }
                else if (words[1] == "F")
                {
                    SoundManager.instance.stopBGM(SoundManager.BGM.FinalBGM);
                }
                else
                {
                    return;
                }
                break;

            //se：SEを流す命令
            case "&se":
                //キーワードにより流すSEを変える
                if (words[1] == "C")
                {
                    SoundManager.instance.playSE(SoundManager.SE.Creature);
                }
                else
                {
                    return;
                }
                break;
        }
    }
    
    ////////////////////////////////////////////////////////////
    //パネルの現在の状態を取得し、逆の状態にセットするメソッド//
    ////////////////////////////////////////////////////////////
    
    private void TogglePanelVisibility()
    {
        //現在の状態を取得
        bool currentVisibility = panelObject.activeSelf;

        //逆の状態へ変更
        SetPanelVisibility(!currentVisibility);
    }
    
    //////////////////////////////////////////
    //指定した状態にパネルを設定するメソッド//
    //////////////////////////////////////////
    
    private void SetPanelVisibility(bool isVisible)
    {
        panelObject.SetActive(isVisible);
    }
}