using System.Collections.Generic;   //�I�u�W�F�N�g�̃��X�g���������߂̖��O���
using DG.Tweening;
using UnityEngine;                  //Unity�̃Q�[���G���W���̋@�\���g�����߂̖��O���
using UnityEngine.SceneManagement;  //Unity�G���W���̃V�[���Ǘ��v���O�����̖��O���
using UnityEngine.UI;               //Unity��UI�@�\�̖��O���

//�p�Y���Q�[���̊Ǘ����s���X�N���v�g
public class PazzleGameManager : MonoBehaviour
{
    ////////////////////////////////////
    //�u���b�N�̐����⑀��ɕK�v�ȏ��//
    ////////////////////////////////////

    //�u���b�N�𐶐�����X�N���v�g
    [SerializeField] BlockGenerator blockGenerator = default;

    //�N���b�N�Œ����𔻒肷��ϐ�
    bool isClicking;

    //�폜����u���b�N�̊i�[���s�����X�g
    [SerializeField] List<Block> removeBlocks = new List<Block>();

    //���݃N���b�N�����u���b�N
    Block currentClickingBlock;

    //////////////////////////////
    //�v���C���[�ƓG�̃p�����[�^//
    //////////////////////////////
    
    int enemyHP; //�G��HP
    [SerializeField] Text enemyHPText = default;        //�X�N���[���\���p
    [SerializeField] Slider enemyHPGauge = default;     //HP�Q�[�W

    int playerHP; //�v���C���[��HP
    [SerializeField] Text playerHPText = default;       //�X�N���[���\���p
    [SerializeField] Slider playerHPGauge = default;    //HP�Q�[�W

    //////////////////////////////////////////
    // Clear���ɕ\���������p�l���I�u�W�F�N�g//
    //////////////////////////////////////////
    
    public GameObject panelObject;

    ////////////////
    //�X�^�[�g�֐�//
    ////////////////
    
    void Start()
    {
        //�N���A�p�l���͔�\��
        panelObject.SetActive(false);

        //�o�g��BGM�ݒ�
        SoundManager.instance.playBGM(SoundManager.BGM.BattleBGM);

        //////////////////////
        //�G�p�����[�^�̐ݒ�//
        //////////////////////

        //�GHP�̏�����
        enemyHP = ParamsSO.Entity.initEnemyHP;
        //�e�L�X�g�̏�����
        enemyHPText.text = enemyHP.ToString() + "/" + enemyHP.ToString();
        //�ő�HP�̐ݒ�
        enemyHPGauge.maxValue = ParamsSO.Entity.initEnemyHPGauge;
        //�Q�[�W�̏�����
        enemyHPGauge.value = ParamsSO.Entity.initEnemyHPGauge;

        //////////////////////////////
        //�v���C���[�p�����[�^�̐ݒ�//
        //////////////////////////////

        //�v���C���[HP�̏�����
        playerHP = ParamsSO.Entity.initPlayerHP;
        //�e�L�X�g�̏�����
        playerHPText.text = playerHP.ToString() + "/" + playerHP.ToString();
        //�ő�HP�̐ݒ�
        playerHPGauge.maxValue = ParamsSO.Entity.initPlayerHPGauge;
        //�Q�[�W�̏�����
        playerHPGauge.value = ParamsSO.Entity.initPlayerHPGauge;

        ////////////////
        //�Ֆʂ̏�����//
        ////////////////
        
        StartCoroutine(blockGenerator.Spawns(ParamsSO.Entity.initBlockCount));
    }

    ////////////////////
    //�A�b�v�f�[�g�֐�//
    ////////////////////
    
    void Update()
    {
        //�E�N���b�N����������
        if (Input.GetMouseButtonDown(0))
        {
            //�N���b�N�J�n�֐�
            Click_Start();
        }
        //�E�N���b�N�𗣂�����
        else if (Input.GetMouseButtonUp(0))
        {
            //�N���b�N�I���֐�
            Click_End();
        }
        //�E�N���b�N�����Ă��鎞
        else if (isClicking) {
            //�N���b�N���֐�
            Click_Now();
        }
    }

