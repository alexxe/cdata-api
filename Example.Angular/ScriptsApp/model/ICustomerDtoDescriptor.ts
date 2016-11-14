
module Example.ModelDescriptors {
    
    // $Classes/Enums/Interfaces(filter)[template][separator]
    // filter (optional): Matches the name or full name of the current item. * = match any, wrap in [] to match attributes or prefix with : to match interfaces or base classes.
    // template: The template to repeat for each matched item
    // separator (optional): A separator template that is placed between all templates e.g. $Properties[public $name: $Type][, ]

    // More info: http://frhagn.github.io/Typewriter/

    
    export interface ICustomerDtoDescriptor extends CData.IFilterDescriptor {
        
        // ID
        id?: CData.IOperator<string> | CData.IInOperator<string>;
        // EDVNR
        edvNr?: CData.IOperator<string> | CData.IInOperator<string>;
        // CUSTOMERNR
        customerNr?: CData.IOperator<string> | CData.IInOperator<string>;
        // FIRMA1
        firma1?: CData.IOperator<string> | CData.IInOperator<string>;
        // FIRMA2
        firma2?: CData.IOperator<string> | CData.IInOperator<string>;
        // SHORTNAME
        shortName?: CData.IOperator<string> | CData.IInOperator<string>;
        // STREET
        street?: CData.IOperator<string> | CData.IInOperator<string>;
        // ORT
        ort?: CData.IOperator<string> | CData.IInOperator<string>;
        // CONTACTS
        contacts?: CData.IMethod<IContactDtoDescriptor>;
        
    } 
}