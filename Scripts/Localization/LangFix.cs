using System.Collections;
using UnityEngine;

namespace Assets.SimpleLocalization
{
    public class LangFix : MonoBehaviour
    {
        private string _defaultLanguage = "English";

        private void Start()
        {
            if (PlayerPrefs.HasKey("Language"))
            {
                LocalizationManager.instance.ChangeLanguage(PlayerPrefs.GetString("Language"));
            }
            else
            {
                LocalizationManager.instance.ChangeLanguage(_defaultLanguage);
            }
        }
    }
}