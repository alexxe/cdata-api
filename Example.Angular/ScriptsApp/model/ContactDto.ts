
 

    export class ContactDto implements IModel{ 
        type: string;
		constructor() {
			this.type = "Example.Data.Contract.Model.ContactDto,Example.Data.Contract";
			
                    
        }      
        
		
		id: number;
		edvNr: number;
		firstName: string;
		lastName: string;
		street: string;
		ort: string;
		customer: CustomerDto;
    }
