using System.Collections.Generic;   //�I�u�W�F�N�g�̃��X�g���������߂̖��O���
using UnityEngine;                  //Unity�̃Q�[���G���W���̋@�\���g�����߂̖��O���
using System.IO;                    //�t�@�C���ɑ΂����{�I�ȃT�|�[�g��񋟂���^���g�p���邽�߂̖��O���

//�V�i���I����f�ޕ\���ؑցA�p�l���؂�ւ��A�����؂�ւ�������X�N���v�g
public class MaterialChange : MonoBehaviour
{
    //////////////////
    //�V�i���I�f�[�^//
    //////////////////

    //�V�i���I�ƂȂ�O���e�L�X�g
    [SerializeField] TextAsset _textFile;

    //�e�L�X�g���P�s���Ƃɕۑ����邽�߂̃��X�g
    List<string> _lineList = new List<string>();

    //////////////////////////////////////////////
    //�O���e�L�X�g�ɂ��\���؂�ւ�������p�l��//
    //////////////////////////////////////////////

    public GameObject panelObject;

    ////////////////////////////////////////////////////////
    //�O���e�L�X�g����V�i���I���P�s�Âǂݍ��ޏ������֐�//
    ////////////////////////////////////////////////////////
    
    void Awake()
    {
        //�e�L�X�g�t�@�C���̓ǂݍ���
        StringReader reader = new StringReader(_textFile.text);

        //�t�@�C���̖����ɒB����܂ŌJ��Ԃ�
        while (reader.Peek() != -1)
        {
            //�P�s�ǂݍ���
            string line = reader.ReadLine();

            //�擾�����s�����X�g�ɒǉ�
            _lineList.Add(line);
        }
    }
    
    ////////////////
    //�X�^�[�g�֐�//
    ////////////////
    
    void Start()
    {
        //�p�l�����\���ɂ��Ă���
        SetPanelVisibility(false);
    }

    ////////////////////
    //�A�b�v�f�[�g�֐�//
    ////////////////////
    
    void Update()
    {
        //�L�[���󂯎������p�l���̕\���ݒ��ύX����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePanelVisibility();
        }
    }

    ///////////////////////////////////
    // ���݂̍s�̕����擾���郁�\�b�h//
    ///////////////////////////////////
    
    public string GetCurrentLine()
    {
        //���݂̍s���Ō�̍s���ǂ�������
        if (GameManager.Instance.lineNumber >= _lineList.Count)
        {
            //�Ō�̍s���J��Ԃ����s����
            return _lineList[_lineList.Count-1];
        }

        //�����łȂ���Ό��݂̍s��ԋp����
        return _lineList[GameManager.Instance.lineNumber];
    }

    //////////////////////////////////////////////////
    //�n���ꂽ�����u���߁v���ǂ����𔻒肷�郁�\�b�h//
    //////////////////////////////////////////////////
    
    public bool Judgement_Order(string line)
    {
        //����"&"�Ŏn�܂��Ă���ꍇ
        if (line[0] == '&')
        {
            //���߂ł���ƕԂ�
            return true;
        }
            return false;
    }

    //////////////////////////
    //���߂����s���郁�\�b�h//
    //////////////////////////
    
    public void ExecuteLine(string line)
    {
        // ���ߕ����X�y�[�X�ŋ�؂��ĕ���
        string[] words = line.Split(' ');

        ////////////////////
        //���߂̔��ʂƎ��s//
        ////////////////////
        
        // ���߂̎�ނɂ���ĈقȂ鏈�����s��
        switch (words[0])
        {
            //////////////////////
            //BG_Operation�̎��s//
            //////////////////////
            
            //img�F�w�i�摜��z�u���閽��
            case "&img":
                GameManager.Instance.BG_script.PutImage(words[1], words[2]);
                break;
            //rmimg�F�w�i�摜���폜���閽��
            case "&rmimg":
                GameManager.Instance.BG_script.RemoveImage(words[1]);
                break;

            ////////////////////////
            //Item_Operation�̎��s//
            ////////////////////////
            
            //item�F�A�C�e����z�u���閽��
            case "&item":
                GameManager.Instance.I_script.PutImage(words[1], words[2]);
                break;
            //rmitem�F�A�C�e�����폜���閽��
            case "&rmitem":
                GameManager.Instance.I_script.RemoveImage(words[1]);
                break;

            /////////////////////////////
            //Character_Operation�̎��s//
            /////////////////////////////
            
            //character�F�L�����N�^�[��z�u���閽��
            case "&character":
                GameManager.Instance.C_script.PutImage(words[1], words[2]);
                break;
            //rmcharacter2�F�L�����N�^�[���폜���閽��
            case "&rmcharacter":
                GameManager.Instance.C_script.RemoveImage(words[1]);
                break;

            ////////////////////
            //�p�l���̕\���ؑ�//
            ////////////////////
            
            //togglePanel�F�ݒ肵���p�l���̕\��/��\����؂�ւ���
            case "&togglePanel":
                TogglePanelVisibility();
            break;
            
            //////////////////////
            //SoundManager�̎��s//
            //////////////////////
            
            //sound�FBGM�𗬂�����
            case "&playsound":
                //�L�[���[�h�ɂ�藬��BGM��ς���
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

            //sound�FBGM���X�g�b�v���閽��
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

            //se�FSE�𗬂�����
            case "&se":
                //�L�[���[�h�ɂ�藬��SE��ς���
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
    //�p�l���̌��݂̏�Ԃ��擾���A�t�̏�ԂɃZ�b�g���郁�\�b�h//
    ////////////////////////////////////////////////////////////
    
    private void TogglePanelVisibility()
    {
        //���݂̏�Ԃ��擾
        bool currentVisibility = panelObject.activeSelf;

        //�t�̏�Ԃ֕ύX
        SetPanelVisibility(!currentVisibility);
    }
    
    //////////////////////////////////////////
    //�w�肵����ԂɃp�l����ݒ肷�郁�\�b�h//
    //////////////////////////////////////////
    
    private void SetPanelVisibility(bool isVisible)
    {
        panelObject.SetActive(isVisible);
    }
}