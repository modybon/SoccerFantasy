using System;
namespace SoccerFantasy.Models
{
	public class CurrentUser
	{
		private static readonly object lockObject = new object();
		private static User instance = null;
		private CurrentUser()
		{

		}

		public static User Instance
		{
			get
			{
				lock (lockObject)
				{
                    return instance ?? null;
                }
				
			}
			set
			{
				lock (lockObject)
				{
                    if (instance == null)
                    {
                        instance = value;
                    }
                }
			}

		}
	}
}

