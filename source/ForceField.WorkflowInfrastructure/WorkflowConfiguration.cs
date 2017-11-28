using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace ForceField.WorkflowInfrastructure
{
    public class WorkflowConfiguration
    {
        public static string SelectedServicePage
        {
            get
            {
                return ConfigurationManager.AppSettings["SelectedServicePage"];
            }
        }

        public static string CharactersAnimFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["CharactersAnimFilePath"];
            }
        }

        public static string[] AnimationPaths
        {
            get
            {
                return ToValuesArray("animationpaths");
            }
        }

        public static string[] SpritePaths
        {
            get
            {
                return ToValuesArray("spritepaths");
            }
        }

        private static string[] ToValuesArray(string configSection)
        {
            var collection = ConfigurationManager.GetSection(configSection) as NameValueCollection;
            var array = new string[collection.Count];

            for (var i = 0; i < array.Count(); i++)
            {
                array[i] = collection[i];
            }

            return array;
        }
    }
}
