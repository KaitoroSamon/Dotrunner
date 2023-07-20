using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange2 : MonoBehaviour
{
    [SerializeField]
    Image fade = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("DS4circle"))
        {
            StartCoroutine(Color_FadeOut());
        }

        if (Input.GetButtonDown("DS4circle2"))
        {
            StartCoroutine(Color_FadeOut());
        }
    }
    public void TitleChange()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            SceneManager.LoadScene("MapSelect", LoadSceneMode.Single);
            BGMManager.Instance.PlayBGM(BGMManager.BGM_TYPE.STAGESELECT,0.6f);//菊地加筆
        }
        else
        {
            SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
            BGMManager.Instance.PlayBGM(BGMManager.BGM_TYPE.TITLE, 0.6f);//菊地加筆
        }
    }

    IEnumerator Color_FadeOut()
    {
        // 画面をフェードインさせるコールチン
        // 前提：画面を覆うPanelにアタッチしている

        // 色を変えるゲームオブジェクトからImageコンポーネントを取得
        //fade = GetComponent<Image>();

        // フェード後の色を設定（黒）★変更可
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

        // フェードインにかかる時間（秒）★変更可
        const float fade_time = 1.5f;

        // ループ回数（0はエラー）★変更可
        const int loop_count = 50;

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ上げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }

        TitleChange();
    }
}
