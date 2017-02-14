// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace PreStart.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }

    #region Setting Constants

        private const string SiteKey = "site_key";
        private static readonly string SiteDefault = string.Empty;

        private const string PrestartKey = "prestart_key";
        private static readonly string PrestartDefault = string.Empty;

    #endregion


        public static string DefaultSiteSetting
    {
      get
      {
        return AppSettings.GetValueOrDefault<string>(SiteKey, SiteDefault);
      }
      set
      {
        AppSettings.AddOrUpdateValue<string>(SiteKey, value);
      }
    }

        


    public static string SelectedPrestartId
    {
        get
        {
            return AppSettings.GetValueOrDefault<string>(PrestartKey, PrestartDefault);
        }
        set
        {
            AppSettings.AddOrUpdateValue<string>(PrestartKey, value);
        }
    }

    }
}