namespace Example.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Contact")]
    public class Contact
    {
        public Contact()
        {
            
        }

        public long Id { get; set; }

        [Required]
        public int EdvNr { get; set; }

        [Required]
        [StringLength(10)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(10)]
        public string LastName { get; set; }
        
        [StringLength(35)]
        public string Street { get; set; }

        [StringLength(35)]
        public string Ort { get; set; }
        
        public virtual Customer Customer { get; set; }



    }
}
