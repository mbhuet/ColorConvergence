﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PostEffect : MonoBehaviour {
    #region Variables  
    public Shader curShader;
    public float scale = 0.1f;
    public bool isLight = false;
    public Texture CRT_Tex;
    public Texture Moire_Tex;
    private Material curMaterial;
    Material material
    {
        get {
            if (curMaterial == null) {
                curMaterial = new Material(curShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }
    #endregion

    void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
        }
        if(!curShader && !curShader.isSupported)
        {
            enabled = false;
        }
    }
    void Update()
    {
        scale = Mathf.Clamp(scale, 0.0f, 2.0f);
    }
    void OnRenderImage(RenderTexture source, RenderTexture target)
    {
        if (curShader != null)
        {
            material.SetFloat("_Scale", scale);
            material.SetFloat("_isLight", (isLight)? 1.0f : 0.0f);
            material.SetTexture("_CRT_Tex", CRT_Tex);
            material.SetTexture("_Moire_Tex", Moire_Tex);
            Graphics.Blit(source, target, material);
        }
        else
        {
            Graphics.Blit(source, target);
        }
    }
    void OnDisable()
    {
        if (curMaterial)
        {
            DestroyImmediate(curMaterial);
        }
    }
}