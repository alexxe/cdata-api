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
module Example.Model { $Classes(*Dto)[ 

    export class $Name$TypeParameters extends CData.IModel{ 
		constructor() {
			super(); 
			this.type = "$FullName,Example.Data.Contract";
			
                    
        }      
        
		$Properties[
		$name: $Type;]
    }]
}