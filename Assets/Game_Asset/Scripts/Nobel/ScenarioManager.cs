using UnityEngine; //Unityのゲームエンジンの機能を使うための名前空間
using TMPro; //テキスト表示に特化した機能を使うための名前空間

//シナリオを管理するスクリプト
public class ScenarioManager : MonoBehaviour 
{
    [SerializeField] TextMeshProUGUI _mainTextObject; //外部テキストオブジェクト
    [SerializeField]int _lineCount; //外部テキストの行数

    int _length; //現在表示中の文字の長さ
    int _wholeLength; //現在表示中の行の文字の総数

    float _time; //文字を表示するタイミングを管理
    float _feedTime; //１文字を表示する間隔を管理

    //_time、_feedTimeの初期化と最初の行のテキスト表示や命令の実行処理
    void Start()
    {
        //初期化
        _time = 0f;
        _feedTime = 0.05f;

        // MaterialChangeスクリプトを用いて、最初の行のテキストを表示、または命令を実行
        string line = GameManager.Instance.M_script.GetCurrentLine();

        //命令かどうかを判定し実行
        if (GameManager.Instance.M_script.Judgement_Order(line))
        {
            GameManager.Instance.M_script.ExecuteLine(line);
            //次の行へ
            NextLine();
        }
        //テキストならば表示
        DisplayText();
    }

    //文字を1文字ずつ表示する処理と、クリックによって次の行に進む処理を行うメソッド
    void Update()
    {
        // 文章を１文字ずつ表示する処理
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

        // クリックされたとき
        if (Input.GetMouseButtonUp(0))
        {
            //外部テキストを最後の行まで読み込んでいたら、クリックによる進行を終了する
            if (GameManager.Instance.lineNumber >= _lineCount)
            {
                enabled = false; // Updateメソッドの実行を停止する
            }
            else 
            {
                //文字が全て表示されていれば
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
    
    //文字を最後まで表示したかを判定するメソッド
    public bool Judgement_NextLine()
    {
        // GameManagerのインスタンスを取得
        GameManager gameManager = GameManager.Instance;

        string line = gameManager.M_script.GetCurrentLine(); //行の文章を取得

        //文章を読み切ったかどうか判定
        _wholeLength = line.Length;
        return (_length > line.Length);
    }

    //次の行に進むメソッド。
    public void NextLine()
    {
        _length = 0;
        _time = 0f;
        _mainTextObject.maxVisibleCharacters = 0;

        GameManager.Instance.lineNumber++; //行カウントを増やす

        // もし行数が最後の行までたどり着いていたら処理を終了する
        if (GameManager.Instance.lineNumber >= _lineCount)
        {
            return;
        }

        string line = GameManager.Instance.M_script.GetCurrentLine(); //行の文章を取得

        //命令なら命令を実行
        if (GameManager.Instance.M_script.Judgement_Order(line)) 
        {
            GameManager.Instance.M_script.ExecuteLine(line);
            NextLine();
        }
    }
    
    //テキストを表示するメソッド
    public void DisplayText()
    {
        string line = GameManager.Instance.M_script.GetCurrentLine();
        _mainTextObject.text = line;
    }
}