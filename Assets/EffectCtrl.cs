using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;
using System;

public class EffectCtrl : MonoBehaviour
{
    public Button ToTreeBtn;
    public Button ToWaterBtn;
    public Button ResetBtn;

    /// <summary>
    /// 树冠特效
    /// </summary>
    public VisualEffect FlowerVfx;
    /// <summary>
    /// 树干特效
    /// </summary>
    public VisualEffect BranchVfx;
    /// <summary>
    /// 水特效
    /// </summary>
    public VisualEffect WaterVfx;
    public float FlowerThrottle = 1;
    public float BranchThrottle = 1;
    public float WaterThrottle = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        //开始显示粒子
        FlowerVfx.SetFloat("Throttle", FlowerThrottle);
        BranchVfx.SetFloat("Throttle", BranchThrottle);

        ToTreeBtn.onClick.AddListener(ToTreeBtnClick);
        ToWaterBtn.onClick.AddListener(ToWaterBtnClick);
        ResetBtn.onClick.AddListener(ResetBtnBtn);
    }

    private void ToTreeBtnClick()
    {
        startLerp = true;
        //waterLerp = true;
    }

    private void ToWaterBtnClick()
    {
        waterLerp = true;
    }

    private void ResetBtnBtn()
    {
        ResetData();
    }

    bool startLerp = false;
    float curLerp = 0;

    bool waterLerp = false;
    float curWaterLerp = 0;

    private void ResetData()
    {
        startLerp = false;
        curLerp = 0;
        waterLerp = false;
        curWaterLerp = 0;
        FlowerVfx.SetFloat("Throttle", 0);
        BranchVfx.SetFloat("Throttle", 0);
        WaterVfx.SetFloat("Throttle", 0);

        FlowerVfx.SetFloat("LerpPercent", 0);
        BranchVfx.SetFloat("LerpPercent", 0);
        WaterVfx.SetFloat("LerpPercent", 0);
    }

    //渐变成树的速度
    public float ChangeSpeed = 1;
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    //开始显示粒子
        //    FlowerVfx.SetFloat("Throttle", FlowerThrottle);
        //    BranchVfx.SetFloat("Throttle", BranchThrottle);
        //}

        if (Input.GetKeyUp(KeyCode.B))
        {
            //渐变成树
            startLerp = true;
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            //渐变成水
            waterLerp = true;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            ResetData();
        }

        //渐变成树
        if (startLerp)
        {
            if(curLerp<1)
                curLerp += Time.deltaTime* ChangeSpeed;
            else
            {
                //渐变成树完成
                curLerp = 1;
                startLerp = false;
                WaterVfx.SetFloat("Throttle", WaterThrottle);
                WaterVfx.SetFloat("LerpPercent", 0);
            }
            //代表树和粒子平面之间的状态，1代表为最终状态
            FlowerVfx.SetFloat("LerpPercent", curLerp);
            BranchVfx.SetFloat("LerpPercent", curLerp);
        }

        //渐变成水
        if (waterLerp)
        {
            if (curWaterLerp < 1)
            {
                curWaterLerp += Time.deltaTime * ChangeSpeed * 0.5f;
                Vector3 scale = WaterVfx.transform.localScale;
                if (scale.x<1)
                {
                    scale.x+= Time.deltaTime * ChangeSpeed * 0.75f;
                }
                if (scale.z < 1)
                {
                    scale.z += Time.deltaTime * ChangeSpeed * 0.5f;
                }
                WaterVfx.transform.localScale = scale;
            }
            else
            {
                //渐变成水完成
                curWaterLerp = 1;
                waterLerp = false;
                WaterVfx.transform.localScale = Vector3.one;
            }
            WaterVfx.SetFloat("LerpPercent", curWaterLerp);
        }
    }
}
