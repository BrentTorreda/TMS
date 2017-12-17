
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class MemberPositions
    {
        [Key]
        public int MemberPositionId { get; set; }

        public string PositionName { get; set; }
    }
}