using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Entities
{
	public class IssueEntity
	{
		public int Id { get; set; }	
		public int IdPos { get; set; }	
		public PosEntity Pos { get; set; }//+
		public int IdType { get; set; }	
		public	IssuesTypeEntity IssuesType { get; set; }	
		public int IdSubType { get; set; }  //-
		//public IdSubTypeEntity
		public int IdProblem { get; set; }	//-
		//public IdProblemEntity
		public string Priority { get; set; }
		public int IdStatus { get; set; }
		public StatusEntity Status { get; set; } //-
		public string Memo { get; set; }
        public int IdUserCreated { get; set; }
		public UserEntity User { get; set; }//+
		public int IdUserType { get; set; }
		public UserTypeEntity UserType { get; set; }//+
		public string Description { get; set; }
		public long AssignedDate { get; set; }
		public long CreationDate { get; set;}
		public long ModifDate{ get; set; }
		public string Solotion { get; set; }
		public List<LogEntity> Logs { get; set; }//+
		public long? DeleteAt { get; set; }	

	}
}
