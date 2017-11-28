using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForceField.WorkflowControls
{
    public class ServicePageCollection : System.Windows.Forms.TabControl.TabPageCollection
    {
        public ServicePageCollection(ServicePageContainer container) : base(container) { }

        public new ServicePage this[string key]
        {
            get
            {
                return base[key] as ServicePage;
            }
        }
    }
}
