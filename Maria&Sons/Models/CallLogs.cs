using System;
using System.ComponentModel.DataAnnotations;

namespace Maria_Sons.Models
{
	public class CallLogs
	{

		[Key]
		public int id { get; set; }
		[Required]
		public DateTime CallDate { get; set; }
		[Required]
		public string? CallerId { get; set; }
		[Required]
		public double CostOfCall { get; set; }
		[Required]
		public CallType CallType { get; set; }
		[Required]
		public int Duration { get; set; }
		[Required]
		public string? Destination { get; set; }



	}

	public enum CallType
	{
		CellPhone,
		FixedLine,
		International
	}

}