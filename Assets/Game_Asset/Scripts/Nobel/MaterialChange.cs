using System.Collections.Generic; //ジェネリック（汎用的な）コレクションクラスやインターフェースを操作するための名前空間
using UnityEngine; //Unityのゲームエンジンの機能を使うための名前空間
using System.IO; //ファイルに対する基本的なサポートを提供する型を使用するための名前空間

//シナリオから素材表示切替や音源切り替えをするスクリプト
public class MaterialChange : MonoBehaviour
{
    [SerializeField] TextAsset _textFile; //シナリオとなる外部テキスト

    List<string> _lineList = new List<string>(); //文章を１行ごとに保存するためのリスト

    public GameObject panelObject; // 表示切り替えをするパネル

    //テキストファイルの内容を1行ずつ読み込んで、リストに格納する。
    void Awake()
    {
        StringReader reader = new StringReader(_textFile.text); // テキストファイルの中身を、１行ずつリストに入れておく

        //ファイルの末尾に達するまで、1行ずつ読み込む。
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); //1行読み込んだら、その行を取得
            _lineList.Add(line); //取得した行をリストに追加
        }
    }
    
    void Start()
    {
        SetPanelVisibility(false); //パネルを非表示
    }

    //パネルの状態の取得
    void Update()
    {
        //キーを受け取ったらパネルの表示設定を変更する
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePanelVisibility();
        }
    }

    // 現在の行の文を取得するメソッド
    public string GetCurrentLine()
    {
        // もし行数が最後の行までたどり着いていたら最後の行を返却する
        if (GameManager.Instance.lineNumber >= _lineList.Count)
        {
            return _lineList[_lineList.Count-1];
        }
        //そうでなければ現在の行を返却する
        return _lineList[GameManager.Instance.lineNumber];
    }

    //渡された文が「命令」かどうかを判定するメソッド
    public bool Judgement_Order(string line)
    {
        //文が"&"で始まっている場合、それは命令とする
        if (line[0] == '&')
        {
            return true;
        }
            return false;
    }

    //命令を実行するメソッド
    public void ExecuteLine(string line)
    {
        string[] words = line.Split(' '); // 命令をスペースで区切って分割

        // 命令の種類によって異なる処理を行う
        switch (words[0])
        {
            //img：背景画像を配置する命令
            case "&img":
                GameManager.Instance.BG_script.PutImage(words[1], words[2]);
                break;
            //rmimg：背景画像を削除する命令
            case "&rmimg":
                GameManager.Instance.BG_script.RemoveImage(words[1]);
                break;

            //item：アイテムを配置する命令
            case "&item":
                GameManager.Instance.I_script.PutImage(words[1], words[2]);
                break;
            //rmitem：アイテムを削除する命令
            case "&rmitem":
                GameManager.Instance.I_script.RemoveImage(words[1]);
                break;

            //character：キャラクターを配置する命令
            case "&character":
                GameManager.Instance.C_script.PutImage(words[1], words[2]);
                break;
            //rmcharacter2：キャラクターを削除する命令
            case "&rmcharacter":
                GameManager.Instance.C_script.RemoveImage(words[1]);
                break;

            //togglePanel：設定したパネルの表示/非表示を切り替える
            case "&togglePanel":
                TogglePanelVisibility();
            break;
            
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
    
    //パネルの現在の状態を取得し、逆の状態にセットするメソッド
    private void TogglePanelVisibility()
    {
        bool currentVisibility = panelObject.activeSelf;
        SetPanelVisibility(!currentVisibility);
    }
    
    //指定した状態にパネルを設定するメソッド
    private void SetPanelVisibility(bool isVisible)
    {
        panelObject.SetActive(isVisible);
    }
}