
module Example.Model {  

    export class ContactDto extends CData.IModel{ 
		constructor() {
			super(); 
			this.type = "Example.Data.Contract.Model.ContactDto,Example.Data.Contract";
			
                    
        }      
        
		
		id: string;
		edvNr: string;
		firstName: string;
		lastName: string;
		street: string;
		ort: string;
		customer: CustomerDto;
    }
}