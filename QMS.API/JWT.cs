﻿namespace QMS.API
{
	public class JWT
	{
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public int Lifetime { get; set; }
		public string Key { get; set; }

	}
}