    //////////////////////////////////
    //�N���b�N������Ă΂�郁�\�b�h//
    //////////////////////////////////

    void Click_Start() {

        //�}�E�X�̃N���b�N�ʒu���擾
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //�QD�ɐݒ�
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        //////////////////////////
        //�u���b�N��I�����鏈��//
        //////////////////////////
        
        //�u���b�N�ɓ��������ꍇ
        if (hit && hit.collider.GetComponent<Block>()) {
            //�N���b�N�����u���b�N���폜���X�g�ɉ�����
            Block block = hit.collider.GetComponent<Block>();
            AddRemoveBlock(block);
            isClicking = true;
        }
    }

    //////////////////////////////////////////
    //�N���b�N���Ă���Œ��ɌĂ΂�郁�\�b�h//
    //////////////////////////////////////////
    
    void Click_Now() {

        //�}�E�X�̃N���b�N�ʒu���擾
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //�QD�ɐݒ�
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        ////////////////////////
        //�u���b�N���q���鏈��//
        ////////////////////////
        
        //�u���b�N�ɓ��������ꍇ
        if (hit && hit.collider.GetComponent<Block>())
        {
            Block block = hit.collider.GetComponent<Block>();
            
            //�P�O�ɂɃN���b�N�����u���b�N�Ɠ�����ނ�����
            if (block.id == currentClickingBlock.id) 
            { 
                //�P�O�ɃN���b�N�����u���b�N����̋������߂����
                float distance = Vector2.Distance(block.transform.position, currentClickingBlock.transform.position);
                if (distance < ParamsSO.Entity.BlockDistance) {
                    //�폜���X�g�ɒǉ�����
                    AddRemoveBlock(block);
                }
            }
        }
    }

    //////////////////////////////////////
    //�N���b�N�𗣂�����Ă΂�郁�\�b�h//
    //////////////////////////////////////
    
