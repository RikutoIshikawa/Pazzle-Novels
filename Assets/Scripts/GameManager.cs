using UnityEngine; //Unityのゲームエンジンの機能を使うための名前空間

//ノベルゲームの管理を行うスクリプト
public class GameManager : MonoBehaviour
{
    //シングルトン：どのコードからもアクセスできる
    public static GameManager Instance { get; private set; }
   
    //////////////////////
    //各スクリプトの参照//
    //////////////////////
    
    public ScenarioManager S_script;        //シナリオを管理するスクリプト
    public MaterialChange M_script;         //シナリオから素材表示切替や音源切り替えをするスクリプト
    public BG_Operation BG_script;          //背景画像やギャラリー管理するスクリプト
    public Item_Operation I_script;         //アイテムを管理するスクリプト
    public Character_Operation C_script;    //キャラクターを管理するスクリプト

    //現在読んでいる行数：クリックのたびに１行増える
    [System.NonSerialized] public int lineNumber;

    //////////////
    //初期化関数//
    //////////////
    
    void Awake()
    {
        Instance = this;    //シングルトンの設定

        lineNumber = 0;     //行番号を初期化
    }
}