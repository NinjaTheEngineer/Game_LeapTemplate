using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarScript : MonoBehaviour
{
    [SerializeField] RectTransform FxHolder;
    [SerializeField] Image CircleImg;
    [SerializeField] Text txtProgress;

    [SerializeField] [Range(0, 1)] float progress = 1f;
    private bool startProgressBar = false;
    private DeadScreenScript deadScript;
    public float timer = 5.0f;
    private void Start()
    {
        deadScript = GetComponentInParent<DeadScreenScript>();
    }
    void Update()
    {
        if (startProgressBar)
        {
            CircleImg.fillAmount = progress;
            txtProgress.text = Mathf.Round(timer).ToString();

            if (timer > 0)
            {
                timer -= Time.unscaledDeltaTime;
            }
            if(timer < 2.35f)
            {
                deadScript.NoThanksActive();
            }
            progress = timer / 5;
            FxHolder.rotation = Quaternion.Euler(new Vector3(0f, 0f, -progress * 360));
            if(timer <= 0)
            {
                Time.timeScale = 1;
                deadScript.GameOver();
            }
        }
    }

    public void SetStartProgressBar(bool active)
    {
        startProgressBar = active;
        timer = 5.0f;
    }
}