    void Click_End() {

        //////////////////////////
        //�u���b�N���폜���鏈��//
        //////////////////////////
        
        //�폜���X�g�ɓ����Ă��鐔���擾
        int removeCount = removeBlocks.Count;

        //�����u���b�N�R�ȏ�q�����Ă��邩����
        if (removeCount >= 3) {

            ////////////////////////////////////////
            //�������u���b�N�̎�ނɂ�鏈���̕ύX//
            ////////////////////////////////////////

            //���u���b�N�̏ꍇ�F���������~�R�O�����G��HP�����炷
            if (removeBlocks[0].id == 0)
            {
                //�G�̃p�����[�^�̕ύX
                enemyHP -= ParamsSO.Entity.SwordPoint * removeCount;
                enemyHPText.text = enemyHP.ToString() + "/" + enemyHPGauge.maxValue;
                
                //�U������炷
                SoundManager.instance.playSE(SoundManager.SE.Sword);

                //�G��HP�Q�[�W�����炷
                enemyHPGauge.value = enemyHP;

                //�G��HP���O�����ɂȂ�����A�N���A���\�b�h�����s
                if (enemyHP < 0) {
                    Clear();
                }
            }
            //���ʃu���b�N�̏ꍇ�F���������~10�����G��HP�����炷
            else if (removeBlocks[0].id == 1)
            {
                //�G�̃p�����[�^�̕ύX
                enemyHP -= ParamsSO.Entity.LadlePoint * removeCount;
                enemyHPText.text = enemyHP.ToString() + "/" + enemyHPGauge.maxValue;

                //�U������炷
                SoundManager.instance.playSE(SoundManager.SE.Ladle);

                //�G��HP�Q�[�W�����炷
                enemyHPGauge.value = enemyHP;

                //�G��HP���O�����ɂȂ�����A�N���A���\�b�h�����s
                if (enemyHP < 0)
                {
                    Clear();
                }
            }

            //�n�[�g�u���b�N�̏ꍇ�F���������~2�O�����v���C���[HP���񕜂���
            else if (removeBlocks[0].id == 2)
            {
                //�v���C���[�p�����[�^�̕ύX
                playerHP += ParamsSO.Entity.HeartPoint * removeCount;
                playerHPText.text = playerHP.ToString() + "/" + playerHPGauge.maxValue;

                //�񕜉���炷
                SoundManager.instance.playSE(SoundManager.SE.Heart);

                //�v���C���[HP�̃Q�[�W�𑝂₷
                playerHPGauge.value = playerHP;
            }

            //�h�N���u���b�N�̏ꍇ�F���������~�P�T�����v���C���[HP������
            else if (removeBlocks[0].id == 3)
            {
                //�v���C���[�p�����[�^�̕ύX
                playerHP -= ParamsSO.Entity.SkullPoint * removeCount; ;
                playerHPText.text = playerHP.ToString() + "/" + playerHPGauge.maxValue;

                //�_���[�W����炷
                SoundManager.instance.playSE(SoundManager.SE.Skull);

                //�v���C���[��HP�Q�[�W�����炷
                playerHPGauge.value = playerHP;

                //�v���C���[��HP���O�����ɂȂ�����A�Q�[���I�[�o�[�����s
                if (playerHP < 0)
                {
                    GameOver();
                }
            }
            else 
            {
                Debug.Log("�G���[");
            }

            //////////////////////
            //�u���b�N�̍폜����//
            //////////////////////
            
            //���X�g�ɒǉ����ꂽ�u���b�N���폜����
            for (int i = 0; i < removeCount; i++)
            {
                //�j��A�j���[�V�������Đ�
                removeBlocks[i].Explosion();
            }

            //�������u���b�N�̐�����BlockGenerator�Ő�������
            StartCoroutine(blockGenerator.Spawns(removeCount));
        }
        
        ////////////////////////////////////////////
        //�I�񂾃u���b�N�̑傫���ƐF�����ɖ߂�����//
        ////////////////////////////////////////////
        
        for (int i = 0; i < removeCount; i++) {
            removeBlocks[i].transform.localScale = Vector3.one * 5.0f;
            removeBlocks[i].GetComponent<SpriteRenderer>().color = Color.white;
        }

        //////////////////////////////////////////////////////
        //�폜���X�g����ɂ��āA�N���b�N��Ԃ̉��������鏈��//
        //////////////////////////////////////////////////////
        
        removeBlocks.Clear();
        isClicking = false;
    }

    ////////////////////////////////////////////////////
    //�w�肳�ꂽ�u���b�N���폜���X�g�ɒǉ����郁�\�b�h//
    ////////////////////////////////////////////////////
    
    void AddRemoveBlock(Block block) 
    {
        //���݃N���b�N���Ă���u���b�N���X�V����
        currentClickingBlock = block;
        
        //
        if (removeBlocks.Contains(block) == false) {
            //�I�񂾃u���b�N��傫������
            block.transform.localScale = Vector3.one * 6.0f;

            //�I�񂾃u���b�N�̐F��ς���
            block.GetComponent<SpriteRenderer>().color = Color.cyan;

            //�폜���X�g�ɒǉ�
            removeBlocks.Add(block);

            //�u���b�N�ɐG�ꂽ���ɁABlockSE��炷
            SoundManager.instance.playSE(SoundManager.SE.Block);
        }
    }
    
    //////////////////
    //�N���A���\�b�h//
    //////////////////
    
    private void Clear()
    {
        //�Ƃǂ�SE�𗬂�
        SoundManager.instance.playSE(SoundManager.SE.LastAttack);

        //�o�g��BGM���X�g�b�v
        SoundManager.instance.stopBGM(SoundManager.BGM.BattleBGM);

        //�N���A��ʕ\��
        panelObject.SetActive(true);

        //�N���ASE�𗬂�
        SoundManager.instance.playSE(SoundManager.SE.Victory);
    }

    //////////////////////////
    //�Q�[���I�[�o�[���\�b�h//
    //////////////////////////
    
    private void GameOver()
    {
        //�P�b��Ɏ��s
        DOVirtual.DelayedCall(1.0f, () => {
            //GameOver2�V�[���ֈړ�
            SceneManager.LoadScene("Game_Over2", LoadSceneMode.Single);
        });
    }
}