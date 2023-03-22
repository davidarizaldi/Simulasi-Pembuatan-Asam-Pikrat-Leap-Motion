using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuideHudUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text guideText;

    private static readonly string[] guides =
    {
        "Add 5 grams of phenol into the 250 ml round bottom flask.",
        "Add 7 ml of concentrated sulfuric acid.\n\n(Sulfuric acid in the graduated cylinder is pre-measured)",
        "Heat it with a water heater for 30 minutes while shaking it.",
        "Cool the flask in a mixture of water and ice then place them on the hotplate.",
        "While the liquid is stirred, add 20 ml of concentrated nitric acid.\n\nLet stand for a few moments until a reaction occurs and a red smoke formed.",
        "When the reaction stops, remove the ice bath, then heat it for 1.5 - 2 hours while shaking it on the water heater.",
        "Turn off the hotplate.\n\nPour in 200 ml of cold water.",
        "Filter with a suction funnel."
    };
    
    public void UpdateGuideHud()
    {
        guideText.SetText(guides[GameManager.practicumStep]);
    }
}
