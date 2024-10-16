using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.Interfaces
{
	public interface ICSRPerformance
	{
		IEnumerable<object> CreateEmployeePerformance();
	}
}
