﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Arad.SMS.Gateway.GeneralLibrary.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.8.1.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://bpm.shaparak.ir/pgwchannel/services/pgw")]
        public string GeneralLibrary_bankmellat_bpm_PaymentGatewayImplService {
            get {
                return ((string)(this["GeneralLibrary_bankmellat_bpm_PaymentGatewayImplService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://pec.shaparak.ir/pecpaymentgateway/EShopService.asmx")]
        public string GeneralLibrary_Parsian_EShopService {
            get {
                return ((string)(this["GeneralLibrary_Parsian_EShopService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://pec.shaparak.ir/NewIPGServices/Sale/SaleService.asmx")]
        public string GeneralLibrary_ir_shaparak_pec_SaleService {
            get {
                return ((string)(this["GeneralLibrary_ir_shaparak_pec_SaleService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://pec.shaparak.ir/NewIPGServices/Confirm/ConfirmService.asmx")]
        public string GeneralLibrary_ir_shaparak_pec1_ConfirmService {
            get {
                return ((string)(this["GeneralLibrary_ir_shaparak_pec1_ConfirmService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://pec.shaparak.ir/NewIPGServices/Reverse/ReversalService.asmx")]
        public string GeneralLibrary_ir_shaparak_pec2_ReversalService {
            get {
                return ((string)(this["GeneralLibrary_ir_shaparak_pec2_ReversalService"]));
            }
        }
    }
}