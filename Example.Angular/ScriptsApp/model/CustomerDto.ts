
 

    export class CustomerDto implements IModel{ 
        type: string;
		constructor() {
			this.type = "Example.Data.Contract.Model.CustomerDto,Example.Data.Contract";
			
                    
        }      
        
		
		id: number;
		edvNr: number;
		firma1: string;
		firma2: string;
		street: string;
		ort: string;
		contacts: ContactDto[];
    }
