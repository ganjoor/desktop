﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace gsync2vid.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string s3dbpath {
            get {
                return ((string)(this["s3dbpath"]));
            }
            set {
                this["s3dbpath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int PoemId {
            get {
                return ((int)(this["PoemId"]));
            }
            set {
                this["PoemId"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int AudioId {
            get {
                return ((int)(this["AudioId"]));
            }
            set {
                this["AudioId"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Tahoma, 20.25pt")]
        public global::System.Drawing.Font LastUsedFont {
            get {
                return ((global::System.Drawing.Font)(this["LastUsedFont"]));
            }
            set {
                this["LastUsedFont"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("960")]
        public int LastImageWidth {
            get {
                return ((int)(this["LastImageWidth"]));
            }
            set {
                this["LastImageWidth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("720")]
        public int LastImageHeight {
            get {
                return ((int)(this["LastImageHeight"]));
            }
            set {
                this["LastImageHeight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string LastImagePath {
            get {
                return ((string)(this["LastImagePath"]));
            }
            set {
                this["LastImagePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://source.unsplash.com/random/{0}x{1}")]
        public string UnsplashSearchUrl {
            get {
                return ((string)(this["UnsplashSearchUrl"]));
            }
            set {
                this["UnsplashSearchUrl"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("nature")]
        public string UnsplashSearchTerm {
            get {
                return ((string)(this["UnsplashSearchTerm"]));
            }
            set {
                this["UnsplashSearchTerm"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string FFmpegPath {
            get {
                return ((string)(this["FFmpegPath"]));
            }
            set {
                this["FFmpegPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("wmv")]
        public string VidDefExt {
            get {
                return ((string)(this["VidDefExt"]));
            }
            set {
                this["VidDefExt"] = value;
            }
        }
    }
}
