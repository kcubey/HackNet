using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackNet.Data
{
    public class Machines
    {
		[Key, ForeignKey("User")]
		public int UserId {get; set;}
		public string MachineName { get; set; }
		public int Health{get; set;}
		public int Speed{get; set;}
		public int Attack{get; set;}
		public int Defence{get; set;}
		// Foreign key references
		public virtual Users User { get; set; }
    }
}