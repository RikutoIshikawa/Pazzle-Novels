using UnityEngine; //Unityのゲームエンジンの機能を使うための名前空間

//簡単にパラメータを操作できるクラス
[CreateAssetMenu]
public class ParamsSO : ScriptableObject
{
    [Header("敵の初期HP")]
    public int initEnemyHP;

    [Header("敵の初期HPゲージ用")]
    public float initEnemyHPGauge;

    [Header("プレイヤーの初期HP")]
    public int initPlayerHP;

    [Header("プレイヤーの初期HPゲージ用")]
    public float initPlayerHPGauge;

    [Header("初期のブロック数")]
    public int initBlockCount;

    [Header("ブロックの判定距離")]
    public float BlockDistance;

    [Header("Swordブロック１つの攻撃力")]
    public int SwordPoint;

    [Header("Ladleブロック１つの攻撃力")]
    public int LadlePoint;

    [Header("Hertブロック１つの回復量")]
    public int HeartPoint;

    [Header("Skullブロック１つのダメージ量")]
    public int SkullPoint;

    //MyScriptableObjectが保存してある場所のパス
    public const string PATH = "ParamsSO";

    //MyScriptableObjectの実体
    private static ParamsSO _entity;
    public static ParamsSO Entity
    {
        get
        {
            //初アクセス時にロードする
            if (_entity == null)
            {
                _entity = Resources.Load<ParamsSO>(PATH);

                //ロード出来なかった場合はエラーログを表示
                if (_entity == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }

            return _entity;
        }
    }
}
