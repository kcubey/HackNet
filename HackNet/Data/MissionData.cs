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
        public string MissionIP { get; set; }
        public string MissionDesc { get; set; }
        public MissionType MissionType { get; set; }
        public RecommendLevel RecommendLevel { get; set; }
        [Required]
        public int MissionExp { get; set; }
        [Required]
        public int MissionCoin { get; set; }


        public static MissionData GetMissionData(int missId,bool ReadOnly=true,DataContext db=null)
        {
           
            if (ReadOnly == true)
            {
                using(DataContext db1=new DataContext())
                {
                    MissionData mis;
                    mis = (from m in db1.MissionData
                           where m.MissionId == missId
                           select m).FirstOrDefault();

                    return mis;
                }
            }else
            {
                MissionData mis;
                mis = (from m in db.MissionData
                       where m.MissionId == missId
                       select m).FirstOrDefault();

                return mis;
            }
            
                
        }

        public static List<MissionData> GetMisList(int recomLvl=-1)
        {           
            try
            {
                using (DataContext db = new DataContext())
                {
                    if (recomLvl != -1)
                    {
                        var query = from mis in db.MissionData where mis.RecommendLevel == (RecommendLevel)recomLvl select mis;
                        return query.ToList();
                    }
                    else
                    {
                        var query = from mis in db.MissionData select mis;
                        return query.ToList();
                    }
                }
            }
            catch (EntityCommandExecutionException)
            {
                throw new ConnectionException("Database link failure has occured");
            }           
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