using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.models
{  

    public static class Settings
    {
        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }

        private const string UserNameKey = "username_key";
        private static readonly string UserNameDefault = string.Empty;

        public static string UserName
        {
            get { return AppSettings.GetValueOrDefault<string>(UserNameKey, UserNameDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserNameKey, value); }
        }


        private const string UserPasswordKey = "userpassword_key";
        private static readonly string UserPasswordDefault = string.Empty;

        public static string UserPassword
        {
            get { return AppSettings.GetValueOrDefault<string>(UserPasswordKey, UserPasswordDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserPasswordKey, value); }
        }

        private const string ColourCodes = "stage_colour_code";
        private static readonly string UserColourCodeDefault = string.Empty;

        public static string StageColourCode
        {
            get { return AppSettings.GetValueOrDefault<string>(ColourCodes, UserColourCodeDefault); }
            set { AppSettings.AddOrUpdateValue<string>(ColourCodes, value); }

        }

        private const string PrefKeyIsLockedkey = "False";
        private static readonly string PrefKeyIsLockedDefault = "False";

        public static string PrefKeyIsLocked
        {
            get { return AppSettings.GetValueOrDefault<string>(PrefKeyIsLockedkey, PrefKeyIsLockedDefault); }
            set { AppSettings.AddOrUpdateValue<string>(PrefKeyIsLockedkey, value); }
        }

        private const string PrefKeyUserDetailsKey = "UserDetails";
        private static readonly string PrefKeyUserDetailsDefault = "UserDefaultDetails";

        public static string PrefKeyUserDetails
        {
            get { return AppSettings.GetValueOrDefault<string>(PrefKeyUserDetailsKey, PrefKeyUserDetailsDefault); }
            set { AppSettings.AddOrUpdateValue<string>(PrefKeyUserDetailsKey, value); }
        }


    }
}
