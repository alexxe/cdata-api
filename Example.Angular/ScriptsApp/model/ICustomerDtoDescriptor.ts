
    
    // $Classes/Enums/Interfaces(filter)[template][separator]
    // filter (optional): Matches the name or full name of the current item. * = match any, wrap in [] to match attributes or prefix with : to match interfaces or base classes.
    // template: The template to repeat for each matched item
    // separator (optional): A separator template that is placed between all templates e.g. $Properties[public $name: $Type][, ]

    // More info: http://frhagn.github.io/Typewriter/

    
export interface ICustomerDtoDescriptor extends IFilterDescriptor {
        
        id?: IOperator<number> | IInOperator<number>;
        edvNr?: IOperator<number> | IInOperator<number>;
        firma1?: IOperator<string> | IInOperator<string>;
        firma2?: IOperator<string> | IInOperator<string>;
        street?: IOperator<string> | IInOperator<string>;
        ort?: IOperator<string> | IInOperator<string>;
        contacts?: IMethod<IContactDtoDescriptor>;
        
} 
