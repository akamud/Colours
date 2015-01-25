
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace AndroidSample
{
	public class ColourPalette : LinearLayout
	{
		public ColourPalette (Context context) :
			base (context)
		{
			Initialize (context);
		}

		public ColourPalette (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize (context);
		}

		void Initialize (Context context)
		{
			View.Inflate(context, Resource.Layout.ColourPalette, this);
		}
	}
}

