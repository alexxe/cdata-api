"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var data_service_1 = require('./services/data.service');
//import * as Descriptor from './lib/QueryDescriptor';
//import * as  DQuery from './lib/DQuery';
//import * as  CQuery from './lib/CQuery';
var AppComponent = (function () {
    function AppComponent(dataService) {
        this.dataService = dataService;
        this.search();
    }
    AppComponent.prototype.search = function () {
        // kommt von view
        var viewFilter;
        viewFilter = {};
        viewFilter["id"] = 1;
        viewFilter["solutionName"] = "s";
        viewFilter["name"] = "s";
        var filters = this.buildFilters(viewFilter);
        var projection = this.buildProjection();
        this.getResult(filters, projection);
    };
    AppComponent.prototype.buildFilters = function (viewFilter) {
        var filters;
        filters = [];
        var properties = Object.getOwnPropertyNames(viewFilter);
        for (var i = 0; i < properties.length; i++) {
            var property = properties[i];
            var value = viewFilter[property];
            if (property === "id") {
                filters.push({
                    id: {
                        operator: CQueryDescriptor.CompareOperator.GreaterThan,
                        value: value
                    }
                });
            }
            else if (property === "name") {
                filters.push({
                    name: {
                        operator: CQueryDescriptor.StringMethods.Contains,
                        value: value
                    }
                });
            }
            else if (property === "solutionName") {
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
    };
    AppComponent.prototype.buildProjection = function () {
        var p = new CData.Projector();
        p.project("name", function (x) { return x.name; });
        p.project("solutionName", function (x) { return x.solution.name2; });
        return p.getProjection();
    };
    AppComponent.prototype.getResult = function (filters, projection) {
        var projectQuery = new CData.CQuery(new Example.Model.ProjectDto(), filters, projection);
        var result;
        this.dataService.getProjects(projectQuery.getDescriptor())
            .subscribe(function (data) {
            result = data;
            var name = result[0].name;
            return null;
        }, function (error) { return console.error(error); });
    };
    AppComponent = __decorate([
        core_1.Component({
            selector: 'my-app',
            template: '<h2>My First Angular 2 App</h2>',
            providers: [data_service_1.ProjectService]
        }), 
        __metadata('design:paramtypes', [data_service_1.ProjectService])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=mainApp.js.map