using CmsDataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CmsWeb.Utils.UserAccount
{
	public  class PasswordUtil
	{
		public static IConfiguration _config;
		public static UserManager<IdentityUser> _userManager; // Add UserManager


		public PasswordUtil(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}

		public  string GetRolesAsync(IdentityUser user)
		{
			var roles = _userManager.GetRolesAsync(user).Result;
			return string.Join(",", roles);
		}
		public  string CreateTokenForPatient(Patient pat)
		{
			var roles = GetRolesAsync(pat.User);

			List<Claim> claims = new List<Claim>()
			{
				new Claim(ClaimTypes.NameIdentifier, pat.User.UserName),
				new Claim(ClaimTypes.SerialNumber, pat.Id.ToString()),
				new Claim(ClaimTypes.Name, pat.FullName ),
				new Claim(ClaimTypes.Email, pat.User.Email),
				new Claim(ClaimTypes.MobilePhone, pat.User.PhoneNumber),
				new Claim(ClaimTypes.Role, roles),
			};


			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
				getCongigValue("jwt", "Key")
				));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
			var token = new JwtSecurityToken(
				issuer: getCongigValue("jwt", "Issuer"),
				audience: getCongigValue("jwt", "Audience"),
				claims: claims,
				expires: DateTime.Now.AddDays(30),
				signingCredentials: creds

				);
			var jwt = new JwtSecurityTokenHandler().WriteToken(token);
			return jwt;
		}
		public  void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}
		public  bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512(passwordSalt))
			{
				var computedPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

				return computedPasswordHash.SequenceEqual(passwordHash);
			}
		}
		public  string? CreateRandomToken(string UserName)
		{
			List<Claim> claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name,UserName),
			};
			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(getCongigValue("AppSettings", "Token")));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
			var token = new JwtSecurityToken(
				issuer: getCongigValue("jwt", "Issuer"),
				audience: getCongigValue("jwt", "Issuer"),
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: creds

				);
			var jwt = new JwtSecurityTokenHandler().WriteToken(token);
			return jwt;
		}
		public  string CreateRandomToken8()
		{
			return Convert.ToHexString(RandomNumberGenerator.GetBytes(8));
		}

        public string CreateRandomTokenN(int N)
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(N));
        }


        public string getCongigValue(string sec, string subSec)
		{
			var configurationBuilder = new ConfigurationBuilder();
			var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
			configurationBuilder.AddJsonFile(path, false);
			var root = configurationBuilder.Build();
			var domain = root.GetSection(sec);
			var StripeApiKey = domain.GetSection(subSec).Value;
			return StripeApiKey;
		}

	}
}
