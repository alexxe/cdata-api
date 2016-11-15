${
    Template(Settings settings)
    {
        settings.IncludeProject("Example.Data.Contract");
    }
    string genericTypeName(Property p)
    {
        return p.Type.TypeArguments.FirstOrDefault();
        
    }
    
}
$Classes(*Dto)[ 

    export class $Name$TypeParameters implements IModel{ 
        type: string;
		constructor() {
			this.type = "$FullName,Example.Data.Contract";
			
                    
        }      
        
		$Properties[
		$name: $Type;]
    }]
