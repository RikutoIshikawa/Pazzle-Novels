using UnityEngine.SceneManagement;  //Unity�G���W���̃V�[���Ǘ��v���O�����̖��O���
using UnityEngine;                  //Unity�̃Q�[���G���W���̋@�\���g�����߂̖��O���
using DG.Tweening;                  //DOTween��DOVirtual.DelayedCall()�𗘗p���邽�߂̖��O���

//�V�[���ύX�֐����Ǘ�����X�N���v�g
public class ChangeScene : MonoBehaviour
{
    ///////////////////////////////////////////////////
    //Start�{�^���������ꂽ�ۂ�Main_Scene�֐؂�ւ���//
    ///////////////////////////////////////////////////
    
    public void Button_changesStart()
    {
        //�X�^�[�g�{�^��SE�𗬂�
        SoundManager.instance.playSE(SoundManager.SE.StartButton);

        //�R�b��Ɏ��s
        DOVirtual.DelayedCall(3.0f, () => {
            //Main_Scene�ֈړ�
            SceneManager.LoadScene("Main_Scene", LoadSceneMode.Single);
        });
    }

    ///////////////////////////////////////////////////
    //Yes�{�^���������ꂽ�ۂ�Middle_Scene�֐؂�ւ���//
    ///////////////////////////////////////////////////
    
    public void Button_changeYes()
    {
        //�{�^��SE�𗬂�
        SoundManager.instance.playSE(SoundManager.SE.Button);

        //�Q�b��Ɏ��s
        DOVirtual.DelayedCall(2.0f, () => {
            //Middle_Scene�ֈړ�
            SceneManager.LoadScene("Middle_Scene", LoadSceneMode.Single);
        });
    }

    /////////////////////////////////////////////////
    //No�{�^���������ꂽ�ۂ�Game_Over�P�֐؂�ւ���//
    /////////////////////////////////////////////////
    
    public void Button_changeNo()
    {
        //�{�^��SE�𗬂�
        SoundManager.instance.playSE(SoundManager.SE.Button);

        //�Q�b��Ɏ��s
        DOVirtual.DelayedCall(2.0f, () => {
            //Game_Over1�ֈړ�
            SceneManager.LoadScene("Game_Over1", LoadSceneMode.Single);
        });
    }

    ///////////////////////////////////////////////////
    //Retry�{�^���������ꂽ�ۂ�Main_Scene�֐؂�ւ���//
    ///////////////////////////////////////////////////
    
    public void Button_changeRetry1()
    {
        //�{�^��SE�𗬂�
        SoundManager.instance.playSE(SoundManager.SE.Button);

        //�Q�b��Ɏ��s
        DOVirtual.DelayedCall(2.0f, () => {
            //Main_Scene�ֈړ�
            SceneManager.LoadScene("Main_Scene", LoadSceneMode.Single);
        });
    }

    /////////////////////////////////////////////////////
    //Ready�{�^���������ꂽ�ۂ�Pazzle_Scene�֐؂�ւ���//
    /////////////////////////////////////////////////////
    
    public void Button_changeReady()
    {
        //�{�^��SE�𗬂�
        SoundManager.instance.playSE(SoundManager.SE.Button);

        //�Q�b��Ɏ��s
        DOVirtual.DelayedCall(2.0f, () => {
            //Pazzle_Scene�ֈړ�
            SceneManager.LoadScene("Pazzle_Scene", LoadSceneMode.Single);
        });
    }

    /////////////////////////////////////////////////////
    //Retry�{�^���������ꂽ�ۂ�Pazzle_Scene�֐؂�ւ���//
    /////////////////////////////////////////////////////

    public void Button_changeRetry2()
    {
        //�{�^��SE�𗬂�
        SoundManager.instance.playSE(SoundManager.SE.Button);

        //�Q�b��Ɏ��s
        DOVirtual.DelayedCall(2.0f, () => {
            //Pazzle_Scene�ֈړ�
            SceneManager.LoadScene("Pazzle_Scene", LoadSceneMode.Single);
        });
    }

    /////////////////////////////////////////////////////////////
    //�^�C�g���֖߂�{�^���������ꂽ�ۂ�Title_Scene�֐؂�ւ���//
    /////////////////////////////////////////////////////////////
    
    public void Button_changeTitle()
    {
        //�{�^��SE�𗬂�
        SoundManager.instance.playSE(SoundManager.SE.Button);

        //�Q�b��Ɏ��s
        DOVirtual.DelayedCall(2.0f, () => {

            //BGM�𗬂�
            SoundManager.instance.playBGM(SoundManager.BGM.StartBGM);

            //Title_Scene�ֈړ�
            SceneManager.LoadScene("Title_Scene", LoadSceneMode.Single);
        });
    }

    ////////////////////////////////////////////////////
    //�i��y�{�^���������ꂽ�ۂ�Final_Scene�֐؂�ւ���//
    ////////////////////////////////////////////////////
    
    public void Button_changeNext()
    {
        //�{�^��SE�𗬂�
        SoundManager.instance.playSE(SoundManager.SE.Button);

        //�Q�b��Ɏ��s
        DOVirtual.DelayedCall(2.0f, () => {
            //Final_Scene�ֈړ�
            SceneManager.LoadScene("Final_Scene", LoadSceneMode.Single);
        });
    }

    /////////////////////////////////////////////////////////////
    //�^�C�g���֖߂�{�^���������ꂽ�ۂ�Title_Scene�֐؂�ւ���//
    /////////////////////////////////////////////////////////////
    
    public void Button_changeTitle2()
    {
        //�{�^��SE�𗬂�
        SoundManager.instance.playSE(SoundManager.SE.Button);

        //�Q�b��Ɏ��s
        DOVirtual.DelayedCall(2.0f, () => {
            //BGM������
            SoundManager.instance.stopBGM(SoundManager.BGM.FinalBGM);

            //BGM�𗬂�
            SoundManager.instance.playBGM(SoundManager.BGM.StartBGM);

            //Title_Scene�ֈړ�
            SceneManager.LoadScene("Title_Scene", LoadSceneMode.Single);
        });
    }
}