using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
	public partial class MissionData
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MissionId { get; set; }
		public string MissionName { get; set; }
        public MissionType MissionType { get; set; }

	}
    public enum MissionType
    {
        AtkTypDdos=1,
        AtkTyp
    }
}