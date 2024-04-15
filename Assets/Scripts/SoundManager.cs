using UnityEngine; //Unity�̃Q�[���G���W���̋@�\���g�����߂̖��O���

//BGM��SE�̊Ǘ�������X�N���v�g
public class SoundManager : MonoBehaviour
{
    //BGM�̍\��
    [SerializeField] AudioSource audioSourceBGM = default; //�X�s�[�J�[
    [SerializeField] AudioClip[] bgmClips = default; //CD

    //SE�̍\��
    [SerializeField] AudioSource audioSourceSE = default; //�X�s�[�J�[
    [SerializeField] AudioClip[] seClips = default; //CD

    //BGM�̑���F�񋓌^
    public enum BGM 
    {
        StartBGM,
        LifeBGM, 
        UnrestBGM, 
        BattleBGM, 
        FinalBGM
    }

    //SE�̑���F�񋓌^
    public enum SE
    {
        StartButton,    //�X�^�[�g�{�^������������
        Button,         //�{�^������������
        Block,          //�u���b�N�ɐG�ꂽ��
        Sword,          //���̍U����
        Ladle,          //���ʂ̍U����
        Heart,          //�n�[�g�̉񕜉�
        Skull,          //�ł̃_���[�W��
        LastAttack,     //�Ō�̍U����
        Victory,        //������
        Creature,       //�N���[�`���[�̖���
    }

    //�V���O���g���F�ǂ̃R�[�h������A�N�Z�X�ł���
    public static SoundManager instance;

    //////////////
    //�������֐�//
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
    //�w�肵��BGM��炷���\�b�h//
    ///////////////////////////////
    public void playBGM(BGM bgm) {
        audioSourceBGM.clip = bgmClips[(int)bgm];
        audioSourceBGM.Play();
    }

    ///////////////////////////////
    //�w�肵��BGM���~�߂郁�\�b�h//
    ///////////////////////////////
    public void stopBGM(BGM bgm)
    {
        audioSourceBGM.clip = bgmClips[(int)bgm];
        audioSourceBGM.Stop();
    }

    //////////////////////////////
    //�w�肵��SE��炷���\�b�h//
    //////////////////////////////
    public void playSE(SE se)
    {
        audioSourceSE.PlayOneShot(seClips[(int)se]);
    }
}
