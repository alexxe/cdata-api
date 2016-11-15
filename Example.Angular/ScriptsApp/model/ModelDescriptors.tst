${
    // Enable extension methods by adding using Typewriter.Extensions.*
    using Typewriter.Extensions.Types;

    // Uncomment the constructor to change template settings.
     Template(Settings settings)
     {
        settings.IncludeProject("Example.Data.Contract"); 
        settings.OutputFilenameFactory = file => string.Format("I{0}Descriptor.ts",file.Classes.First().Name);
     }

    // Custom extension methods can be used in the template by adding a $ prefix e.g. $LoudName
    string LoudName(Property property)
    {
        return property.Name.ToUpperInvariant();
    }
    string InterfaceName(Class c)
    {
        return string.Format("I{0}Descriptor",c.Name);
    }
    string PropertyType(Property p)
    {
        if(p.Type.TypeArguments.FirstOrDefault() != null) 
        {
            return string.Format("IMethod<I{0}Descriptor>",p.Type.TypeArguments.FirstOrDefault().Name);
        }
        if(p.Type.OriginalName.Contains("Dto")) 
        {
            return string.Format("I{0}Descriptor",p.Type.OriginalName);
        }

        return string.Format("IOperator<{0}> | IInOperator<{0}>",p.Type);
    }
}
    
    // $Classes/Enums/Interfaces(filter)[template][separator]
    // filter (optional): Matches the name or full name of the current item. * = match any, wrap in [] to match attributes or prefix with : to match interfaces or base classes.
    // template: The template to repeat for each matched item
    // separator (optional): A separator template that is placed between all templates e.g. $Properties[public $name: $Type][, ]

    // More info: http://frhagn.github.io/Typewriter/

    $Classes(*Dto)[
export interface $InterfaceName extends IFilterDescriptor {
        $Properties[
        $name?: $PropertyType;]
        
}] 
