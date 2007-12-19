using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Cuyahoga.Modules.ECommerce.Util {

	/// <summary>
	/// Summary description for GraphicUtils.
	/// </summary>
	public class GraphicUtils {

		private const double ASPECT_RATIO = 640 / 480;
		private const double ASPECT_RATIO_MAX_ERROR = 0.01;

		private GraphicUtils() {
		}		

		//Still useful?
		public static bool IsCorrectAspectRatio(Image image) {

			double ratio;
			
			if (IsLandscape(image)) {
				ratio = image.Width / image.Height;
			} else {
				ratio = image.Height / image.Width;
			}

			double error = Math.Abs((ratio - ASPECT_RATIO) / ASPECT_RATIO);
			return error > ASPECT_RATIO_MAX_ERROR;
		}

        public static bool IsLandscape(Image image) {
			return (image.Width > image.Height);
		}		

		/// <summary>
		/// Performs an automatic crop and scale
		/// </summary>
		/// <param name="originalImage"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
        public static Image ResizeImage(Image originalImage, int width, int height) {

			int sourceWidth = originalImage.Width; 
			int sourceHeight = originalImage.Height; 
			
			int xOffset = 0; 
			int yOffset = 0; 

			float scalingFactor;
			
			float widthRatio = ((float)width/(float)sourceWidth); 
			float heightRatio = ((float)height/(float)sourceHeight); 
			
			if (heightRatio < widthRatio) { 
				scalingFactor = widthRatio;
				yOffset = (int) ((height - (sourceHeight * scalingFactor))/2);
			} else { 
				scalingFactor = heightRatio; 
				xOffset = (int) ((width - (sourceWidth * scalingFactor))/2);
			} 
			
			int destWidth = (int)(sourceWidth * scalingFactor); 
			int destHeight = (int)(sourceHeight * scalingFactor); 
			
			Bitmap newImage = new Bitmap(width, height, PixelFormat.Format24bppRgb); 
			newImage.SetResolution(originalImage.HorizontalResolution, originalImage.VerticalResolution); 

			Graphics photo = Graphics.FromImage(newImage); 
			photo.InterpolationMode = InterpolationMode.HighQualityBicubic; 
			
			photo.DrawImage(originalImage, 
				new Rectangle(xOffset, yOffset, destWidth, destHeight), 
				new Rectangle(0, 0, sourceWidth, sourceHeight), 
				GraphicsUnit.Pixel); 
			
			photo.Dispose(); 
			
			return newImage; 
		}

        public static Image RotateClockwise(Image image) {
			return FlipRotateImage(image, RotateFlipType.Rotate90FlipNone);
		}

        public static Image RotateAnticlockwise(Image image) {
			return FlipRotateImage(image, RotateFlipType.Rotate270FlipNone);
		}

        private static Image FlipRotateImage(Image image, RotateFlipType FlipType) {
			Image newImage = new Bitmap(image);
			newImage.RotateFlip(FlipType);
			return newImage;
		}

        public static void CreateThumbnail(string source, string destination, int width, int height) {

			Image image = Image.FromFile(source);
			Image thumbnail;

			if (IsLandscape(image)) {
				thumbnail = ResizeImage(image, width, height);
			} else {
				thumbnail = ResizeImage(image, height, width);
			}

			thumbnail.Save(destination, image.RawFormat);

			image.Dispose();
			thumbnail.Dispose();
		
		}

        public static void CreateClockwiseRotatedImage(string source, string destination) {

			Image image = Image.FromFile(source);
			Image rotated = RotateClockwise(image);
			rotated.Save(destination, image.RawFormat);
		
			image.Dispose();
			rotated.Dispose();
		}

        public static void CreateAnticlockwiseRotatedImage(string source, string destination) {

			Image image = Image.FromFile(source);
			Image rotated = RotateAnticlockwise(image);
			rotated.Save(destination, image.RawFormat);
		
			image.Dispose();
			rotated.Dispose();
		}
	}
}