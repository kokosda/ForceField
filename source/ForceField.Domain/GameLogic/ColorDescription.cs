using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForceField.Domain.GameLogic
{
    public class ColorDescription
    {
        public ColorDescription(NationColor primaryColor, NationColor secondaryColor = NationColor.None, bool isTrueDualColor = false)
        {
            this.primaryColor = primaryColor;
            this.secondaryColor = secondaryColor;
            this.isTrueDualColor = isTrueDualColor;
        }

        #region Properties

        public NationColor PrimaryColor 
        {
            get
            {
                return primaryColor;
            }
        }

        public NationColor SecondaryColor 
        {
            get
            {
                return secondaryColor;
            }
        }

        public bool IsTrueDualColor 
        {
            get
            {
                return isTrueDualColor;
            }
        }

        public bool IsDualColor 
        {
            get
            {
                if (SecondaryColor != NationColor.None)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region private 

        private NationColor primaryColor;
        private NationColor secondaryColor;
        private bool isTrueDualColor;

        #endregion
    }
}
