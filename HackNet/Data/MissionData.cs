using HackNet.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
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
        public RecommendLevel RecommendLevel { get; set; }

        public static MissionData GetMis()
        {
            MissionData misdata = new MissionData();
            try
            {
                using (DataContext db = new DataContext())
                {
                    misdata = (from mis in db.MissionData select mis).FirstOrDefault();
                }
            }
            catch (EntityCommandExecutionException)
            {
                throw new ConnectionException("Database link failure has occured");
            }

            return misdata;
        }
	}
    public enum RecommendLevel
    {
        Lvl1to5=0,
        Lvl6to10=1,
        Lvl11to15=2,
        Lvl16to20=3
    }
    public enum MissionType
    {
        AtkTypPwdAtks=0,
        AtkTypDdos=1,
        AtkTypMITM=2,
        AtkTypSQL=3,
        AtkTypXSS=4
    }
}