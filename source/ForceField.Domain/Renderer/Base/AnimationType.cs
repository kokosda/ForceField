using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ForceField.Domain.Renderer.Base
{
    public class AnimationTypes
    {
        public static void Initialize()
        {
            if (Types == null)
            {
                var values = Enum.GetValues(typeof(AnimatedAction));

                Types = new Dictionary<string, AnimatedAction>(values.Length);

                foreach (var value in values)
                {
                    Types.Add(((AnimatedAction)value).ToString(), (AnimatedAction)value);
                }
            }
        }

        public static AnimatedAction Get(string name)
        {
            return Types[name];
        }

        public static IOrderedEnumerable<KeyValuePair<string, AnimatedAction>> OrderedTypes
        {
            get
            {
                return Types.OrderBy(pair => pair.Value);
            }
        }

        #region private

        private static Dictionary<string, AnimatedAction> Types { get; set; }

        #endregion
    }
}
