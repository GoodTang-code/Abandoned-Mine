using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    //public GameObject targetObj;
    //private hpUI target;

    public float hp;
    public float maxHp;
    public float smooth = 0.5f;
    public Image fill;
    public Image shadow;
    public Gradient color;

    void Start()
    {
        hp = 100;
        maxHp = 100;
        fill.fillAmount = 1f;
        shadow.fillAmount = 1f;
        fill.color = color.Evaluate(1f);
    }

    // Update is called once per frame
    void Update()
    {
        //current = player.hp;
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float targetAmount = hp / maxHp;

        if (fill.fillAmount != targetAmount)
            fill.fillAmount = targetAmount;

        if (shadow.fillAmount != targetAmount)
            shadow.fillAmount = Mathf.Lerp(shadow.fillAmount, targetAmount, Time.deltaTime * smooth);

        if (fill.color != color.Evaluate(targetAmount))
            fill.color = color.Evaluate(targetAmount);
    }
}
