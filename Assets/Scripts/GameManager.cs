using UnityEngine; //Unity�̃Q�[���G���W���̋@�\���g�����߂̖��O���

//�m�x���Q�[���̊Ǘ����s���X�N���v�g
public class GameManager : MonoBehaviour
{
    //�V���O���g���F�ǂ̃R�[�h������A�N�Z�X�ł���
    public static GameManager Instance { get; private set; }
   
    //////////////////////
    //�e�X�N���v�g�̎Q��//
    //////////////////////
    
    public ScenarioManager S_script;        //�V�i���I���Ǘ�����X�N���v�g
    public MaterialChange M_script;         //�V�i���I����f�ޕ\���ؑւ≹���؂�ւ�������X�N���v�g
    public BG_Operation BG_script;          //�w�i�摜��M�������[�Ǘ�����X�N���v�g
    public Item_Operation I_script;         //�A�C�e�����Ǘ�����X�N���v�g
    public Character_Operation C_script;    //�L�����N�^�[���Ǘ�����X�N���v�g

    //���ݓǂ�ł���s���F�N���b�N�̂��тɂP�s������
    [System.NonSerialized] public int lineNumber;

    //////////////
    //�������֐�//
    //////////////
    
    void Awake()
    {
        Instance = this;    //�V���O���g���̐ݒ�

        lineNumber = 0;     //�s�ԍ���������
    }
}