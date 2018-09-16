namespace BackToMe.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(Namespace = "BackToMe.Models")]
    public sealed class Hero
    {        
        [Key]
        [Required]
        [Range(0, long.MaxValue)]
        [DataMember]
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataMember]
        public string Name { get; set; }

        [Required]
        [Range(1, 500)]
        [DataMember]
        public int Age { get; set; }

        [Required]
        [DataMember]        
        public bool Sex { get; set; }       
    }
}
