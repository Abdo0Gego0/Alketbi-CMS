using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Enums
{
	public static class BloodType
	{
		
		public static string A_Positive { get { return "A+"; } }
		public static string A_Negative { get { return "A-"; } }

		public static string B_Positive { get { return "B+"; } }
		public static string B_Negative { get { return "B-"; } }

		public static string AB_Positive { get { return "AB+"; } }
		public static string AB_Negative { get { return "AB-"; } }

		public static string O_Positive { get { return "O+"; } }
		public static string O_Negative { get { return "O-"; } }


		public static Dictionary<int,string> BloodTypes()
		{
			return new Dictionary<int, string> { 
				{ 1, A_Positive },
				{ 2, A_Negative },
				{ 3, B_Positive },
				{ 4, B_Negative },
				{ 5, AB_Positive },
				{ 6, AB_Negative },
				{ 7, O_Positive },
				{ 8, O_Negative },
			};
		}
			


	}
}
