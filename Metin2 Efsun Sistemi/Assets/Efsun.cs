using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Efsun : MonoBehaviour
{
    public string aciklama;
    public TMP_Text uiText;

    [Header("Efsun Türü")]
    public bool ortalamaHasar;
    public bool beceriHasar;
    public int gelenOrtalama;
    public int gelenBeceri;


    [Header("Efsun Botu")]
    public int beklenenOrtalama;
    public float efsunAtmaTimer = 0.2f;
    public TMP_InputField botOranText;
    public bool botAktif;
    public int kullanilanEfsunSayisi;
    float timer;


    [Header("Ortalama Oraný")]
    public List<int> ortalamaMaxOran;
    public List<int> ortalamaOraninGelmeOrani;
    public List<bool> ortalamaOranGeldimi;

    [Header("Beceri Oraný")]
    public List<int> beceriMaxOran;
    public List<int> beceriMaxOraninGelmeOrani;
    public List<bool> beceriOranGeldimi;

    private void Update()
    {
        if (botAktif)
        {
            timer += Time.deltaTime;
            if(timer > efsunAtmaTimer)
                EfsunBotu();
        }
    }

    public void BotAktif()
    {
        if (botAktif)
        {
            botAktif = false;
            return;
        }
        beklenenOrtalama = int.Parse(botOranText.text);
        if (beklenenOrtalama < 1)
            return;
        kullanilanEfsunSayisi = 0;
        botAktif = true;
    }

    void EfsunBotu()
    {
        if (!botAktif)
            return;

        EfsunAt();

        kullanilanEfsunSayisi++;
        timer = 0;
        if (gelenOrtalama >= beklenenOrtalama)
            botAktif = false;

    }

    public void EfsunAt()
    { 
        ortalamaHasar = (Random.Range(0,2) == 0) ? true : false;
        beceriHasar = !ortalamaHasar;
        ortalamaOranGeldimi.Clear();
        beceriOranGeldimi.Clear();
        if (ortalamaHasar)
        {
            for (int i = 0; i < ortalamaMaxOran.Count; i++)
            {
                bool oran = (Random.Range(1, 101) <= ortalamaOraninGelmeOrani[i]) ? true : false;
                ortalamaOranGeldimi.Add(oran);
                if (!oran || i == ortalamaMaxOran.Count - 1)
                {
                    i = ortalamaMaxOran.Count;
                    if (ortalamaOranGeldimi.Count > 1)
                        gelenOrtalama = Random.Range(1, ortalamaMaxOran[ortalamaOranGeldimi.Count - 2]);
                    else
                        gelenOrtalama = Random.Range(1, ortalamaMaxOran[0]);
                    gelenBeceri = (gelenOrtalama < 3) ? -1 : -gelenOrtalama / 3;

                    aciklama = $"Gelmesi gereken Max Ortalama: {ortalamaMaxOran[(ortalamaOranGeldimi.Count > 1) ? ortalamaOranGeldimi.Count - 2 : 0]}\n\n\n" + 
                        $" Ortalama Hasar: {gelenOrtalama}\n" +
                        $" Beceri Hasar: {gelenBeceri}";
                }
            }
        }
        else if (beceriHasar)
        {
            for (int i = 0; i < beceriMaxOran.Count; i++)
            {
                bool oran = (Random.Range(1, 101) <= beceriMaxOraninGelmeOrani[i]) ? true : false;
                beceriOranGeldimi.Add(oran);
                if (!oran || i == beceriMaxOran.Count - 1)
                {
                    i = beceriMaxOran.Count;
                    if(beceriOranGeldimi.Count > 1)
                        gelenBeceri = Random.Range(1, beceriMaxOran[beceriOranGeldimi.Count - 1]);
                    else
                        gelenBeceri = Random.Range(1, beceriMaxOran[0]);
                    gelenOrtalama = -gelenBeceri * 2;
                    aciklama = $"Gelmesi gereken Max Beceri: {beceriMaxOran[(beceriOranGeldimi.Count > 1) ? beceriOranGeldimi.Count - 2 : 0]}\n\n\n" +
                        $" Ortalama Hasar: {gelenOrtalama}\n" +
                        $" Beceri Hasar: {gelenBeceri}";
                }
            }
        }

        uiText.text = aciklama;
    }
}
