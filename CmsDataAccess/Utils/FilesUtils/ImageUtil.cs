using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace CmsDataAccess.Utils.FilesUtils
{
	public static class ImageUtil
	{
		public static string GetImage(string fullOutputPath)
		{
			using (Image image = Image.FromFile(fullOutputPath))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					image.Save((Stream)memoryStream, image.RawFormat);
					return Convert.ToBase64String(memoryStream.ToArray());
				}
			}

			return "";


		}

		public static Image SaveImage(string data, string fullOutputPath)
		{
			byte[] bytes = Convert.FromBase64String(data.Split(';')[1].Split(',')[1]);

			Image image;
			using (MemoryStream ms = new MemoryStream(bytes))
			{
				image = Image.FromStream(ms);
			}

			var ImageToSave = new Bitmap(image);
			ImageToSave.Save(fullOutputPath, ImageFormat.Png);
			return image;
		}

	}
}
