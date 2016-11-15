namespace Example.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Customer")]
    public class Customer
    {
        public Customer()
        {
            this.Contacts = new HashSet<Contact>();
        }

        public long Id { get; set; }

        [Required]
        public int EdvNr { get; set; }

        [StringLength(100)]
        public string Firma1 { get; set; }

        [StringLength(100)]
        public string Firma2 { get; set; }

        [StringLength(35)]
        public string Street { get; set; }

        [StringLength(35)]
        public string Ort { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

    }
}
