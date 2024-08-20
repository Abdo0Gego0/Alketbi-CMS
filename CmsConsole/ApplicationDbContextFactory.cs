using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsConsole
{
	using CmsDataAccess;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Design;
	using Microsoft.Extensions.Configuration;
	using System.IO;

	public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		public ApplicationDbContext CreateDbContext(string[] args)
		{


			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			//optionsBuilder.UseSqlServer("Data Source=SQL6031.site4now.net,1433;Initial Catalog=db_aa21a1_alketbi;User Id=db_aa21a1_alketbi_admin;Password=alketbi_125874;TrustServerCertificate=True;MultipleActiveResultSets=true");
			//optionsBuilder.UseSqlServer("Data Source=SQL8005.site4now.net,1433;Initial Catalog=db_a94910_cms;User Id=db_a94910_cms_admin;Password=wh1v1Q1w2_e3r411223;TrustServerCertificate=True");

			return new ApplicationDbContext(optionsBuilder.Options);
		}
	}
}
