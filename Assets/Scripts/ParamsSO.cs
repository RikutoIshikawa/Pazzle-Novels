using UnityEngine; //Unity�̃Q�[���G���W���̋@�\���g�����߂̖��O���

//�ȒP�Ƀp�����[�^�𑀍�ł���N���X
[CreateAssetMenu]
public class ParamsSO : ScriptableObject
{
    [Header("�G�̏���HP")]
    public int initEnemyHP;

    [Header("�G�̏���HP�Q�[�W�p")]
    public float initEnemyHPGauge;

    [Header("�v���C���[�̏���HP")]
    public int initPlayerHP;

    [Header("�v���C���[�̏���HP�Q�[�W�p")]
    public float initPlayerHPGauge;

    [Header("�����̃u���b�N��")]
    public int initBlockCount;

    [Header("�u���b�N�̔��苗��")]
    public float BlockDistance;

    [Header("Sword�u���b�N�P�̍U����")]
    public int SwordPoint;

    [Header("Ladle�u���b�N�P�̍U����")]
    public int LadlePoint;

    [Header("Hert�u���b�N�P�̉񕜗�")]
    public int HeartPoint;

    [Header("Skull�u���b�N�P�̃_���[�W��")]
    public int SkullPoint;

    //MyScriptableObject���ۑ����Ă���ꏊ�̃p�X
    public const string PATH = "ParamsSO";

    //MyScriptableObject�̎���
    private static ParamsSO _entity;
    public static ParamsSO Entity
    {
        get
        {
            //���A�N�Z�X���Ƀ��[�h����
            if (_entity == null)
            {
                _entity = Resources.Load<ParamsSO>(PATH);

                //���[�h�o���Ȃ������ꍇ�̓G���[���O��\��
                if (_entity == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }

            return _entity;
        }
    }
}
