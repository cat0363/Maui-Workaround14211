#if ANDROID
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_Workaround14211
{
    public partial class ServiceFocus
    {
        /// <summary>
        /// Clear Focus
        /// </summary>
        public partial void ClearFocus()
        {
            Android.Views.View view = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.CurrentFocus;
            if (view != null && view is EditText)
            {
                view.ClearFocus();
            }
        }
    }
}
#endif