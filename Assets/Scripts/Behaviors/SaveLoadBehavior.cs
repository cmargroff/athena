using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SaveLoadBehavior :BaseMonoBehavior
{

    private const string SettingsFile= "settings.es3";
    private const string SettingsKey = "settings";
    public void SaveSettings(Settings settings)
    {

        ES3.Save(SettingsKey, settings, SettingsFile);
    }

    public Settings LoadSettings()
    {
        if (ES3.FileExists(SettingsFile))
        {
            return ES3.Load<Settings>(SettingsKey, SettingsFile);
        }
        else
        {
            return new Settings() { MusicVolume = 1, VoiceVolume = 1, EffectsVolume = 1, MasterVolume = 1 }; 
        }
    }
}
