﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Battleships.Domain.Resources {
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
    public class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Battleships.Domain.Resources.Resource", typeof(Resource).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}, call out a position on your targeting grid..
        /// </summary>
        public static string CallOutPositionOnTargetingGrid {
            get {
                return ResourceManager.GetString("CallOutPositionOnTargetingGrid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field must be beetwen {0} and {1}..
        /// </summary>
        public static string ErrorFieldMustBeBetween {
            get {
                return ResourceManager.GetString("ErrorFieldMustBeBetween", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You will have to try choose different starting point or direction for {0}. {1}.
        /// </summary>
        public static string ErrorGetNextPoint {
            get {
                return ResourceManager.GetString("ErrorGetNextPoint", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The next point {0} is off the grid..
        /// </summary>
        public static string ErrorNextPointIsOffGrid {
            get {
                return ResourceManager.GetString("ErrorNextPointIsOffGrid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The point {0} is off the grid..
        /// </summary>
        public static string ErrorPointIsOffGrid {
            get {
                return ResourceManager.GetString("ErrorPointIsOffGrid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You will have to try choose different starting point or direction for {0}. The point {1} is already selected or next to a different ship..
        /// </summary>
        public static string ErrorSelectPoint {
            get {
                return ResourceManager.GetString("ErrorSelectPoint", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You will have to try choose different starting point or direction for {0}. The point {1} is not allowed..
        /// </summary>
        public static string ErrorStartingPoint {
            get {
                return ResourceManager.GetString("ErrorStartingPoint", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to To many hits on ship {0}..
        /// </summary>
        public static string ErrorToManyHitsOnShip {
            get {
                return ResourceManager.GetString("ErrorToManyHitsOnShip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Game ended. {0} won..
        /// </summary>
        public static string GameEnded {
            get {
                return ResourceManager.GetString("GameEnded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}, place the {1} on the ocean grid. Choose starting point and direction horizontal or vertical..
        /// </summary>
        public static string PlaceShipMessage {
            get {
                return ResourceManager.GetString("PlaceShipMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There will be {0} ships in the game that you will have to sink. If you want to quit the game just press &apos;q&apos;..
        /// </summary>
        public static string PlayRuleDescription {
            get {
                return ResourceManager.GetString("PlayRuleDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Well done. All ships are on the grid. Let&apos;s start the game..
        /// </summary>
        public static string ShipsOnGrid {
            get {
                return ResourceManager.GetString("ShipsOnGrid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are welcome in the Battleships Game..
        /// </summary>
        public static string WelcomeGame {
            get {
                return ResourceManager.GetString("WelcomeGame", resourceCulture);
            }
        }
    }
}
