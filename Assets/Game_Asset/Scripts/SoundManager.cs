using UnityEngine; //Unityのゲームエンジンの機能を使うための名前空間

//BGMやSEの管理をするスクリプト
public class SoundManager : MonoBehaviour
{
    //BGM
    [SerializeField] AudioSource audioSourceBGM = default; //スピーカー
    [SerializeField] AudioClip[] bgmClips = default; //CD

    //SE
    [SerializeField] AudioSource audioSourceSE = default; //スピーカー
    [SerializeField] AudioClip[] seClips = default; //CD

    //BGM列挙型
    public enum BGM 
    {
        StartBGM,
        LifeBGM, 
        UnrestBGM, 
        BattleBGM, 
        FinalBGM
    }

    //SE列挙型
    public enum SE
    {
        StartButton, //スタートボタンを押した音
        Button, //ボタンを押した音
        Block, //ブロックに触れた音
        Sword, //剣の攻撃音
        Ladle, //お玉の攻撃音
        Heart, //ハートの回復音
        Skull, //毒のダメージ音
        LastAttack, //最後の攻撃音
        Victory, //勝利音
        Creature, //クリーチャーの鳴き声
    }

    //シングルトン：どのコードからもアクセスできる
    public static SoundManager instance;

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

    //BGMを鳴らすメソッド
    public void playBGM(BGM bgm) {
        audioSourceBGM.clip = bgmClips[(int)bgm];
        audioSourceBGM.Play();
    }

    public void stopBGM(BGM bgm)
    {
        audioSourceBGM.clip = bgmClips[(int)bgm];
        audioSourceBGM.Stop();
    }

    //SEを鳴らすメソッド
    public void playSE(SE se)
    {
        audioSourceSE.PlayOneShot(seClips[(int)se]);
    }
}
