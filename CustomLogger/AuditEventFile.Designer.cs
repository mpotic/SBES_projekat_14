﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CustomLogger {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class AuditEventFile {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AuditEventFile() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CustomLogger.AuditEventFile", typeof(AuditEventFile).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service {0} failed to create a smart card for {1}..
        /// </summary>
        internal static string CardCreationFailure {
            get {
                return ResourceManager.GetString("CardCreationFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service {0} successfully created a smart card for {1}..
        /// </summary>
        internal static string CardCreationSuccess {
            get {
                return ResourceManager.GetString("CardCreationSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service {0} failed to pay {1} to user {2}!.
        /// </summary>
        internal static string PaymentFailure {
            get {
                return ResourceManager.GetString("PaymentFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service {0} successfully payed {1} to user {2}!.
        /// </summary>
        internal static string PaymentSuccess {
            get {
                return ResourceManager.GetString("PaymentSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service {0} failed to payout {1} to user {2}!.
        /// </summary>
        internal static string PayoutFailure {
            get {
                return ResourceManager.GetString("PayoutFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service {0} successfully payed out {1} to user {2}!.
        /// </summary>
        internal static string PayoutSuccess {
            get {
                return ResourceManager.GetString("PayoutSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service {0} failed to change user {1} pin!.
        /// </summary>
        internal static string PinChangeFailure {
            get {
                return ResourceManager.GetString("PinChangeFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service {0} successfully changed user {1} pin!.
        /// </summary>
        internal static string PinChangeSuccess {
            get {
                return ResourceManager.GetString("PinChangeSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service {0} failed to replicate {1} pin chage!.
        /// </summary>
        internal static string PinReplicationFailure {
            get {
                return ResourceManager.GetString("PinReplicationFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service {0} successfully replicated {1} pin change..
        /// </summary>
        internal static string PinReplicationSuccess {
            get {
                return ResourceManager.GetString("PinReplicationSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service {0} failed to replicate {1} smart card data!.
        /// </summary>
        internal static string ReplicationFailure {
            get {
                return ResourceManager.GetString("ReplicationFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service {0} successfully replicated {1} smart card data..
        /// </summary>
        internal static string ReplicationSuccess {
            get {
                return ResourceManager.GetString("ReplicationSuccess", resourceCulture);
            }
        }
    }
}
