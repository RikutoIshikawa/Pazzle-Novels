using UnityEngine;  //Unity�̃Q�[���G���W���̋@�\���g�����߂̖��O���
using TMPro;        //�e�L�X�g�\���ɓ��������@�\���g�����߂̖��O���

//�V�i���I���Ǘ�����X�N���v�g
public class ScenarioManager : MonoBehaviour 
{
    //////////////////////
    //�O���e�L�X�g�̏��//
    //////////////////////
    
    [SerializeField] TextMeshProUGUI _mainTextObject;   //�O���e�L�X�g�I�u�W�F�N�g
    [SerializeField]int _lineCount;                     //�O���e�L�X�g�̍s��

    //////////////////////////
    //�V�i���I�\���ň������//
    //////////////////////////
    
    int _length;        //���ݕ\�����̕����̒���
    int _wholeLength;   //���ݕ\�����̍s�̕����̑���

    float _time;        //������\������^�C�~���O���Ǘ�
    float _feedTime;    //�P������\������Ԋu���Ǘ�


    ////////////////
    //�X�^�[�g�֐�//
    ////////////////
    
    void Start()
    {
        //////////////////
        //�e���Ԃ̏�����//
        //////////////////
        
        _time = 0f;
        _feedTime = 0.05f;

        //////////////////////
        //�O���e�L�X�g�̏���//
        //////////////////////
        
        // MaterialChange�X�N���v�g��p���āA���݂̍s�̃e�L�X�g��\���A�܂��͖��߂����s
        string line = GameManager.Instance.M_script.GetCurrentLine();

        //���߂��ǂ����𔻒�
        if (GameManager.Instance.M_script.Judgement_Order(line))
        {
            //���߂̏ꍇ�́A���߂����s����
            GameManager.Instance.M_script.ExecuteLine(line);
            //���̍s��
            NextLine();
        }
        //�e�L�X�g�Ȃ�Ε\��
        DisplayText();
    }

    ////////////////////
    //�A�b�v�f�[�g�֐�//
    ////////////////////
    
    void Update()
    {
        /////////////////////////////////
        // ���͂��P�������\�����鏈��//
        /////////////////////////////////
        
        _time += Time.deltaTime; //�o�ߎ��Ԃ��X�V
        
        //�����i�P�����\�����Ԃ��o�߁j�𖞂����ƁA1�����\�����鏈�������s
        if (_time >= _feedTime)
        {
            _time -= _feedTime;

            //�܂��������S�ĕ\������Ă��Ȃ����
            if (!Judgement_NextLine())
            {
                //�\�����̕������J�E���g���P�������₷
                _length++;
                _mainTextObject.maxVisibleCharacters = _length;
            }
        }

        ////////////////////////////
        //�N���b�N���ꂽ�Ƃ��̏���//
        ////////////////////////////
        
        if (Input.GetMouseButtonUp(0))
        {
            //�O���e�L�X�g���Ō�̍s�܂œǂݍ���ł��邩����
            if (GameManager.Instance.lineNumber >= _lineCount)
            {
                // Update���\�b�h�̎��s���~����
                enabled = false;
            }
            else 
            {
                //�������S�ĕ\������Ă��邩����
                if (Judgement_NextLine())
                {
                    //���̍s�Ɉړ�
                    NextLine();

                    //����\������
                    DisplayText();
                }

                //�܂��������\������Ă��Ȃ����
                else
                {
                    //�S�Ă̕�����\������
                    _length = _wholeLength;
                }
            }
        }
    }
    
    //////////////////////////////////////////////
    //�������Ō�܂ŕ\���������𔻒肷�郁�\�b�h//
    //////////////////////////////////////////////
    
    public bool Judgement_NextLine()
    {
        ////////////
        //���̎擾//
        ////////////
        
        // GameManager�̃C���X�^���X���擾
        GameManager gameManager = GameManager.Instance;

        //���݂̍s�̕����擾
        string line = gameManager.M_script.GetCurrentLine(); 

        ////////////////////////////////
        //���͂�ǂݐ؂������ǂ�������//
        ////////////////////////////////
        
        //���݂̍s�̕������ƌ��݂̕\�����̕��������r����
        _wholeLength = line.Length;

        //���茋�ʂ�Ԃ�
        return (_length > line.Length);
    }

    ////////////////////////////
    //���̍s�����s���郁�\�b�h//
    ////////////////////////////
    
    public void NextLine()
    {
        //////////////////////////////////////
        //�V�i���I�\���Ɉ����f�[�^�̃��Z�b�g//
        //////////////////////////////////////
        
        _length = 0;
        _time = 0f;
        _mainTextObject.maxVisibleCharacters = 0;

        //�s�J�E���g�𑝂₷
        GameManager.Instance.lineNumber++;

        ////////////////////////////////////////
        //�O���e�L�X�g���Ō�܂Ŏ��s����������//
        ////////////////////////////////////////

        // �����s�����Ō�̍s�܂ł��ǂ蒅���Ă����珈�����I������
        if (GameManager.Instance.lineNumber >= _lineCount)
        {
            return;
        }

        //�s�̕��͂��擾
        string line = GameManager.Instance.M_script.GetCurrentLine();

        //���߂Ȃ疽�߂����s
        if (GameManager.Instance.M_script.Judgement_Order(line)) 
        {
            GameManager.Instance.M_script.ExecuteLine(line);
            NextLine();
        }
    }
    
    //////////////////////////////
    //�e�L�X�g��\�����郁�\�b�h//
    //////////////////////////////
    
    public void DisplayText()
    {
        string line = GameManager.Instance.M_script.GetCurrentLine();
        _mainTextObject.text = line;
    }
}