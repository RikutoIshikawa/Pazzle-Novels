using System.Collections;   //�I�u�W�F�N�g�̃��X�g���������߂̖��O���
using UnityEngine;          //Unity�̃Q�[���G���W���̋@�\���g�����߂̖��O���

//�u���b�N�̐������s���X�N���v�g
public class BlockGenerator : MonoBehaviour
{
    //////////////////
    //�u���b�N�f�[�^//
    //////////////////
    
    [SerializeField] GameObject blockPrefab = default;  //�u���b�N�v���n�u�̐ݒ�

    [SerializeField] Sprite[] blockSprites = default;   //�u���b�N�̉摜��ݒ�

    //////////////////////////////////////////////
    //�w�肳�ꂽ�������u���b�N�𐶐����郁�\�b�h//
    //////////////////////////////////////////////
    
    public IEnumerator Spawns(int count) 
    {
       //Count�̐������J��Ԃ�
        for(int i = 0; i < count; i++)
        {
            //�u���b�N�v���n�u�̍��W���擾
            float pos_x = blockPrefab.transform.position.x;
            float pos_y = blockPrefab.transform.position.y;
            float pos_z = blockPrefab.transform.position.z;

            //x���W�������_���ɕύX
            Vector3 pos = new Vector3(pos_x + Random.Range(-1f, 1f), pos_y, pos_z);
            GameObject block = Instantiate(blockPrefab, pos, Quaternion.identity);

            //�u���b�N��ID�������_���ɐݒ�
            int blockID = Random.Range(0, blockSprites.Length);

            //�摜�ύX�̃R���|�[�l���g��p���ĉ摜��}��
            block.GetComponent<SpriteRenderer>().sprite = blockSprites[blockID];

            //�u���b�N�X�N���v�g��id���擾
            block.GetComponent<Block>().id = blockID; 

            //�����Ƀ��O����������
            yield return new WaitForSeconds(0.04f);
        }
    }
}