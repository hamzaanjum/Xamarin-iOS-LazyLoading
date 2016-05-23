using System;

namespace Lazy
{
	public class TableSource
	{
		public TableSource ()
		{
		}
		
		async void BeginDownloadingImage (string ImageUrl, NSIndexPath indexPath, UITableView tableView)
		{
			var cell = tableView.CellAt(indexPath);

			if (cell != null)
			{
				cell.ImageView.Image = UIImage.FromBundle("TemporaryImageFromBundle"); // just to show something as a placeholder while the actual image is being downloaded
				var returnImage = await LoadImage(ImageUrl);
				if (returnImage != null)
					cell.ImageView.Image = returnImage;
			}
		}
		
		async Task<UIImage> LoadImage(string url)
		{
			return await Task.Run(() =>
			{
				UIImage img = null;
				try
				{
					var data = new System.Net.WebClient().DownloadData(url);
					img = UIImage.LoadFromData(NSData.FromArray(data));
				}
				catch { img = null; }
				return img;
			});
		}
	}
}

