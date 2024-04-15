using UnityEngine; //Unity�̃Q�[���G���W���̋@�\���g�����߂̖��O���

//�u���b�N�̒�`�X�N���v�g
public class Block : MonoBehaviour
{
    ////////////////
    //�u���b�N���//
    ////////////////
    
    //�u���b�N�̎�ނ����ʂ��邽�߂�ID
    public int id;

    //�����A�j���[�V�����I�u�W�F�N�g
    [SerializeField] GameObject explosionPrehub = default;

    ////////////////////////////////////
    //�u���b�N�����������ɌĂ΂��֐�//
    ////////////////////////////////////
    
    public void Explosion() {
        //�u���b�N��j��
        Destroy(gameObject);

        //�A�j���[�V�������Đ�
        GameObject explosion = Instantiate(explosionPrehub, transform.position, transform.rotation);

        //0.2�b��ɔj��
        Destroy(explosion, 0.2f);
    }

}