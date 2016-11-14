
module Example.Model {  

    export class CustomerDto extends CData.IModel{ 
		constructor() {
			super(); 
			this.type = "Example.Data.Contract.Model.CustomerDto,Example.Data.Contract";
			
                    
        }      
        
		
		id: string;
		edvNr: string;
		customerNr: string;
		firma1: string;
		firma2: string;
		shortName: string;
		street: string;
		ort: string;
		contacts: ContactDto[];
    }
}