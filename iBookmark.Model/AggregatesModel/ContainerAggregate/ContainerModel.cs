using System.ComponentModel.DataAnnotations;

namespace iBookmark.Model.AggregatesModel.ContainerAggregate
{
    public class ContainerModel
    {
        public int ContainerId { get; set; }

        [Required]
        public string ContainerName { get; set; }

        [Required]
        public int UserId { get; set; }

        public bool IsDefault { get; set; }
    }
}
