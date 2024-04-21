using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTradeHubViewService.Models
{
    public class Model
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("marks")]
        public int MarkaId { get; set; }
        public Marka? Marka { get; set; }
    }
}
