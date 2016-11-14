/// <reference path="../../../Covis.Data.Script.CQuery/model/IModel.ts" />
module CData {
export interface IModel {
    
    }

export class Projector<T extends IModel> {
    projections: Array<Binding>;
    constructor() {
        this.projections = [];
    }
    project(alias: string, property: (x: T) => void) {
        let p = property.toString().split('.');
        var path;
        for (var i = 1; i < p.length; i++) {
            if (path === undefined) {
                path = p[i];
            } else {
                path = path + "." + p[i];
            }
            
        }
        let binding = new Binding(alias,new CQueryDescriptor.MemberNode(path.split(';')[0]));
        
        this.projections.push(binding);
    }

    getProjection() {
        return this.projections;
    }
}


export class Binding {
    Key: string;
    Value:CQueryDescriptor.INode;
    constructor(key: string, value: CQueryDescriptor.INode) {
        this.Key = key;
        this.Value = value;
    }
    
}


    export class CQuery<T extends IFilterDescriptor> {
        private WhereNode: CQueryDescriptor.CallNode;
        private descriptor: CQueryDescriptor.CQueryDescriptor;
        constructor(model: CData.IModel,filters: T[],projections?:Binding[]) {
            this.descriptor = new CQueryDescriptor.CQueryDescriptor(model);
            for (var i = 0; i < filters.length; i++) {
                this.buildFilter(filters[i], "");
            }

            this.buildProjection(projections);
            
        }

        buildProjection(bindings: Binding[]) {
            var node = new CQueryDescriptor.ProjectorNode();
            node.Bindings = bindings;
            node.Left = this.descriptor.Root;
            this.descriptor.Root = node;
            this.descriptor.QueryType = CQueryDescriptor.QueryType.AnonymeProjection;
        }

        getDescriptor(): CQueryDescriptor.CQueryDescriptor {
            return this.descriptor;
        }
        private buildFilter(obj: IFilterDescriptor, path: string) {
            let properties = Object.getOwnPropertyNames(obj);
            for (let i = 0; i < properties.length; i++) {
                let property = properties[i];
                let value = obj[property];
                let operator = <IOperator<any>>value;
                let method = <IMethod<any>>value;
                if (method.method != null) {
                    //let track = new Tracker();
                    //track.path = path + "." + property;
                    //track.operator = method.method;
                    //track.value = method.filter;
                    //this.tracker.push(track);
                }
                else if (operator.value == null) {
                    let member = "";
                    if (path !== "") {
                        member = path + "." + property;
                    } else {
                        member = property;
                    }
                    this.buildFilter(value, member);
                }
                else if (operator.value instanceof Array){
                    let member = "";
                    if (path !== "") {
                        member = path + "." + property;
                    } else {
                        member = property;
                    }
                    this.addMethodWhere(CQueryDescriptor.BinaryOperator.And, member, CQueryDescriptor.Methods.In, value.value);
                }
                else {
                    let member = "";
                    if (path !== "") {
                        member = path + "." + property;
                    } else {
                        member = property;
                    }
                    if (CQueryDescriptor.CompareOperator[value.operator] !== undefined) {
                        this.addBinaryWhere(CQueryDescriptor.BinaryOperator.And, member, value.operator, value.value);
                    } else {
                        this.addMethodWhere(CQueryDescriptor.BinaryOperator.And, member, value.operator, value.value);
                    } 
                }

                //let o = filter[properties[i]];
                //let isMethod = (<IMethod<any>>o).method !== undefined;
                //if ((<IOperator<any>>o).value !== undefined && (<IOperator<any>>o).operator !== undefined) {
                //    let operator = (<IOperator<any>>o).operator;
                //    let value = (<IOperator<any>>o).value;
                //}
                //let isInOperator = (<IInOperator<any>>o).value !== undefined;
            }
        }

        

        private addBinaryWhere(logicOp: CQueryDescriptor.BinaryOperator, member: string, compareOp: CQueryDescriptor.CompareOperator, value: any) {
            let binaryNode = new CQueryDescriptor.BinaryNode(compareOp);
            binaryNode.Left = new CQueryDescriptor.MemberNode(member);
            binaryNode.Right = new CQueryDescriptor.ConstantNode(value);

            if (this.WhereNode == null) {
                this.WhereNode = new CQueryDescriptor.CallNode("Where");
                this.WhereNode.Right = binaryNode;
                this.AppendNode(this.WhereNode);
            } else {
                let andNode = new CQueryDescriptor.BinaryNode(logicOp);
                andNode.Left = this.WhereNode.Right as CQueryDescriptor.LNode;
                andNode.Right = binaryNode;

                this.WhereNode.Right = andNode;
            }


        }

        private addMethodWhere(logicOp: CQueryDescriptor.BinaryOperator, member: string, method: CQueryDescriptor.Methods | CQueryDescriptor.StringMethods, value: any) {
            let methodName;
            if (CQueryDescriptor.StringMethods[method] !== undefined) {
                methodName = CQueryDescriptor.StringMethods[method].toString();
            }
            else if (CQueryDescriptor.Methods[method] !== undefined) {
                methodName = CQueryDescriptor.Methods[method].toString();
            } else {
                alert("unbekannte method" + method);
            }
            let callNode = new CQueryDescriptor.CallNode(methodName);
            callNode.Left = new CQueryDescriptor.MemberNode(member);
            callNode.Right = new CQueryDescriptor.ConstantNode(value);

            if (this.WhereNode == null) {
                let call = new CQueryDescriptor.CallNode("Where");
                call.Right = callNode;
                this.AppendNode(call);
            }
            else {
                let andNode = new CQueryDescriptor.BinaryNode(logicOp);
                andNode.Left = this.WhereNode.Right as CQueryDescriptor.LNode;
                andNode.Right = callNode;
                this.WhereNode.Right = andNode;
            }


        }

        private AppendNode(node: CQueryDescriptor.BNode) {
            node.Left = this.descriptor.Root;
            this.descriptor.Root = node;
        }
    }

    export interface IFilter<T extends CData.IModel> {
        operator: CQueryDescriptor.CompareOperator | CQueryDescriptor.StringMethods;
    }

    export interface IFilterDescriptor {

    }

    export interface IOperator<T> {
        operator: CQueryDescriptor.CompareOperator | CQueryDescriptor.StringMethods;
        value: T;
    }

    export interface IInOperator<T> {
        value: T[];
    }

    export interface IMethod<T extends IFilterDescriptor> {
        method: CQueryDescriptor.Methods;
        value: T;
    }




}

