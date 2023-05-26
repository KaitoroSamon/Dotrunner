using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yuta_GameManger : MonoBehaviour
{
    [SerializeField] GameObject Portopn;
    [SerializeField] GameObject Bucket;
    [SerializeField] GameObject Bomb;

    // Start is called before the first frame update
    void Start()
    {
        //�c9�}�X�@��16�}�X��z��
        int[] Vertical = { 4, 3, 2, 1, 0, -1, -2, -3, -4 };
        float[] Beside = { -7.5f, -6.5f, -5.5f, -4.5f, -3.5f, -2.5f, -1.5f, -0.5f, 0.5f, 1.5f, 2.5f, 3.5f, 4.5f, 5.5f, 6.5f, 7.5f };

        //15��J��Ԃ�
        for (int i = 1; i <= 15; i++)
        {
            int beside = Random.Range(0, 16);
            int vertical = Random.Range(0, 9);
            int x = Vertical[vertical];
            float y = Beside[beside];
            Vector2 pos = new Vector2(x, y);

            //P1�̃S�[���|�W�V�����ɐ���������
            if (x == 0 && y == 7.5f)
            {
                //�����������Ȃ��ŁA�������
                i--;
            }
            //P2�̃S�[���|�W�V�����ɐ���������
            if (x == 0 && y == -7.5f)
            {
                //�����������Ȃ��ŁA�������
                i--;
            }
            //�S�[���|�W�V�����O�ɐ��������ꍇ
            else
            {
                if (i <= 5)
                {
                    //���e����
                    Debug.Log("1");
                }
                else if (i > 5 && i <= 10)
                {
                    //�|�[�V��������
                    Debug.Log("2");
                }
                else if (i > 10 && i <= 15)
                {
                    //�o�P�c�𐶐�
                    Debug.Log("3");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
