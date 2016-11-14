import {Component} from '@angular/core';


import {ProjectService} from './services/data.service';
//import * as Descriptor from './lib/QueryDescriptor';
//import * as  DQuery from './lib/DQuery';
//import * as  CQuery from './lib/CQuery';




@Component({
    selector: 'my-app',
    template: '<h2>My First Angular 2 App</h2>',
    providers: [ProjectService]
})
export class AppComponent {
    dataService: ProjectService;
    constructor(dataService: ProjectService) {
        this.dataService = dataService;
        this.search();
       
    }
    
    

    private search() {
        // kommt von view
        let viewFilter: any;
        viewFilter = {};
        viewFilter["id"] = 1;
        viewFilter["solutionName"] = "s";
        viewFilter["name"] = "s";

        let filters = this.buildFilters(viewFilter);
        let projection = this.buildProjection();
        this.getResult(filters, projection);
    }

    private buildFilters(viewFilter: any): Example.ModelDescriptors.IProjectDtoDescriptor[] {
        let filters: Example.ModelDescriptors.IProjectDtoDescriptor[];
        filters = [];
        let properties = Object.getOwnPropertyNames(viewFilter);
        for (let i = 0; i < properties.length; i++) {
            let property = properties[i];
            let value = viewFilter[property];
            if (property === "id") {
                filters.push({
                    id: {
                        operator: CQueryDescriptor.CompareOperator.GreaterThan,
                        value: value
                    }
                });
            } else if (property === "name") {
                filters.push({
                    name: {
                        operator: CQueryDescriptor.StringMethods.Contains,
                        value: value
                    }
                });
            } else if (property === "solutionName") {
                filters.push({
                    solution: {
                        name2: {
                            operator: CQueryDescriptor.StringMethods.Contains,
                            value: value
                        }
                    }
                });
            }

        }
        return filters;
    }

    private buildProjection(): Array<CData.Binding> {
        let p = new CData.Projector<Example.Model.ProjectDto>();
        p.project("name", (x) => x.name);
        p.project("solutionName", (x) => x.solution.name2);
        return p.getProjection();
    }

    private getResult(filters: Example.ModelDescriptors.IProjectDtoDescriptor[], projection:CData.Binding[]): void {
        let projectQuery = new CData.CQuery<Example.ModelDescriptors.IProjectDtoDescriptor>(new Example.Model.ProjectDto(), filters, projection);


        let result: Example.Model.ProjectDto[];
        this.dataService.getProjects(projectQuery.getDescriptor())
            .subscribe(
            data => {
                result = data;
                let name = result[0].name;
                return null;
            }
            ,
            error => console.error(error));

    }

    

    
}
