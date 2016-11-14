
namespace Example.Data.Contract.Model 
{
    using System;
    using System.Collections.Generic;

    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;
    // $Classes/Enums/Interfaces(filter)[template][separator]
    // filter (optional): Matches the name or full name of the current item. * = match any, wrap in [] to match attributes or prefix with : to match interfaces or base classes.
    // template: The template to repeat for each matched item
    // separator (optional): A separator template that is placed between all templates e.g. $Properties[public $name: $Type][, ]

    // More info: http://frhagn.github.io/Typewriter/

    
    public class CustomerDtoDescriptor  : CustomerDto, ISearchableDescriptor {
        
        
        public new PropertyAcsessor<string> Id { get; set; }
        
        public new PropertyAcsessor<string> EdvNr { get; set; }
        
        public new PropertyAcsessor<string> CustomerNr { get; set; }
        
        public new PropertyAcsessor<string> Firma1 { get; set; }
        
        public new PropertyAcsessor<string> Firma2 { get; set; }
        
        public new PropertyAcsessor<string> ShortName { get; set; }
        
        public new PropertyAcsessor<string> Street { get; set; }
        
        public new PropertyAcsessor<string> Ort { get; set; }
        
        public new IEnumerable<ContactDto> Contacts { get; set; }
        
    } 
}