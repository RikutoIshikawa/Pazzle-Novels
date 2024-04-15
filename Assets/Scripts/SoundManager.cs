using UnityEngine; //Unityのゲームエンジンの機能を使うための名前空間

//BGMやSEの管理をするスクリプト
public class SoundManager : MonoBehaviour
{
    //BGMの構造
    [SerializeField] AudioSource audioSourceBGM = default; //スピーカー
    [SerializeField] AudioClip[] bgmClips = default; //CD

    //SEの構造
    [SerializeField] AudioSource audioSourceSE = default; //スピーカー
    [SerializeField] AudioClip[] seClips = default; //CD

    //BGMの代入：列挙型
    public enum BGM 
    {
        StartBGM,
        LifeBGM, 
        UnrestBGM, 
        BattleBGM, 
        FinalBGM
    }

    //SEの代入：列挙型
    public enum SE
    {
        StartButton,    //スタートボタンを押した音
        Button,         //ボタンを押した音
        Block,          //ブロックに触れた音
        Sword,          //剣の攻撃音
        Ladle,          //お玉の攻撃音
        Heart,          //ハートの回復音
        Skull,          //毒のダメージ音
        LastAttack,     //最後の攻撃音
        Victory,        //勝利音
        Creature,       //クリーチャーの鳴き声
    }

    //シングルトン：どのコードからもアクセスできる
    public static SoundManager instance;

    //////////////
    //初期化関数//
    //////////////
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        playBGM(SoundManager.BGM.StartBGM);
    }

    ///////////////////////////////
    //指定したBGMを鳴らすメソッド//
    ///////////////////////////////
    public void playBGM(BGM bgm) {
        audioSourceBGM.clip = bgmClips[(int)bgm];
        audioSourceBGM.Play();
    }

    ///////////////////////////////
    //指定したBGMを止めるメソッド//
    ///////////////////////////////
    public void stopBGM(BGM bgm)
    {
        audioSourceBGM.clip = bgmClips[(int)bgm];
        audioSourceBGM.Stop();
    }

    //////////////////////////////
    //指定したSEを鳴らすメソッド//
    //////////////////////////////
    public void playSE(SE se)
    {
        audioSourceSE.PlayOneShot(seClips[(int)se]);
    }
}
