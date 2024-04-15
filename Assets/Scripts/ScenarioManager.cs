using UnityEngine;  //Unityのゲームエンジンの機能を使うための名前空間
using TMPro;        //テキスト表示に特化した機能を使うための名前空間

//シナリオを管理するスクリプト
public class ScenarioManager : MonoBehaviour 
{
    //////////////////////
    //外部テキストの情報//
    //////////////////////
    
    [SerializeField] TextMeshProUGUI _mainTextObject;   //外部テキストオブジェクト
    [SerializeField]int _lineCount;                     //外部テキストの行数

    //////////////////////////
    //シナリオ表示で扱う情報//
    //////////////////////////
    
    int _length;        //現在表示中の文字の長さ
    int _wholeLength;   //現在表示中の行の文字の総数

    float _time;        //文字を表示するタイミングを管理
    float _feedTime;    //１文字を表示する間隔を管理


    ////////////////
    //スタート関数//
    ////////////////
    
    void Start()
    {
        //////////////////
        //各時間の初期化//
        //////////////////
        
        _time = 0f;
        _feedTime = 0.05f;

        //////////////////////
        //外部テキストの処理//
        //////////////////////
        
        // MaterialChangeスクリプトを用いて、現在の行のテキストを表示、または命令を実行
        string line = GameManager.Instance.M_script.GetCurrentLine();

        //命令かどうかを判定
        if (GameManager.Instance.M_script.Judgement_Order(line))
        {
            //命令の場合は、命令を実行する
            GameManager.Instance.M_script.ExecuteLine(line);
            //次の行へ
            NextLine();
        }
        //テキストならば表示
        DisplayText();
    }

    ////////////////////
    //アップデート関数//
    ////////////////////
    
    void Update()
    {
        /////////////////////////////////
        // 文章を１文字ずつ表示する処理//
        /////////////////////////////////
        
        _time += Time.deltaTime; //経過時間を更新
        
        //条件（１文字表示時間が経過）を満たすと、1文字表示する処理が実行
        if (_time >= _feedTime)
        {
            _time -= _feedTime;

            //まだ文字が全て表示されていなければ
            if (!Judgement_NextLine())
            {
                //表示中の文字数カウントを１文字増やす
                _length++;
                _mainTextObject.maxVisibleCharacters = _length;
            }
        }

        ////////////////////////////
        //クリックされたときの処理//
        ////////////////////////////
        
        if (Input.GetMouseButtonUp(0))
        {
            //外部テキストを最後の行まで読み込んでいるか判定
            if (GameManager.Instance.lineNumber >= _lineCount)
            {
                // Updateメソッドの実行を停止する
                enabled = false;
            }
            else 
            {
                //文字が全て表示されているか判定
                if (Judgement_NextLine())
                {
                    //次の行に移動
                    NextLine();

                    //文を表示する
                    DisplayText();
                }

                //まだ文字が表示されていなければ
                else
                {
                    //全ての文字を表示する
                    _length = _wholeLength;
                }
            }
        }
    }
    
    //////////////////////////////////////////////
    //文字を最後まで表示したかを判定するメソッド//
    //////////////////////////////////////////////
    
    public bool Judgement_NextLine()
    {
        ////////////
        //文の取得//
        ////////////
        
        // GameManagerのインスタンスを取得
        GameManager gameManager = GameManager.Instance;

        //現在の行の文を取得
        string line = gameManager.M_script.GetCurrentLine(); 

        ////////////////////////////////
        //文章を読み切ったかどうか判定//
        ////////////////////////////////
        
        //現在の行の文字数と現在の表示中の文字数を比較する
        _wholeLength = line.Length;

        //判定結果を返す
        return (_length > line.Length);
    }

    ////////////////////////////
    //次の行を実行するメソッド//
    ////////////////////////////
    
    public void NextLine()
    {
        //////////////////////////////////////
        //シナリオ表示に扱うデータのリセット//
        //////////////////////////////////////
        
        _length = 0;
        _time = 0f;
        _mainTextObject.maxVisibleCharacters = 0;

        //行カウントを増やす
        GameManager.Instance.lineNumber++;

        ////////////////////////////////////////
        //外部テキストを最後まで実行したか判定//
        ////////////////////////////////////////

        // もし行数が最後の行までたどり着いていたら処理を終了する
        if (GameManager.Instance.lineNumber >= _lineCount)
        {
            return;
        }

        //行の文章を取得
        string line = GameManager.Instance.M_script.GetCurrentLine();

        //命令なら命令を実行
        if (GameManager.Instance.M_script.Judgement_Order(line)) 
        {
            GameManager.Instance.M_script.ExecuteLine(line);
            NextLine();
        }
    }
    
    //////////////////////////////
    //テキストを表示するメソッド//
    //////////////////////////////
    
    public void DisplayText()
    {
        string line = GameManager.Instance.M_script.GetCurrentLine();
        _mainTextObject.text = line;
    }
}